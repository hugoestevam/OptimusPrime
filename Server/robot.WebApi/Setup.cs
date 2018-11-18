using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using robot.Application.Features;
using robot.Domain.Contract;
using robot.Infra.Data;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace robot.WebApi
{
    public static class Setup
    {
        public static void AddSimpleInjector(this IServiceCollection services, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
          
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));

            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        public static void AddMediator(this IServiceCollection services, Container container)
        {
            var assemblies = typeof(Application.AppModule).GetTypeInfo().Assembly;

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(Behaviours.MeasureTimePipeline<,>),
                typeof(Behaviours.ValidateCommandPipeline<,>)
            });
        }

        public static void AddRepositories(this IServiceCollection services, Container container)
        {
            container.RegisterSingleton<IRobotRepository, RobotRepository>();
        }

        public static void AddValidators(this IServiceCollection services, Container container)
        {
            container.Collection.Register(typeof(IValidator<>), typeof(Application.AppModule).GetTypeInfo().Assembly);
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
        }
    }
}
