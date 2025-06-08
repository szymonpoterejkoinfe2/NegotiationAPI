using Mapster;
using NegotiationAPI.Application.Services.Authentication;
using NegotiationAPI.Contracts.Authentication;
using NegotiationAPI.Contracts.Negotiation;
using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Api.Common.Mappings
{
    public class NegotiationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Negotiation, ChangeNegotiationStateResponse>()
                .Map(des => des.AttemptsCount, src => src.Attempts.Count);
               
        }
    }
}
