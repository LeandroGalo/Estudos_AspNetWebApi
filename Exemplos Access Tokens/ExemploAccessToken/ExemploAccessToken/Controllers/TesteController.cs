using System.Web.Http;

namespace ExemploAccessToken.Controllers
{
    public class TesteController : ApiController
    {
        [Authorize]
        public bool Get()
        {
            return true;
        }
    }
}
