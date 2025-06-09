using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.DeleteAttemptCommand
{
    public class DeleteNegotiationAttemptCommandHandler : IRequestHandler<DeleteAttemptCommand, ErrorOr<Success>>
    {
        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;

        public DeleteNegotiationAttemptCommandHandler(INegotiationAttemptRepository negotiationAttemptRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteAttemptCommand command, CancellationToken cancellationToken)
        {
            var attemptId = command.AttemptId;

            var attemptToDelete = _negotiationAttemptRepository.GetNegotiationAttemptById(attemptId);

            if (attemptToDelete is null)
            {
                return Errors.NegotiationAttempt.NoAttemptWithGivenId;
            }

            var isDeleted = _negotiationAttemptRepository.DeleteNegotiationAttempt(attemptId);

            if (!isDeleted)
            { 
                return Errors.NegotiationAttempt.ErrorWhileTryingToDelete;
            }

            return new ErrorOr<Success>();
        }
    }
}
