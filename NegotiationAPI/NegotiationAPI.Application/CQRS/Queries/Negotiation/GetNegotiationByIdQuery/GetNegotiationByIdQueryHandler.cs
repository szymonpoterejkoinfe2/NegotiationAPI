using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Queries.Negotiation.GetNegotiationById
{
    public class GetNegotiationByIdQueryHandler : IRequestHandler<GetNegotiationByIdQuery, ErrorOr<Domain.Entities.Negotiation>>
    {
        private readonly INegotiationRepository _negotiationRepository;

        public GetNegotiationByIdQueryHandler(INegotiationRepository negotiationRepository)
        {
            _negotiationRepository = negotiationRepository;
        }

        public async Task<ErrorOr<Domain.Entities.Negotiation>> Handle(GetNegotiationByIdQuery query, CancellationToken cancellationToken)
        {
            var negotiationId = query.NegotiationId;

            var negotiation = _negotiationRepository.GetNegotiationById(negotiationId);

            if (negotiation is null)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            return negotiation;    
        }
    }
}
