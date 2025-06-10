namespace NegotiationAPI.Application.Common.Interfaces.Notification
{
    public interface INotificationService
    {
        Task NotifyEmployeeAsync(string employeeId, Guid negotiationId);
        Task NotifyClientAsync(Guid negotiationId, string newStatus);
    }
}
