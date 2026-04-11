namespace ow_api.Infrastructure.Services
{
    /// <summary>
    /// Appliation insights settigns.
    /// </summary>
    public class ApplicationInsightsSettings
    {
        /// <summary>
        /// Adds the AutoCollectedMetricExtractor, which automatically collects and extracts metrics from the application. This can include performance metrics, request metrics, and other relevant data that helps in monitoring and analyzing the application's behavior.
        /// </summary>
        public bool AddAutoCollectedMetricExtractor { get; set; } = true;

        /// <summary>
        /// API Authentication key
        /// </summary>
        /// <remarks>Empty key is provided on instantiation which will fail validation.</remarks>
        public string ApiKey { get; set; } = "";

        /// <summary>
        /// Application insights connection string.
        /// </summary>
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// Enables adaptive sampling, which dynamically adjusts the sampling rate based on the volume of telemetry data being generated. This helps to ensure that the most relevant and important telemetry data is collected while reducing the overall volume of data sent to Application Insights, which can help to optimize performance and reduce costs.
        /// </summary>
        /// <value>True means adaptive sampling is enabled.</value>
        public bool EnableAdaptiveSampling { get; set; } = true;

        /// <summary>
        /// Enables the dependency tracking telemetry module, which automatically tracks dependencies such as HTTP calls, database queries, and other external services that the application interacts with. This can help to identify performance bottlenecks and issues related to external dependencies.
        /// </summary>
        public bool EnableDependencyTrackingTelemetryModule { get; set; } = true;

        /// <summary>
        /// Enables the performance counter collection module, which collects performance counter data from the application and sends it to Application Insights. This can include metrics such as CPU usage, memory usage, and other performance-related data that can help to identify issues and optimize the application's performance.
        /// </summary>
        public bool EnablePerformanceCounterCollectionModule { get; set; } = false;

        /// <summary>
        /// Enables the request tracking telemetry module, which automatically tracks incoming HTTP requests to the application and collects telemetry data such as request duration, response codes, and other relevant information. This can help to identify performance issues and monitor the overall health of the application.
        /// </summary>
        public bool EnableRequestTrackingTelemetryModule { get; set; } = false;


    }
}
