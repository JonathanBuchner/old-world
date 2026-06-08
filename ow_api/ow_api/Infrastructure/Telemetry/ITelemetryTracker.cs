using System.Diagnostics;

namespace ow_api.Infrastructure.Telemetry
{
    public interface ITelemetryTracker
    {
        Activity? StartActivity(string name);
        void TrackEvent(string eventName);
        void TrackEvent(string eventName, Dictionary<string, object?> tags);
        void TrackError(string errorName);
        void TrackError(string errorName, Dictionary<string, object?> tags);
    }
}
