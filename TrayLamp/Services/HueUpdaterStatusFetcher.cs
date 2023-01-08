using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using TrayLamp.Abstractions;
using TrayLamp.Dtos;

namespace TrayLamp.Services
{
    public class HueUpdaterStatusFetcher : IHueUpdaterStatusFetcher
    {
        private Url StatusFileUrl { get; }
        private int TimeoutSeconds { get; }

        public HueUpdaterStatusFetcher(
            string statusFileUrl,
            int? timeoutSeconds = null
        )
        {
            StatusFileUrl = new Url(statusFileUrl ?? throw new ArgumentNullException(nameof(statusFileUrl)));
            TimeoutSeconds = (timeoutSeconds.HasValue && timeoutSeconds.Value > 0) ? timeoutSeconds.Value : 2;
        }

        public HueUpdaterStatus? GetStatus()
        {
            HueUpdaterStatus? result = GetStatusAsync().GetAwaiter().GetResult();
            return result;
        }

        public async Task<HueUpdaterStatus?> GetStatusAsync()
        {
            HueUpdaterStatus? result;
            try
            {
                result = await StatusFileUrl.WithTimeout(TimeoutSeconds).GetJsonAsync<HueUpdaterStatus>();
            }
            catch (Exception ex) when (ex is FlurlHttpException || ex is FlurlHttpTimeoutException)
            {
                result = null;
            }
            return result;
        }
    }
}
