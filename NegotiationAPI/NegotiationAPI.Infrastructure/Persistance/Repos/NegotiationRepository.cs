using MapsterMapper;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;
using NegotiationAPI.Infrastructure.Persistance.Entities;

namespace NegotiationAPI.Infrastructure.Persistance.Repos
{
    public class NegotiationRepository : INegotiationRepository
    {
        private static readonly List<NegotiationEntity> _negotiationEntities = new()
        {
            new NegotiationEntity
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Status = NegotiationStatus.Waiting,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                LastRejectedAt = DateTime.UtcNow.AddHours(-20),
                Attempts = new List<NegotiationAttemptEntity>
                {
                    new NegotiationAttemptEntity
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        ProposedPrice = 150,
                        ProposedAt = DateTime.UtcNow.AddDays(-1),
                        Result = NegotiationResult.Rejected
                    },
                    new NegotiationAttemptEntity
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        ProposedPrice = 170,
                        ProposedAt = DateTime.UtcNow.AddHours(-20),
                        Result = NegotiationResult.Rejected
                    }
                }
            },
            new NegotiationEntity
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Status = NegotiationStatus.Accepted,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                LastRejectedAt = null,
                Attempts = new List<NegotiationAttemptEntity>
                {
                    new NegotiationAttemptEntity
                    {
                        Id = Guid.Parse("32222222-3333-2222-2222-222222222222"),
                        ProposedPrice = 250,
                        ProposedAt = DateTime.UtcNow.AddDays(-2),
                        Result = NegotiationResult.Accepted
                    }
                }
            }
        };

        private readonly IMapper _mapper;

        public NegotiationRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Guid AddNegotiation(Negotiation negotiation)
        {
            var negotiationEntity = _mapper.Map<NegotiationEntity>(negotiation);
            _negotiationEntities.Add(negotiationEntity);
            
            return negotiationEntity.Id;
        }

        public bool DeleteNegotiation(Guid negotiationId)
        {
            int index = _negotiationEntities.FindIndex(n => n.Id == negotiationId);

            if (index < 0)
            {
                return false;
            }

            _negotiationEntities.RemoveAt(index);

            return true;
        }

        public IEnumerable<Negotiation> GetAllNegotiations()
        {
            var negotiations = _mapper.Map<IEnumerable<Negotiation>>(_negotiationEntities);

            return negotiations ?? Enumerable.Empty<Negotiation>();
        }

        public Negotiation? GetNegotiationById(Guid negotioationId)
        {
            var negotiationEntity = _negotiationEntities.Where(n => n.Id == negotioationId).FirstOrDefault();

            return _mapper.Map<Negotiation>(negotiationEntity);
        }

        public Negotiation? ChangeNegotiationStatus(Guid negotiationId, NegotiationStatus status)
        {
            int index = _negotiationEntities.FindIndex(n => n.Id == negotiationId);

            if (index < 0)
            {
                return null;
            }

            var negotiation = _negotiationEntities[index];

            if (status == NegotiationStatus.Rejected) 
            {
                negotiation.LastRejectedAt = DateTime.UtcNow;
            }

            negotiation.Status = status;
            _negotiationEntities[index] = negotiation;

            return _mapper.Map<Negotiation>(_negotiationEntities[index]);
        }

        public bool UpdateNegotiation(Negotiation negotiation)
        {
            int index = _negotiationEntities.FindIndex(n => n.Id == negotiation.Id);
            
            if (index < 0)
            {
                return false;
            }

            var negotiaitonEntity = _mapper.Map<NegotiationEntity>(negotiation);
            _negotiationEntities[index] = negotiaitonEntity;

            return true;
        }


        public Negotiation? GetNegotiationByAttemptId(Guid attemptId)
        {
            var negotiationEntity = _negotiationEntities.Where(n => n.Attempts.Any(a => a.Id == attemptId)).FirstOrDefault();

            var negotiation = _mapper.Map<Negotiation>(negotiationEntity);

            return negotiation;
        }

    }
}
