using TrayLamp.Abstractions;
using TrayLamp.Dtos;
using TrayLamp.Models;

namespace TrayLamp.Services
{
    public class IconColorFromHueUpdaterStatusResolver : IResolver<HueUpdaterStatus?, IconColor>
    {
        public IconColor Resolve(HueUpdaterStatus? input)
        {
            IconColor result = IconColor.Grey;

            if (input != null)
            {
                if (input.BuildStatus == HueUpdaterBuildStatus.Stable && input.ActivityStatus == HueUpdaterActivityStatus.Idle) { result = IconColor.Green; }
                if (input.BuildStatus == HueUpdaterBuildStatus.Stable && input.ActivityStatus == HueUpdaterActivityStatus.Building) { result = IconColor.Blue; }
                if (input.BuildStatus == HueUpdaterBuildStatus.Broken && input.ActivityStatus == HueUpdaterActivityStatus.Idle) { result = IconColor.Red; }
                if (input.BuildStatus == HueUpdaterBuildStatus.Broken && input.ActivityStatus == HueUpdaterActivityStatus.Building) { result = IconColor.Yellow; }
            }

            return result;
        }
    }
}
