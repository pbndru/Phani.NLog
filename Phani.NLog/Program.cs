using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace Phani.NLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Starting main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "there is an exception");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                    {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                }).UseNLog();
    }
}
