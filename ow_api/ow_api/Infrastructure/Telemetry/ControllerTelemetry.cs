using Microsoft.Extensions.Options;
using ow_api.Infrastructure.Settings;

namespace ow_api.Infrastructure.Telemetry
{
    public class ControllerTelemetry : IControllerTelemetry
    {
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly ILogger<ControllerTelemetry> _logger;
        private readonly GameRulesSettings _gameRulesSettings;

        public ControllerTelemetry(ITelemetryTracker telemetryTracker, ILogger<ControllerTelemetry> logger, IOptions<GameRulesSettings> gameRulesOptions)
        {
            _telemetryTracker = telemetryTracker;
            _logger = logger;
            _gameRulesSettings = gameRulesOptions.Value;
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

        private Dictionary<string, object?> BuildTags<TController>(ControllerTelemetryContext<TController> context)
        {
            var tags = new Dictionary<string, object?>(context.Tags);

            if (!tags.ContainsKey("game.version"))
                tags["game.version"] = _gameRulesSettings.DefaultGameVersion.ToString();

            tags["controller"] = typeof(TController).Name;
            tags["action"] = context.ActionName;
            tags["route"] = context.HttpContext.Request.Path.Value;

            return tags;
        }
    }
}
