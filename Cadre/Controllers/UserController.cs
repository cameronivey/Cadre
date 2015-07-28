using Cadre.DataAccessLayer;
using Cadre.Domain.Models;
using Cadre.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Cadre.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IPostDatabase database;

        public UserController(IPostDatabase database)
        {
            this.database = database;
        }

        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User() { Email = model.Email, Name = model.Name, Password = model.Password };

            database.Add(user);

            database.CommitAsync<User>();

            return Ok(user);
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            var users = database.Get<User>();

            if (users == null)
            {
                return BadRequest();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("remove/{id:int}")]
        public IHttpActionResult Remove(int id)
        {
            var user = database.Get<User>().SingleOrDefault(u => u.Id == id);

            database.Remove(user);

            database.CommitAsync<User>();

            return Ok();
        }

        [HttpGet]
        [Route("getuserinfo")]
        public IHttpActionResult GetUserInfo()
        {
            var currentUser = database.Get<User>().SingleOrDefault(u => u.Email == HttpContext.Current.User.Identity.Name);
            var viewModel = new UserInfoViewModel(currentUser.Name);

            if (viewModel == null)
            {
                return BadRequest();
            }

            return Ok(viewModel);
        }
    }
}
