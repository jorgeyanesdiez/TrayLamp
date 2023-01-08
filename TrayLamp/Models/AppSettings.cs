namespace TrayLamp.Models
{
    public class AppSettings
    {
        private string _statusUrl = string.Empty;
        public string StatusUrl
        {
            get => _statusUrl;
            set => _statusUrl = !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
        }

        public static readonly int DelaySecondsDefault = 5;
        private int _delaySeconds = DelaySecondsDefault;
        public int DelaySeconds
        {
            get => _delaySeconds;
            set => _delaySeconds = value > 0 ? value : DelaySecondsDefault;
        }

        public static readonly int TimeoutSecondsDefault = 2;
        private int _timeoutSeconds = TimeoutSecondsDefault;
        public int TimeoutSeconds
        {
            get => _timeoutSeconds;
            set => _timeoutSeconds = (value > 0 && value <= DelaySeconds) ? value : TimeoutSecondsDefault;
        }
    }
}
