using System.ComponentModel;

namespace TrayLamp.Models
{
    public class AppModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IconColor? _iconColor = null;
        public IconColor? IconColor
        {
            get => _iconColor;
            set
            {
                if (value != null && value != _iconColor)
                {
                    _iconColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconColor)));
                }
            }
        }
    }
}
