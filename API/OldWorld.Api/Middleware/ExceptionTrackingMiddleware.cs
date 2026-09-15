using System.Net;
using Microsoft.Extensions.Options;
using OldWorld.Infrastructure.Configuration;
using OldWorld.Infrastructure.Telemetry;
using OldWorld.Api.Models.Api;

namespace OldWorld.Api.Middleware
{
    public class ExceptionTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionTrackingMiddleware> _logger;
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly GameRulesSettings _gameRulesSettings;

        public ExceptionTrackingMiddleware(RequestDelegate next, ILogger<ExceptionTrackingMiddleware> logger, ITelemetryTracker telemetryTracker, IOptions<GameRulesSettings> gameRulesOptions)
        {
            _next = next;
            _logger = logger;
            _telemetryTracker = telemetryTracker;
            _gameRulesSettings = gameRulesOptions.Value;
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
                    ["game.version"] = _gameRulesSettings.DefaultGameVersion.ToString(),
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
