using Mapster;
using NegotiationAPI.Application.Services.Authentication;
using NegotiationAPI.Contracts.Authentication;

namespace NegotiationAPI.Api.Common.Mappings
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(des => des.Token, src => src.Token)
                .Map(des => des, src => src.Employee);
        }
    }

}
