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
            _telemetryTracker.TrackEvent(context.Name, BuildDimensions(context));
        }

        public void TrackLogError<TController>(ControllerTelemetryContext<TController> context)
        {
            var dimensions = BuildDimensions(context);

            _telemetryTracker.TrackError(context.Name, dimensions);
            _logger.LogWarning("Controller telemetry error tracked: {ErrorName}", context.Name);
        }

        public void TrackLogException<TController>(ControllerTelemetryContext<TController> context, Exception exception)
        {
            var dimensions = new Dictionary<string, object?>(context.Dimensions);
            dimensions["exception.type"] = exception.GetType().Name;
            dimensions["exception.message"] = exception.Message;
            context.Dimensions = dimensions;

            _telemetryTracker.TrackError(context.Name, BuildDimensions(context));
            _logger.LogWarning(exception, "Controller telemetry exception tracked: {ErrorName}", context.Name);
        }

        private static Dictionary<string, object?> BuildDimensions<TController>(ControllerTelemetryContext<TController> context)
        {
            var dimensions = new Dictionary<string, object?>(context.Dimensions);

            dimensions["controller"] = typeof(TController).Name;
            dimensions["action"] = context.ActionName;
            dimensions["route"] = context.HttpContext.Request.Path.Value;

            return dimensions;
        }
    }
}
