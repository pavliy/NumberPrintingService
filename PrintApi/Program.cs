using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Web;

using PrintApi.Logging;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PrintApi
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(
                    logging =>
                        {
                            logging.ClearProviders();
                            logging.SetMinimumLevel(LogLevel.Debug);
                        })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        public static void Main(string[] args)
        {
            LoggingExtensions.SetServiceName("PrintApi");
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                IHost host = Program.CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopping service because of exception: {0}", ex.Message);
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}