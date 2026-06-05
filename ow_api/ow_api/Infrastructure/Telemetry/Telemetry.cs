using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace ow_api.Infrastructure.Telemetry
{
    public static class Telemetry
    {
        public const string ServiceName = "ow_api";
        public static readonly ActivitySource ActivitySource = new(ServiceName);
        public static readonly Meter Meter = new(ServiceName);
        public static readonly Counter<long> EventsLogged = Meter.CreateCounter<long>("ow_api.events.logged");
        public static readonly Counter<long> ErrorsLogged = Meter.CreateCounter<long>("ow_api.errors.logged");

        public static Activity? StartActivity(string name)
        {
            return ActivitySource.StartActivity(name);
        }

        public static void TrackEvent(string eventName)
        {
            EventsLogged.Add(1, BuildTags("event.name", eventName));
        }

        public static void TrackEvent(string eventName, Dictionary<string, object?> dimensions)
        {
            EventsLogged.Add(1, BuildTags("event.name", eventName, dimensions));
        }

        public static void TrackError(string errorName)
        {
            ErrorsLogged.Add(1, BuildTags("error.name", errorName));
        }

        public static void TrackError(string errorName, Dictionary<string, object?> dimensions)
        {
            ErrorsLogged.Add(1, BuildTags("error.name", errorName, dimensions));
        }

        private static KeyValuePair<string, object?>[] BuildTags(string nameKey, string nameValue)
        {
            return [new KeyValuePair<string, object?>(nameKey, nameValue)];
        }

        private static KeyValuePair<string, object?>[] BuildTags(string nameKey, string nameValue, Dictionary<string, object?> dimensions)
        {
            var tags = new List<KeyValuePair<string, object?>>
            {
                new KeyValuePair<string, object?>(nameKey, nameValue)
            };

            foreach (var dimension in dimensions)
                tags.Add(new KeyValuePair<string, object?>(dimension.Key, dimension.Value));

            return tags.ToArray();
        }
    }
}
