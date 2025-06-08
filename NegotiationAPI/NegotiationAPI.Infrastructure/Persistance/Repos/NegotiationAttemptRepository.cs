using MapsterMapper;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;
using NegotiationAPI.Infrastructure.Persistance.Entities;

namespace NegotiationAPI.Infrastructure.Persistance.Repos
{
    public class NegotiationAttemptRepository : INegotiationAttemptRepository
    {
        private static readonly List<NegotiationAttemptEntity> _negotiationAttemptEntities = new() 
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
                    },
            new NegotiationAttemptEntity
            {
                Id = Guid.Parse("32222222-3333-2222-2222-222222222222"),
                ProposedPrice = 250,
                ProposedAt = DateTime.UtcNow.AddDays(-2),
                Result = NegotiationResult.Accepted
            }
        };

        private readonly IMapper _mapper;

        public NegotiationAttemptRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Guid AddNegotiationAttempt(NegotiationAttempt negotiationAttempt)
        {
            var negotiationAttemptEntity = _mapper.Map<NegotiationAttemptEntity>(negotiationAttempt);
            _negotiationAttemptEntities.Add(negotiationAttemptEntity);
            return negotiationAttemptEntity.Id;
        }

        public bool DeleteNegotiationAttempt(Guid negotiationAttemptId)
        {
            int index = _negotiationAttemptEntities.FindIndex(a => a.Id == negotiationAttemptId);

            if (index < 0)
            {
                return false;
            }

            _negotiationAttemptEntities.RemoveAt(index);

            return true;
        }

        public IEnumerable<NegotiationAttempt> GetAttempts()
        {
            var negotiationAttempts = _mapper.Map<IEnumerable<NegotiationAttempt>>(_negotiationAttemptEntities);

            return negotiationAttempts ?? Enumerable.Empty<NegotiationAttempt>();
        }

        public NegotiationAttempt? GetNegotiationAttemptById(Guid negotiationAttemptId)
        {
            var negotiationAttemptEntity = _negotiationAttemptEntities.Where(n => n.Id == negotiationAttemptId).FirstOrDefault();

            return _mapper.Map<NegotiationAttempt>(negotiationAttemptEntity);
        }

        public IEnumerable<NegotiationAttempt> GetPendingAttempts()
        {
            var pendingNegotiationAttempts = _mapper.Map<IEnumerable<NegotiationAttempt>>(_negotiationAttemptEntities.Where(n => n.Result == NegotiationResult.AwaitingResponse));

            return _mapper.Map<IEnumerable<NegotiationAttempt>>(pendingNegotiationAttempts);
        }

        public NegotiationAttempt? UpdateNegotiationAttemptResultState(Guid negotiationAttemptId, NegotiationResult result)
        {
            int index = _negotiationAttemptEntities.FindIndex(a => a.Id == negotiationAttemptId);

            if (index < 0)
            {
                return null;
            }

            _negotiationAttemptEntities[index].Result = result;

            return _mapper.Map<NegotiationAttempt>(_negotiationAttemptEntities[index]);
        }
    }
}
