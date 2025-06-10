using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Notification;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.MakeAttemptCommand
{
    public class MakeAttemptCommandHandler : IRequestHandler<MakeAttemptCommand, ErrorOr<Guid>>
    {
        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;
        private readonly INegotiationRepository _negotiationRepository;
        private readonly INotificationService _notificationService;
        private readonly IEmployeeRepository _employeeRepository;

        public MakeAttemptCommandHandler(INegotiationAttemptRepository negotiationAttemptRepository, INegotiationRepository negotiationRepository, INotificationService notificationService, IEmployeeRepository employeeRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
            _negotiationRepository = negotiationRepository;
            _notificationService = notificationService;
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(MakeAttemptCommand command, CancellationToken cancellationToken)
        {
            var negotiatioId = command.NegotiationId;
            var proposedPrice = command.ProposedPrice;

            var negotiation = _negotiationRepository.GetNegotiationById(negotiatioId);

            if (negotiation is null)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            if (!negotiation.CanProposeNewPrice())
            {
                return Errors.Negotiation.NoAttemptsLeft;
            }

            var negotiationAttempt = new Domain.Entities.NegotiationAttempt()
            {
                ProposedPrice = proposedPrice
            };
            var attemptID = _negotiationAttemptRepository.AddNegotiationAttempt(negotiationAttempt);

            negotiation.Attempts.Add(negotiationAttempt);
            negotiation.Status = Domain.Enums.NegotiationStatus.Waiting;

            var isUpdated = _negotiationRepository.UpdateNegotiation(negotiation);
            
            if (!isUpdated) 
            {
                return Errors.Negotiation.FailedToUpdate;
            }

            await _notificationService.NotifyEmployeeAsync(_employeeRepository.GetRandomEmployee()?.Id.ToString() ?? "No employees", negotiatioId);


            return attemptID;
        }
    }
}
