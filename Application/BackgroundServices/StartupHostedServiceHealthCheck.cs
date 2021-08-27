﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.BackgroundServices
{
    public class StartupHostedServiceHealthCheck : IHealthCheck
    {
        private volatile bool _startupTaskCompleted = false;

        public static string GetName()
        {
            return "slow_dependency_check";
        }

        public bool StartupTaskCompleted
        {
            get => _startupTaskCompleted;
            set => _startupTaskCompleted = value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (StartupTaskCompleted)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("The startup task is finished."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("The startup task is still running."));
        }
    }
}
