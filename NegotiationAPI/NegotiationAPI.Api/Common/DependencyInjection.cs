using Mapster;
using MapsterMapper;
using NegotiationAPI.Application.Common.Interfaces.Notification;
using NegotiationAPI.Infrastructure.Services.SignalR;
using System.Reflection;

namespace NegotiationAPI.Api.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddSignalR();
            services.AddScoped<INotificationService, NotificationService>();


            return services;
        }

    }
}
