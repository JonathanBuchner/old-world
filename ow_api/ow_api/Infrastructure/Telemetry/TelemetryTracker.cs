using System.Diagnostics;

namespace ow_api.Infrastructure.Telemetry
{
    public class TelemetryTracker : ITelemetryTracker
    {
        public Activity? StartActivity(string name)
        {
            return Telemetry.StartActivity(name);
        }

        public void TrackEvent(string eventName)
        {
            Telemetry.TrackEvent(eventName);
        }

        public void TrackEvent(string eventName, Dictionary<string, object?> tags)
        {
            Telemetry.TrackEvent(eventName, tags);
        }

        public void TrackError(string errorName)
        {
            Telemetry.TrackError(errorName);
        }

        public void TrackError(string errorName, Dictionary<string, object?> tags)
        {
            Telemetry.TrackError(errorName, tags);
        }
    }
}
