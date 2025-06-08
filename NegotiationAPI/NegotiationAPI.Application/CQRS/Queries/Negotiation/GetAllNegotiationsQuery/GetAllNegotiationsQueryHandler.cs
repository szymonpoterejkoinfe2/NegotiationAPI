using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Queries.Negotiation.GetAllNegotiationsQuery
{
    public class GetAllNegotiationsQueryHandler : IRequestHandler<GetAllNegotiationsQuery, ErrorOr<List<NegotiationAPI.Domain.Entities.Negotiation>>>
    {
        private readonly INegotiationRepository _negotiationRepository;

        public GetAllNegotiationsQueryHandler(INegotiationRepository negotiationRepository)
        {
            _negotiationRepository = negotiationRepository;
        }

        public async Task<ErrorOr<List<Domain.Entities.Negotiation>>> Handle(GetAllNegotiationsQuery query, CancellationToken cancellationToken)
        {
            var negotiations = _negotiationRepository.GetAllNegotiations().ToList();

            if (!negotiations.Any())
            {
                return Errors.Negotiation.NoNegotiations;
            }

            return negotiations;
        }
    }
}
