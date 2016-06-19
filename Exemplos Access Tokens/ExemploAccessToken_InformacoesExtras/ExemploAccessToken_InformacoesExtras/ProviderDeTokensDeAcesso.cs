using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExemploAccessToken_InformacoesExtras.Usuarios;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;

namespace ExemploAccessToken_InformacoesExtras
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication
            (OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials
            (OAuthGrantResourceOwnerCredentialsContext context)
        {
            // encontrando o usuário
            var usuario = BaseUsuarios
                .Usuarios()
                .FirstOrDefault(x => x.Nome == context.UserName 
                                && x.Senha == context.Password);

            // cancelando a emissão do token se o usuário não for encontrado
            if (usuario == null)
            {
                context.SetError("invalid_grant", 
                    "Usuário não encontrado um senha incorreta.");
                return;
            }

            // emitindo o token com informacoes extras
            // se o usuário existe
            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);

            foreach (var funcao in usuario.Funcoes)
                identidadeUsuario.AddClaim(new Claim(ClaimTypes.Role, funcao));
            
            context.Validated(identidadeUsuario);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            // repassando propriedades do token, mas de forma aberta
            foreach (var item in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(item.Key, item.Value);
            }

            // repassando claims que estão no token, mas de forma aberta
            var claims = context.Identity.Claims
                .GroupBy(x => x.Type)
                .Select(y => new { Claim = y.Key, Values = y.Select(z => z.Value).ToArray() });

            foreach (var claim in claims)
            { 
                context.AdditionalResponseParameters.Add(claim.Claim, JsonConvert.SerializeObject(claim.Values));
            }            

            // repassando informacões fixas
            context.AdditionalResponseParameters.Add("info1", "valor");
            context.AdditionalResponseParameters.Add("info2", 1);

            return base.TokenEndpoint(context);
        }

    }
}