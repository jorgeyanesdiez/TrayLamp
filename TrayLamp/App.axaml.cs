using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using TrayLamp.Abstractions;
using TrayLamp.Dtos;
using TrayLamp.Models;
using TrayLamp.Services;
using TrayLamp.ViewModels;

namespace TrayLamp;

public partial class App : Application
{
    private IClassicDesktopStyleApplicationLifetime? Desktop { get; set; }
    private IAssetLoader? Assets { get; set; }
    private IStartStopService? TrayIconRefresher { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();
        Desktop = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime ?? throw new InvalidCastException(nameof(ApplicationLifetime));
        Assets = AvaloniaLocator.Current?.GetService<IAssetLoader>() ?? throw new InvalidCastException(nameof(IAssetLoader));
        ConfigureServices();
        TrayIconRefresher?.Start();
    }

    private void ConfigureServices()
    {
        if (Desktop != null && Assets != null)
        {
            Desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            IResolver<IconColor?, WindowIcon> iconResolver = new WindowIconFromAvaResByIconColorResolver(Assets);

            IResolver<string, AppSettings> settingsResolver = new AppSettingsResolver();
            AppSettings settings = settingsResolver.Resolve("appsettings.json");

            AppModel appModel = new();
            AppViewModel appViewModel = new(appModel, iconResolver);
            appViewModel.PropertyChanged += AppViewModel_PropertyChanged;
            DataContext = appViewModel;

            IHueUpdaterStatusFetcher statusFetcher = new HueUpdaterStatusFetcher(settings.StatusUrl, settings.TimeoutSeconds);
            IResolver<HueUpdaterStatus?, IconColor> iconColorResolver = new IconColorFromHueUpdaterStatusResolver();
            TrayIconRefresher = new TrayIconRefresher(statusFetcher, iconColorResolver, appModel, settings.DelaySeconds);
        }
    }

    private void AppViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AppViewModel.Exit))
        {
            Exit();
        }
    }

    private void Exit()
    {
        TrayIconRefresher?.Stop();
        Desktop?.Shutdown();
    }
}
