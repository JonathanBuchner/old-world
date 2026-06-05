namespace ow_api.Infrastructure.Telemetry
{
    public interface IControllerTelemetry
    {
        void TrackEvent<TController>(ControllerTelemetryContext<TController> context);
        void TrackLogError<TController>(ControllerTelemetryContext<TController> context);
        void TrackLogException<TController>(ControllerTelemetryContext<TController> context, Exception exception);
    }
}
