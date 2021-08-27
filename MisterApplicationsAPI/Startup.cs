using Application.ApplicationMenus;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MisterApplicationsAPI", Version = "v1" });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer" }
                        }, new List<string>()
                    }
                });

            });

            services.AddHealthChecks();
            services.AddPersistance(Configuration);
            services.AddIdentity(Configuration);
            services.AddApplicationServices(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(name: "All",
                                  builder =>
                                  {
                                      builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                                  });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            ApplicationProperties applicationProperties = new ApplicationProperties
            {
                JWTKey = "sdsdsdsdfdfrfDedfededefdfedaszded"
            };

            services.AddTransient<TokenService>();
            services.AddTransient<EncryptionService>();
            services.AddSingleton(applicationProperties);
            services.AddScoped<UserAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MisterApplicationsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("All");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/{**path}", async context =>
                {
                    await context.Response.WriteAsync(
                        "Navigate to /health to see the health status.");
                });
                endpoints.MapControllers();
            });
        }
    }
}
