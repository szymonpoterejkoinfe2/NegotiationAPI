using Mapster;
using NegotiationAPI.Contracts.NegotiationAttempt;

namespace NegotiationAPI.Api.Common.Mappings
{
    public class AttemptMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, MakeAttemptResponse>()
                .Map(x => x.NewAttemptId, x => x);
        }
    }
}
