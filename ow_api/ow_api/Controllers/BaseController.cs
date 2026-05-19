using Microsoft.AspNetCore.Mvc;
using ow_api.Infrastructure.Telemetry;

namespace ow_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> Logger;
        protected readonly ITelemetryTracker TelemetryTracker;

        protected BaseController(ILogger<TController> logger, ITelemetryTracker telemetryTracker)
        {
            Logger = logger;
            TelemetryTracker = telemetryTracker;
        }
    }
}
