using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using robot.Application.Features;
using robot.Application.Features.Robo.Queries;
using robot.Domain.Contract;
using robot.Infra.Data;
using System.Reflection;

namespace robot.WebApi
{
    public static class Setup
    {
        public static void AddMediator(this IServiceCollection services)
        {            
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Behaviours.MeasureTimePipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Behaviours.ValidateCommandPipeline<,>));            

            services.AddMediatR(typeof(Application.AppModule).GetTypeInfo().Assembly);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IRobotRepository, RobotRepository>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            //TODO: Colocar o SimpleInjector, pois o Dependency Injector do ASP.NET Core está com bug na utilização de Generic Constrants
            services.Add(ServiceDescriptor.Transient(typeof(IValidator<RobotQuery>), typeof(RobotQueryValidator)));
            //services.Scan(scan => scan
            //   .FromAssembliesOf(typeof(Application.AppModule))
            //   .AddClasses(cls => cls.AssignableTo(typeof(IValidator<>)))
            //    .AsImplementedInterfaces()
            //    .WithTransientLifetime());
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
