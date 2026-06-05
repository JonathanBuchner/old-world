using System.Net;
using ow_api.Infrastructure.Telemetry;
using ow_api.Models.Api;

namespace ow_api.Infrastructure.Middleware
{
    public class ExceptionTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionTrackingMiddleware> _logger;
        private readonly ITelemetryTracker _telemetryTracker;

        public ExceptionTrackingMiddleware(RequestDelegate next, ILogger<ExceptionTrackingMiddleware> logger, ITelemetryTracker telemetryTracker)
        {
            _next = next;
            _logger = logger;
            _telemetryTracker = telemetryTracker;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled API exception");
                _telemetryTracker.TrackError("api.unhandled.exception", new Dictionary<string, object?>
                {
                    ["exception.type"] = ex.GetType().Name,
                    ["exception.message"] = ex.Message,
                    ["route"] = context.Request.Path.Value,
                    ["method"] = context.Request.Method
                });

                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.Fail("An unexpected error occurred.");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
