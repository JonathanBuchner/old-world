using System.Diagnostics;

namespace ow_api.Infrastructure.Telemetry
{
    public class TelemetryTracker : ITelemetryTracker
    {
        public Activity? StartActivity(string name)
        {
            return Telemetry.StartActivity(name);
        }

        public void TrackEvent(string eventName, Dictionary<string, object?>? dimensions = null)
        {
            Telemetry.TrackEvent(eventName, dimensions);
        }

        public void TrackError(string errorName, Dictionary<string, object?>? dimensions = null)
        {
            Telemetry.TrackError(errorName, dimensions);
        }
    }
}
