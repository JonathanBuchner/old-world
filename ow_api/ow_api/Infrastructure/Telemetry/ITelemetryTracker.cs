using System.Diagnostics;

namespace ow_api.Infrastructure.Telemetry
{
    public interface ITelemetryTracker
    {
        Activity? StartActivity(string name);
        void TrackEvent(string eventName, Dictionary<string, object?>? dimensions = null);
        void TrackError(string errorName, Dictionary<string, object?>? dimensions = null);
    }
}
