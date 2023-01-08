using System;
using System.ComponentModel;
using Avalonia.Controls;
using TrayLamp.Abstractions;
using TrayLamp.Models;

namespace TrayLamp.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private AppModel Model { get; }
        private IResolver<IconColor?, WindowIcon> IconResolver { get; }

        public AppViewModel(
            AppModel model,
            IResolver<IconColor?, WindowIcon> iconResolver
        )
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            IconResolver = iconResolver ?? throw new ArgumentNullException(nameof(iconResolver));
            Model.PropertyChanged += Model_PropertyChanged;
        }

        private WindowIcon? _icon;
        public WindowIcon? Icon
        {
            get => _icon;
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon)));
                }
            }
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.IconColor))
            {
                RefreshTrayIcon();
            }
        }

        private void RefreshTrayIcon() => Icon = IconResolver.Resolve(Model.IconColor);

        public void Exit() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Exit)));

    }
}
