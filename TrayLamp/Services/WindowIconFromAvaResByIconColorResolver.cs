using System;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using TrayLamp.Abstractions;
using TrayLamp.Models;

namespace TrayLamp.Services
{
    public class WindowIconFromAvaResByIconColorResolver : IResolver<IconColor?, WindowIcon>
    {
        private IAssetLoader Assets { get; }

        public WindowIconFromAvaResByIconColorResolver(IAssetLoader assets)
        {
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
        }

        public WindowIcon Resolve(IconColor? input)
        {
            string iconColorName = (input ?? IconColor.Grey).ToString().ToLower();
            Uri iconResUri = new Uri($"avares://{Assembly.GetExecutingAssembly().GetName().Name}/Assets/Icons/{iconColorName}.ico");
            IBitmap bitmap = new Bitmap(Assets.Open(iconResUri));
            WindowIcon trayIcon = new WindowIcon(bitmap);
            return trayIcon;
        }
    }
}
