﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExemploAccessToken_Roles.Usuarios;
using Microsoft.Owin.Security.OAuth;

namespace ExemploAccessToken_Roles
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
    }
}