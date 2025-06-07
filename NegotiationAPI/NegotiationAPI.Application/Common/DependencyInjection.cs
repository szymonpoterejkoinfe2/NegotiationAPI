using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NegotiationAPI.Application.Common.Behaviors.ValidationBehaviors;
using System.Reflection;

namespace NegotiationAPI.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(new MediatRServiceConfiguration().RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
