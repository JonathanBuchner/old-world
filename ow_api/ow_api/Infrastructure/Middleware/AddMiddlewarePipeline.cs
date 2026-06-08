using ow_api.Infrastructure.Settings;

namespace ow_api.Infrastructure.Middleware
{
    public static class AddMiddlewarePipeline
    {
        public static void All(WebApplication app)
        {
            UseSwaggerWhenEnabled(app);
            app.UseMiddleware<ExceptionTrackingMiddleware>();
        }

        private static void UseSwaggerWhenEnabled(WebApplication app)
        {
            var settings = app.Configuration.GetSection(nameof(OldWorldApiSettings)).Get<OldWorldApiSettings>();

            if (!app.Environment.IsDevelopment() || settings?.EnableSwagger != true)
                return;

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
