using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.BackgroundServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.Core
{
    public class StartupHostedService : IHostedService, IDisposable
    {
        private readonly int _delaySeconds = 15;
        private readonly ILogger _logger;
        private readonly StartupHostedServiceHealthCheck _startupHostedServiceHealthCheck;
        private readonly IHealthCheck healthCheck;

        public StartupHostedService(ILogger<StartupHostedService> logger,
            StartupHostedServiceHealthCheck startupHostedServiceHealthCheck,IHealthCheck healthCheck)
        {
            _logger = logger;
            _startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
            this.healthCheck = healthCheck;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Startup Background Service is starting.");

            // Simulate the effect of a long-running startup task.
            Task.Run(async () =>
            {
                await Task.Delay(_delaySeconds * 1000);
                //this.healthCheck.CheckHealthAsync(cancellationToken);
                _startupHostedServiceHealthCheck.StartupTaskCompleted = true;

                _logger.LogInformation("Startup Background Service has started.");
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Startup Background Service is stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
