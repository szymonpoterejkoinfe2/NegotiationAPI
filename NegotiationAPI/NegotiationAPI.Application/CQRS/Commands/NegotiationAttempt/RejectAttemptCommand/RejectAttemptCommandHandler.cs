using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.RejectAttemptCommand
{
    public class RejectAttemptCommandHandler : IRequestHandler<RejectAttemptCommand, ErrorOr<Success>>
    {
        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;
        private readonly INegotiationRepository _negotiationRepository;
        public RejectAttemptCommandHandler(INegotiationAttemptRepository negotiationAttemptRepository, INegotiationRepository negotiationRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
            _negotiationRepository = negotiationRepository;
        }

        public async Task<ErrorOr<Success>> Handle(RejectAttemptCommand command, CancellationToken cancellationToken)
        {
            var attemptId = command.AttemptId;

            var attempt = _negotiationAttemptRepository.GetNegotiationAttemptById(attemptId);

            if (attempt is null)
            {
                return Errors.NegotiationAttempt.NoAttemptWithGivenId;
            }

            var negotiation = _negotiationRepository.GetNegotiationByAttemptId(attemptId);

            if (negotiation is null) 
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            _negotiationAttemptRepository.UpdateNegotiationAttemptResultState(attemptId, Domain.Enums.NegotiationResult.Rejected);

            if (!negotiation.CanProposeNewPrice())
            {
                _negotiationRepository.ChangeNegotiationStatus(negotiation.Id, Domain.Enums.NegotiationStatus.Cancelled);
            }
            else {
                _negotiationRepository.ChangeNegotiationStatus(negotiation.Id, Domain.Enums.NegotiationStatus.Rejected);
            }

            return new ErrorOr<Success>();
        }
    }
}
