using MediatR;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Shared.Infrastructure.Behaviors;

namespace PersonDirectory.DI
{
    public static class DependencyResolverExtensions
    {
        public static IServiceCollection AddApplicationPipelines(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

                services.AddValidatorsFromAssembly(assembly);
            }

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
