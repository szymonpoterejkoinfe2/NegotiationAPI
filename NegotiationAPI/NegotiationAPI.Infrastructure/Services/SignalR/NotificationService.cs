using Microsoft.AspNetCore.SignalR;
using NegotiationAPI.Application.Common.Interfaces.Notification;
using NegotiationAPI.Infrastructure.Hubs;

namespace NegotiationAPI.Infrastructure.Services.SignalR
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyEmployeeAsync(string employeeId, Guid negotiationId)
        {
            await _hubContext.Clients
                .Group($"employee-{employeeId}")
                .SendAsync("NegotiationAssigned", new { negotiationId });
        }

        public async Task NotifyClientAsync(Guid negotiationId, string newStatus)
        {
            await _hubContext.Clients
                .Group($"negotiation-{negotiationId}")
                .SendAsync("NegotiationStatusChanged", new { negotiationId, newStatus });
        }
    }
}
