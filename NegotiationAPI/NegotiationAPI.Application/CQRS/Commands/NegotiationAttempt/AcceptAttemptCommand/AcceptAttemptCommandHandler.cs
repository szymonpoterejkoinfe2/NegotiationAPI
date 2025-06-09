using ErrorOr;
using MediatR;
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

        public AcceptAttemptCommandHandler(INegotiationAttemptRepository negotiationAttemptRepository, INegotiationRepository negotiateRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
            _negotiateRepository = negotiateRepository;
        }

        public async Task<ErrorOr<Success>> Handle(AcceptAttemptCommand command, CancellationToken cancellationToken)
        {
            var attemptId = command.AttemptId;

            var negotiation = _negotiateRepository.GetNegotiationByAttemptId(attemptId);

            if (negotiation is null || negotiation.Status == Domain.Enums.NegotiationStatus.Cancelled)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            _negotiationAttemptRepository.UpdateNegotiationAttemptResultState(attemptId, Domain.Enums.NegotiationResult.Accepted);
            _negotiateRepository.ChangeNegotiationStatus(negotiation.Id, Domain.Enums.NegotiationStatus.Accepted);

            return new ErrorOr<Success>();
        }
    }
}
