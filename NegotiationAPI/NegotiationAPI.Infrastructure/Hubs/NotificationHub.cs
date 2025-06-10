using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace NegotiationAPI.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                
                var employeeId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrWhiteSpace(employeeId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"employee-{employeeId}");
                }
            }

            await base.OnConnectedAsync();
        }

        public async Task SubscribeToNegotiation(string negotiationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"negotiation-{negotiationId}");
        }
    }
}
