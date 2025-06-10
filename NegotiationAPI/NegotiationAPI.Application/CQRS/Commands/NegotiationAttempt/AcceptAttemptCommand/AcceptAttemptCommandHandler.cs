using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Notification;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.AcceptAttemptCommand
{
    public class AcceptAttemptCommandHandler : IRequestHandler<AcceptAttemptCommand, ErrorOr<Success>>
    {
        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;
        private readonly INegotiationRepository _negotiateRepository;
        private readonly INotificationService _notificationService;

        public AcceptAttemptCommandHandler(INegotiationAttemptRepository negotiationAttemptRepository, INegotiationRepository negotiateRepository, INotificationService notificationService)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
            _negotiateRepository = negotiateRepository;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<Success>> Handle(AcceptAttemptCommand command, CancellationToken cancellationToken)
        {
            var attemptId = command.AttemptId;

            var negotiation = _negotiateRepository.GetNegotiationByAttemptId(attemptId);

            if (negotiation is null || negotiation.Status == Domain.Enums.NegotiationStatus.Cancelled)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            var updatedAttempt = _negotiationAttemptRepository.UpdateNegotiationAttemptResultState(attemptId, Domain.Enums.NegotiationResult.Accepted);
            
            var updatedNegotiation = _negotiateRepository.ChangeNegotiationStatus(negotiation.Id, Domain.Enums.NegotiationStatus.Accepted);

            //Mimics updates of related entities in db
            if (updatedAttempt is not null && updatedNegotiation is not null)
            {
                int id =  updatedNegotiation.Attempts.FindIndex(a => a.Id == updatedAttempt.Id);
                if (id >= 0)
                {
                    updatedNegotiation.Attempts[id] = updatedAttempt;

                    _negotiateRepository.UpdateNegotiation(updatedNegotiation);
                }
            }

            await _notificationService.NotifyClientAsync(negotiationId: negotiation.Id, Domain.Enums.NegotiationStatus.Accepted.ToString());

            return new ErrorOr<Success>();
        }
    }
}
