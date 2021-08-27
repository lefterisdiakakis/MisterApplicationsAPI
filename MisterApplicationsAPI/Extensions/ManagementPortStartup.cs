using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public class ManagementPortStartup
    {
        public ManagementPortStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health")
                    .RequireHost($"*:{Configuration["ManagementPort"]}");

                endpoints.MapGet("/{**path}", async context =>
                {
                    await context.Response.WriteAsync(
                        "Navigate to " +
                        $"http://localhost:{Configuration["ManagementPort"]}/health " +
                        "to see the health status.");
                });
            });
        }
    }
}
