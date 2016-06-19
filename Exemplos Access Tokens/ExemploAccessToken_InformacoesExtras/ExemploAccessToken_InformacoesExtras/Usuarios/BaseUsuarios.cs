using System.Collections.Generic;

namespace ExemploAccessToken_InformacoesExtras.Usuarios
{
    // simulacao de acesso a uma base de usuarios
    public static class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario { Nome = "Fulano", Senha = "1234",
                    Funcoes = new string[] { Funcao.UsuarioNormal } },
                new Usuario { Nome = "Beltrano", Senha = "5678",
                    Funcoes = new string[] { Funcao.Gerente } },
                new Usuario { Nome = "Sicrano", Senha = "0912",
                    Funcoes = new string[] { Funcao.Administrador,
                                                Funcao.Gerente } },
            };
        }
    }
}