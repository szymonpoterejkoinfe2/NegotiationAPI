using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetPendingAttemptsQuery
{
    public class GetPendingNegotiationAttemptsQueryHandler : IRequestHandler<GetPendingAttemptsQuery, ErrorOr<List<Domain.Entities.NegotiationAttempt>>>
    {
        private readonly INegotiationAttemptRepository _negotiateAttemptRepository;

        public GetPendingNegotiationAttemptsQueryHandler(INegotiationAttemptRepository negotiateAttemptRepository)
        {
            _negotiateAttemptRepository = negotiateAttemptRepository;
        }

        public async Task<ErrorOr<List<Domain.Entities.NegotiationAttempt>>> Handle(GetPendingAttemptsQuery request, CancellationToken cancellationToken)
        {
            var negotiationAttempts = _negotiateAttemptRepository.GetPendingAttempts().ToList();

            if(!negotiationAttempts.Any())
            {
                return Errors.NegotiationAttempt.NoAttemptsFound;
            }

            return negotiationAttempts;
        }
    }
}
