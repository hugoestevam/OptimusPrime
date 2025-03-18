using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using robot.WebApi.Base;
using SimpleInjector;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Logs;

namespace robot.WebApi
{
    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddAutoMapper();

            services.AddSimpleInjector(container);

            services.AddOpenTelemetry()
                .WithLogging(builder =>
                {
                    builder
                    .AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri("http://localhost:4317");
                    });
                })
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("RobotAPI"))
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation((opt) => opt.SetDbStatementForText = false)
                        .AddOtlpExporter(otlpOptions =>
                        {
                            otlpOptions.Endpoint = new Uri("http://localhost:4317");
                        });

                });

            // Registra o Swagger para documentar a API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Robot API",
                    Description = "A simple example of interaction with the robot through the ASP.NET Core Web API.",
                    TermsOfService = new Uri("https://example.com/termofservice"),
                    Contact = new OpenApiContact
                    {
                        Name = "Hugo Estevam Longo",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/hugoestevam")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // Adiciona os comentários da API no Swagger JSON da UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            // Registra os Repositorios que mantém a persistência do Robo
            services.AddRepositories(Configuration, container);

            // Registra os Validators que executam as validações dos Inputs
            services.AddValidators(container);

            // Registra o Mediator que manipula as chamadas (no caso MediatR)
            services.AddMediator(container);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Habilita o Middleware do Swagger.
            app.UseSwagger();

            // Configura o Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Robot API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowSpecificOrigin");

            // Add UseRouting before UseEndpoints
            app.UseRouting();

            // Integrate Simple Injector
            app.UseSimpleInjector(container);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            container.RegisterInstance<IServiceProvider>(container);
            
            // Configurar injeção de propriedade
            container.RegisterInitializer<ApiControllerBase>(controller =>
            {
                controller.Mapper = container.GetInstance<IMapper>();
            });

            container.Verify();
        }
    }
}
