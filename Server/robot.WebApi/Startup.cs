using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using robot.Application;
using robot.Application.Features;
using robot.Domain.Contract;
using robot.Infra.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace robot.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });            

            // Registra o Swagger para documentar a API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Robot API",
                    Description = "A simple example of interaction with the robot through the ASP.NET Core Web API.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Hugo Estevam Longo",
                        Email = string.Empty,
                        Url = "https://twitter.com/hugoestevam"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }

                });

                // Adiciona os comentários da API no Swagger JSON da UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<ServiceFactory>(p => p.GetService);

            // Registra o Repositorio que mantém o cache do Robo
            services.AddSingleton<IRobotRepository, RobotRepository>();

            // Registra o Mediator que manipula as chamadas
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator), typeof(AppModule))
                .AddClasses()
                .AsImplementedInterfaces());            

            services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseCors(builder => builder.WithOrigins("http://localhost:8000")
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseMvc();
        }
    }
}
