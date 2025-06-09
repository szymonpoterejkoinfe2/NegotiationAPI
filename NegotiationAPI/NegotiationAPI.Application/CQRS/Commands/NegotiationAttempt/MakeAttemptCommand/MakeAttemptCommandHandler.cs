using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.MakeAttemptCommand
{
    public class MakeAttemptCommandHandler : IRequestHandler<MakeAttemptCommand, ErrorOr<Guid>>
    {
        private readonly INegotiationAttemptRepository _negotiationAttemptRepository;
        private readonly INegotiationRepository _negotiationRepository;


        public MakeAttemptCommandHandler(INegotiationAttemptRepository negotiationAttemptRepository, INegotiationRepository negotiationRepository)
        {
            _negotiationAttemptRepository = negotiationAttemptRepository;
            _negotiationRepository = negotiationRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(MakeAttemptCommand command, CancellationToken cancellationToken)
        {
            var negotiatioId = command.NegotiationId;
            var proposedPrice = command.ProposedPrice;

            var negotiation = _negotiationRepository.GetNegotiationById(negotiatioId);

            if (negotiation is null)
            {
                return Errors.Negotiation.NoNegotiationWithGivenId;
            }

            if (!negotiation.CanProposeNewPrice())
            {
                return Errors.Negotiation.NoAttemptsLeft;
            }

            var negotiationAttempt = new Domain.Entities.NegotiationAttempt()
            {
                ProposedPrice = proposedPrice
            };
            var attemptID = _negotiationAttemptRepository.AddNegotiationAttempt(negotiationAttempt);

            negotiation.Attempts.Add(negotiationAttempt);
            negotiation.Status = Domain.Enums.NegotiationStatus.Waiting;

            var isUpdated = _negotiationRepository.UpdateNegotiation(negotiation);
            
            if (!isUpdated) 
            {
                return Errors.Negotiation.FailedToUpdate;
            }

            return attemptID;
        }
    }
}
