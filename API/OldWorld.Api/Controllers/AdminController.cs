using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OldWorld.Api.Telemetry;

namespace OldWorld.Api.Controllers
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
