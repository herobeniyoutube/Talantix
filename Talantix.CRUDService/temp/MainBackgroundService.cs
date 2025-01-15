
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Talantix.WebApi.temp
{
    public class MainBackgroundService : BackgroundService
    {
        public MainBackgroundService(MonitoringService ms,ILogger<MainBackgroundService> logger)
        {
            this.ms = ms;
            this.logger = logger;
        }

        private readonly MonitoringService ms;
        private readonly ILogger logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (ms.Connections.Count > 0)
                    {
                        logger.LogInformation($"{ms.Connections[0].State}");
                    }
                }
            });
                
        }
    }
}
