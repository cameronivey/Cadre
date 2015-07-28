using Cadre.Domain.Models;
using Cadre.DataAccessLayer;
using System.Web.Http;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Cadre.ViewModels;

namespace Cadre.Controllers
{
    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        private readonly IPostDatabase database;

        public PostController(IPostDatabase database)
        {
            this.database = database;
        }

        [HttpPost]
        [Route("addrequest")]
        public IHttpActionResult AddRequest([FromBody]AddPostViewModel postViewModel)
        {
            if (postViewModel == null)
            {
                return BadRequest();
            }
          
            var newPost = new Request()
            {
                Submitter = database.Get<User>().SingleOrDefault(u => u.Email == HttpContext.Current.User.Identity.Name),
                Summary = postViewModel.Summary,
                Details = postViewModel.Details
            };

            database.Add(newPost);
            database.CommitAsync<Post>();

            return Ok(newPost);
        }


        [HttpPost]
        [Route("addannouncement")]
        public IHttpActionResult AddAnnouncement([FromBody]AddPostViewModel postViewModel)
        {
            if (postViewModel == null)
            {
                return BadRequest();
            }

            var newPost = new Announcement()
            {
                Submitter = database.Get<User>().SingleOrDefault(u => u.Email == HttpContext.Current.User.Identity.Name),
                Summary = postViewModel.Summary,
                Details = postViewModel.Details
            };

            database.Add(newPost);
            database.CommitAsync<Post>();

            return Ok(newPost);
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            var viewModels = new List<PostViewModel>();
            var posts = database.Get<Post>();

            if (posts == null)
            {
                return BadRequest();
            }

            viewModels.AddRange(posts.Select(post => new PostViewModel()
            {
                Id = post.Id,
                SubmitterName = post.Submitter.Name,
                SubmitterEmail = post.Submitter.Email,
                Summary = post.Summary,
                Details = post.Details
            }));

            foreach(var viewModel in viewModels)
            {
                viewModel.EmailText = GetById(viewModel.Id).GetEmailText();
            }

            return Ok(viewModels);
        }

        /*[HttpGet]
        [Route("removeallnulluserposts/")]
        public IHttpActionResult RemoveAllNullUserPosts()
        {
            var posts = database.Get<Post>();

            foreach (var post in posts)
            {
                if (post.Submitter == null)
                {
                    database.Remove(post);
                }
            }

            database.CommitAsync();

            return Ok();
        }*/

        [HttpGet]
        [Route("remove/{id}:int")]
        public IHttpActionResult Remove(int id)
        {
            var post = GetById(id);

            database.Remove(post);

            database.CommitAsync<Post>();

            return Ok();
        }

        private Post GetById(int id)
        {
            return database.Get<Post>().SingleOrDefault(post => post.Id == id);
        }

    }
}
