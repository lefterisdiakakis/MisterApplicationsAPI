﻿#define SYSTEM_TEXT_JSON

using System.IO;
#if SYSTEM_TEXT_JSON
using System.Text;
using System.Text.Json;
#endif
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Persistance.Implementations;
#if !SYSTEM_TEXT_JSON
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endif

namespace API.Extensions
{
    public class CustomWriterStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region snippet_ConfigureServices
            services.AddHealthChecks()
                .AddMemoryHealthCheck("memory");
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    // This custom writer formats the detailed status as JSON.
                    ResponseWriter = WriteResponse
                });

                endpoints.MapGet("/{**path}", async context =>
                {
                    await context.Response.WriteAsync(
                        "Navigate to /health to see the health status.");
                });
            });
        }

#if SYSTEM_TEXT_JSON
        #region snippet_WriteResponse_SystemTextJson
        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, options))
            {
                writer.WriteStartObject();
                writer.WriteString("status", result.Status.ToString());
                writer.WriteStartObject("results");
                foreach (var entry in result.Entries)
                {
                    writer.WriteStartObject(entry.Key);
                    writer.WriteString("status", entry.Value.Status.ToString());
                    writer.WriteString("description", entry.Value.Description);
                    writer.WriteStartObject("data");
                    foreach (var item in entry.Value.Data)
                    {
                        writer.WritePropertyName(item.Key);
                        JsonSerializer.Serialize(
                            writer, item.Value, item.Value?.GetType() ??
                            typeof(object));
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            var json = Encoding.UTF8.GetString(stream.ToArray());

            return context.Response.WriteAsync(json);
        }
        #endregion
#else
        #region snippet_WriteResponse_NewtonSoftJson
        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));

            return context.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
        #endregion
#endif
    }
}
