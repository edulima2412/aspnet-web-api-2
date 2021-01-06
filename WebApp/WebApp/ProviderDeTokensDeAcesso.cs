using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;

namespace WebApp
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthAuthorizationEndpointResponseContext context)
        {

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantClientCredentialsContext context)
        {

        }
    }
}