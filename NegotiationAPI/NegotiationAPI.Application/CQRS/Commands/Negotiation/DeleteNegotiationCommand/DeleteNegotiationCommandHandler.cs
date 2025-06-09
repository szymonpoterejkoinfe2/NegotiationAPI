using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.DeleteNegotiationCommand
{
    public class DeleteNegotiationCommandHandler : IRequestHandler<DeleteNegotiationCommand,ErrorOr<Success>>
    {
        private readonly INegotiationRepository _negotiationRepository;

        public DeleteNegotiationCommandHandler(INegotiationRepository negotiationRepository)
        {
            _negotiationRepository = negotiationRepository;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteNegotiationCommand command, CancellationToken cancellationToken)
        {
            var negotiationId = command.NegotiationId;

            var negotiation = _negotiationRepository.GetNegotiationById(negotiationId);

            if (negotiation is null)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            var isDeleted = _negotiationRepository.DeleteNegotiation(negotiationId);

            if (!isDeleted)
            {
                return Errors.Negotiation.FailedToDelete;
            }

            return new ErrorOr<Success>();
        }
    }
}
