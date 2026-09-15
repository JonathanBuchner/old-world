using Microsoft.AspNetCore.Http;

namespace OldWorld.Api.Telemetry
{
    public class ControllerTelemetryContext<TController>
    {
        public required string Name { get; set; }
        public required string ActionName { get; set; }
        public required HttpContext HttpContext { get; set; }
        public Dictionary<string, object?> Tags { get; set; } = [];
    }
}
