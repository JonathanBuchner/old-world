using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OldWorld.Api.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [AllowAnonymous]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "healthy" });
        }
    }
}
