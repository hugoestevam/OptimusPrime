using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using robot.Infra.Data;
using System.Reflection;
using robot.Domain.Features.Robo;
using robot.WebApi.Features.Robo;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace robot.WebApi
{
    public static class Setup
    {
        public static void AddSimpleInjector(this IServiceCollection services, Container container)
        {
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });
        }

        public static void AddMediator(this IServiceCollection services, Container container)
        {
            var assemblies = typeof(Application.AppModule).GetTypeInfo().Assembly;

            container.RegisterSingleton<IMediator>(() => new Mediator(container.GetInstance<IServiceProvider>()));
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                    typeof(Behaviours.MeasureTimePipeline<,>),
                    typeof(Behaviours.ValidateCommandPipeline<,>)
                });
        }

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration, Container container)
        {
            // Configurar o DbContext para usar PostgreSQL
            services.AddDbContext<RobotDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            container.Register<IRobotRepository, RobotDatabaseRepository>();
        }

        public static void AddValidators(this IServiceCollection services, Container container)
        {
            container.Collection.Register(typeof(IValidator<>), typeof(Application.AppModule).GetTypeInfo().Assembly);
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RobotMappingProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
