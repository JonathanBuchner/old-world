using Microsoft.AspNetCore.Http;

namespace ow_api.Infrastructure.Telemetry
{
    public class ControllerTelemetryContext<TController>
    {
        public required string Name { get; set; }
        public required string ActionName { get; set; }
        public required HttpContext HttpContext { get; set; }
        public Dictionary<string, object?> Dimensions { get; set; } = [];
    }
}
