namespace ow_api.Infrastructure.Telemetry
{
    public class ControllerTelemetry : IControllerTelemetry
    {
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly ILogger<ControllerTelemetry> _logger;

        public ControllerTelemetry(ITelemetryTracker telemetryTracker, ILogger<ControllerTelemetry> logger)
        {
            _telemetryTracker = telemetryTracker;
            _logger = logger;
        }

        public void TrackEvent<TController>(ControllerTelemetryContext<TController> context)
        {
            _telemetryTracker.TrackEvent(context.Name, BuildTags(context));
        }

        public void TrackLogError<TController>(ControllerTelemetryContext<TController> context)
        {
            var tags = BuildTags(context);

            _telemetryTracker.TrackError(context.Name, tags);
            _logger.LogWarning("Controller telemetry error tracked: {ErrorName}", context.Name);
        }

        public void TrackLogException<TController>(ControllerTelemetryContext<TController> context, Exception exception)
        {
            var tags = new Dictionary<string, object?>(context.Tags);
            tags["exception.type"] = exception.GetType().Name;
            tags["exception.message"] = exception.Message;
            context.Tags = tags;

            _telemetryTracker.TrackError(context.Name, BuildTags(context));
            _logger.LogWarning(exception, "Controller telemetry exception tracked: {ErrorName}", context.Name);
        }

        private static Dictionary<string, object?> BuildTags<TController>(ControllerTelemetryContext<TController> context)
        {
            var tags = new Dictionary<string, object?>(context.Tags);

            tags["controller"] = typeof(TController).Name;
            tags["action"] = context.ActionName;
            tags["route"] = context.HttpContext.Request.Path.Value;

            return tags;
        }
    }
}
