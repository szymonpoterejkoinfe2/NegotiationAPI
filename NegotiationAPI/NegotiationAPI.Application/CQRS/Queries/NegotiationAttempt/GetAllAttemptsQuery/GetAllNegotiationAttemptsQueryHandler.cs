using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAllAttemptsQuery
{
    public class GetAllNegotiationAttemptsQueryHandler : IRequestHandler<GetAllAttemptsQuery, ErrorOr<List<Domain.Entities.NegotiationAttempt>>>
    {

        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;

        public GetAllNegotiationAttemptsQueryHandler(INegotiationAttemptRepository negotiationAttemptRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
        }

        public async Task<ErrorOr<List<Domain.Entities.NegotiationAttempt>>> Handle(GetAllAttemptsQuery request, CancellationToken cancellationToken)
        {
            var negotiationAttempts = _negotiationAttemptRepository.GetAttempts().ToList();

            if (!negotiationAttempts.Any())
            {
                return Errors.NegotiationAttempt.NoAttemptsFound;
            }

            return negotiationAttempts;
        }
    }
}
