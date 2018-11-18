using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddMvc();

            services.AddCors();

            services.AddAutoMapper();         

            services.AddSimpleInjector(container);

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

            // Registra os Repositorios que mantém a persistência do Robo
            services.AddRepositories(container);

            // Registra os Validators que executam as validações dos Inputs
            services.AddValidators(container);

            // Registra o Mediator que manipula as chamadas (no caso MediatR)
            services.AddMediator(container);          

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

            container.RegisterMvcControllers(app);

            container.Verify();
        }
    }
}
