using TrayLamp.Dtos;

namespace TrayLamp.Abstractions
{
    public interface IHueUpdaterStatusFetcher
    {
        public HueUpdaterStatus? GetStatus();
    }
}
