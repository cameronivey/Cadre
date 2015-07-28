using System.Web.Http;

namespace Cadre.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("login")]
        public IHttpActionResult LogIn()
        {
            return Ok();
        }
    }
}
