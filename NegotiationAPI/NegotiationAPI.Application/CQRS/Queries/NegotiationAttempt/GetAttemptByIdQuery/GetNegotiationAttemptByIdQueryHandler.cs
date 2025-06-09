using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAttemptByIdQuery
{
    public class GetNegotiationAttemptByIdQueryHandler : IRequestHandler<GetAttemptByIdQuery, ErrorOr<Domain.Entities.NegotiationAttempt?>>
    {
        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;

        public GetNegotiationAttemptByIdQueryHandler(INegotiationAttemptRepository negotiationAttemptRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
        }

        public async Task<ErrorOr<Domain.Entities.NegotiationAttempt?>> Handle(GetAttemptByIdQuery query, CancellationToken cancellationToken)
        {
            var attemptId = query.AttemptId;
            var attempt = _negotiationAttemptRepository.GetNegotiationAttemptById(attemptId);

            if (attempt is null)
            {
                return Errors.NegotiationAttempt.NoAttemptWithGivenId;
            }

            return attempt;
        }
    }
}
