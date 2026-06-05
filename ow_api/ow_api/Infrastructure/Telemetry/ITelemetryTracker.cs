using System.Diagnostics;

namespace ow_api.Infrastructure.Telemetry
{
    public interface ITelemetryTracker
    {
        Activity? StartActivity(string name);
        void TrackEvent(string eventName);
        void TrackEvent(string eventName, Dictionary<string, object?> dimensions);
        void TrackError(string errorName);
        void TrackError(string errorName, Dictionary<string, object?> dimensions);
    }
}
