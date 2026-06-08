using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ow_api.Infrastructure.Telemetry;

namespace ow_api.Controllers
{
    [ApiVersion(1.0)]
    public class AdminController : BaseController<AdminController>
    {
        public AdminController(ILogger<AdminController> logger, IControllerTelemetry controllerTelemetry) : base(logger, controllerTelemetry)
        {
        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            TrackEvent("admin.ping.requested", nameof(Ping));

            return Ok("pong");
        }
    }
}
