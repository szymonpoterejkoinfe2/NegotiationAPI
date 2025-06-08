using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.ChangeNegotiationStatusCommand
{
    public class ChangeNegotiationStatusCommandHandler : IRequestHandler<ChangeNegotiationStatusCommand, ErrorOr<NegotiationAPI.Domain.Entities.Negotiation>>
    {
        private readonly INegotiationRepository _negotiationRepository;

        public ChangeNegotiationStatusCommandHandler(INegotiationRepository negotiationRepository)
        {
            _negotiationRepository = negotiationRepository;
        }

        public async Task<ErrorOr<Domain.Entities.Negotiation>> Handle(ChangeNegotiationStatusCommand command, CancellationToken cancellationToken)
        {
            var negotiationId = command.NegotiationId;
            var status = command.Status;

            var negotiation = _negotiationRepository.GetNegotiationById(negotiationId);

            if (negotiation is null)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }


            var newStatus = _negotiationRepository.ChangeNegotiationStatus(negotiationId,status);

            if (newStatus is null)
            {
                return Errors.Negotiation.FailedToChangeStatus;
            }

            return newStatus; 
        }
    }
}
