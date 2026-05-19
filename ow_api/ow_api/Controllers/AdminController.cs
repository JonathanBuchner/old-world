using Microsoft.AspNetCore.Mvc;
using ow_api.Infrastructure.Telemetry;

namespace ow_api.Controllers
{
    [Route("admin")]
    public class AdminController : BaseController<AdminController>
    {
        public AdminController(ILogger<AdminController> logger, ITelemetryTracker telemetryTracker) : base(logger, telemetryTracker)
        {
        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            Logger.LogInformation("Admin ping requested");
            TelemetryTracker.TrackEvent("admin.ping.requested", new Dictionary<string, object?>
            {
                ["controller"] = nameof(AdminController),
                ["action"] = nameof(Ping),
                ["route"] = "admin/ping"
            });

            return Ok("pong");
        }
    }
}
