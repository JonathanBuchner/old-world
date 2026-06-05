using Microsoft.AspNetCore.Mvc;
using ow_api.Infrastructure.Telemetry;

namespace ow_api.Controllers
{
    [Route("admin")]
    public class AdminController : BaseController<AdminController>
    {
        public AdminController(ILogger<AdminController> logger, IControllerTelemetry controllerTelemetry) : base(logger, controllerTelemetry)
        {
        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            Logger.LogInformation("Admin ping requested");
            TrackEvent("admin.ping.requested", nameof(Ping));

            return Ok("pong");
        }
    }
}
