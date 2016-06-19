using System.Web.Http;

namespace ExemploAccessToken_Roles.Controllers
{
    public class TesteController : ApiController
    {
        [Authorize(Roles = Usuarios.Funcao.Administrador)]
        public bool Get()
        {
            return true;
        }
    }
}