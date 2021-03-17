using NLog;

namespace PrintApi.Logging
{
    public static class LoggingExtensions
    {
        public static void SetServiceName(string serviceName)
        {
            GlobalDiagnosticsContext.Set("ServiceName", serviceName);
        }
    }
}