using Microsoft.AspNetCore.Mvc;
using ow_api.Infrastructure.Telemetry;

namespace ow_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> Logger;
        protected readonly IControllerTelemetry ControllerTelemetry;

        protected BaseController(ILogger<TController> logger, IControllerTelemetry controllerTelemetry)
        {
            Logger = logger;
            ControllerTelemetry = controllerTelemetry;
        }

        protected void TrackEvent(string eventName, string actionName)
        {
            ControllerTelemetry.TrackEvent(BuildTelemetryContext(eventName, actionName, []));
        }

        protected void TrackEvent(string eventName, string actionName, Dictionary<string, object?> tags)
        {
            ControllerTelemetry.TrackEvent(BuildTelemetryContext(eventName, actionName, tags));
        }

        protected void TrackLogError(string errorName, string actionName)
        {
            ControllerTelemetry.TrackLogError(BuildTelemetryContext(errorName, actionName, []));
        }

        protected void TrackLogError(string errorName, string actionName, Dictionary<string, object?> tags)
        {
            ControllerTelemetry.TrackLogError(BuildTelemetryContext(errorName, actionName, tags));
        }

        protected void TrackLogException(string errorName, string actionName, Exception exception)
        {
            TrackLogException(errorName, actionName, exception, []);
        }

        protected void TrackLogException(string errorName, string actionName, Exception exception, Dictionary<string, object?> tags)
        {
            ControllerTelemetry.TrackLogException(BuildTelemetryContext(errorName, actionName, tags), exception);
        }

        private ControllerTelemetryContext<TController> BuildTelemetryContext(string name, string actionName, Dictionary<string, object?> tags)
        {
            return new ControllerTelemetryContext<TController>()
            {
                Name = name,
                ActionName = actionName,
                HttpContext = HttpContext,
                Tags = tags
            };
        }
    }
}
