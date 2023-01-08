using System;
using System.Threading;
using TrayLamp.Abstractions;
using TrayLamp.Dtos;
using TrayLamp.Models;

namespace TrayLamp.Services
{
    public class TrayIconRefresher : AbstractBackgroundStartStopService
    {
        private IHueUpdaterStatusFetcher StatusFetcher { get; }
        private IResolver<HueUpdaterStatus?, IconColor> IconColorResolver { get; }
        private AppModel Model { get; }
        private int DelaySeconds { get; }

        public TrayIconRefresher(
            IHueUpdaterStatusFetcher statusFetcher,
            IResolver<HueUpdaterStatus?, IconColor> iconColorResolver,
            AppModel model,
            int? delaySeconds = null
        ) : base()
        {
            StatusFetcher = statusFetcher ?? throw new ArgumentNullException(nameof(statusFetcher));
            IconColorResolver = iconColorResolver ?? throw new ArgumentNullException(nameof(iconColorResolver));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            DelaySeconds = (delaySeconds.HasValue && delaySeconds.Value > 0) ? delaySeconds.Value : 2;
        }

        protected override void DoWork()
        {
            Model.IconColor = IconColor.Grey;
            while (!CancellationToken.IsCancellationRequested)
            {
                HueUpdaterStatus? status = StatusFetcher.GetStatus();
                IconColor iconColor = IconColorResolver.Resolve(status);
                Model.IconColor = iconColor;
                Thread.Sleep(DelaySeconds * 1000);
            }
        }
    }
}
