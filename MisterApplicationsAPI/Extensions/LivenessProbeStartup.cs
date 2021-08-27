using System;
using Application.BackgroundServices;
using Application.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Extensions
{
    public class LivenessProbeStartup
    {
        private const string HealthCheckServiceAssembly = "Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckPublisherHostedService";

        public void ConfigureServices(IServiceCollection services)
        {
            #region snippet_ConfigureServices
            services.AddHostedService<StartupHostedService>();
            services.AddSingleton<StartupHostedServiceHealthCheck>();

            services.AddHealthChecks()
                .AddCheck<StartupHostedServiceHealthCheck>(
                    "hosted_service_startup",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "ready" });

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });

            services.AddSingleton<IHealthCheckPublisher, ReadinessPublisher>();
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // The readiness check uses all registered checks with the 'ready' tag.
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    // Exclude all checks and return a 200-Ok.
                    Predicate = (_) => false
                });

                endpoints.MapGet("/{**path}", async context =>
                {
                    await context.Response.WriteAsync(
                        "Navigate to /health/ready to see the readiness status.");
                    await context.Response.WriteAsync(Environment.NewLine);
                    await context.Response.WriteAsync(
                        "Navigate to /health/live to see the liveness status.");
                });
            });
        }
    }
}
