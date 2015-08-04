using Cadre.Domain.Models;
using Cadre.DataAccessLayer;
using System.Web.Http;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Cadre.ViewModels;
using System;
using Cadre.Services;

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
                Details = postViewModel.Details,
                TimeSubmitted = DateTime.Now
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
                Details = postViewModel.Details,
                TimeSubmitted = DateTime.Now
            };

            database.Add(newPost);
            database.CommitAsync<Post>();

            return Ok(newPost);
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            var posts = database.Get<Post>();

            if (posts == null)
            {
                return BadRequest();
            }

            var builder = new PostViewModelBuilder(database);
            var viewModels = builder.Build(posts);

            return Ok(viewModels);
        }

        [HttpGet]
        [Route("getallinlastweek")]
        public IHttpActionResult GetAllInLastWeek()
        {
            var oneWeek = new TimeSpan(7, 0, 0, 0);
            var bouttAWeekAgo = DateTime.Now.Subtract(oneWeek);
            var posts = database.Get<Post>().Where(post => post.TimeSubmitted >= bouttAWeekAgo);

            if (posts == null)
            {
                return BadRequest();
            }

            var builder = new PostViewModelBuilder(database);
            var viewModels = builder.Build(posts);

            return Ok(viewModels);
        }

        [HttpGet]
        [Route("getallinlastmonth")]
        public IHttpActionResult GetAllInLastMonth()
        {
            var oneMonth = new TimeSpan(30, 0, 0, 0);
            var bouttAMonthAgo = DateTime.Now.Subtract(oneMonth);
            var posts = database.Get<Post>().Where(post => post.TimeSubmitted >= bouttAMonthAgo);

            if (posts == null)
            {
                return BadRequest();
            }

            var builder = new PostViewModelBuilder(database);
            var viewModels = builder.Build(posts);

            return Ok(viewModels);
        }

        [HttpGet]
        [Route("getpost/{id:int}")]
        public IHttpActionResult GetPost(int id)
        {
            var post = database.Get<Post>().Where(p => p.Id == id);

            if (post == null)
            {
                return BadRequest();
            }

            var viewModel = (post.Select(p => new PostViewModel()
            {
                Id = p.Id,
                TimeSubmitted = p.TimeSubmitted,
                SubmitterName = p.Submitter.Name,
                SubmitterEmail = p.Submitter.Email,
                Summary = p.Summary,
                Details = p.Details
            }));

            return Ok(viewModel);
        }

        [HttpGet]
        [Route("getuserposts/{id:int}")]
        public IHttpActionResult GetUserPosts(int id)
        {
            var posts = database.Get<Post>().Where(post => post.Submitter.Id == id);

            if (posts == null)
            {
                return BadRequest();
            }

            var viewModels = new PostViewModelBuilder(database);

            viewModels.Build(posts);
            
            return Ok(posts);
        }

        [HttpGet]
        [Route("remove/{id:int}")]
        public IHttpActionResult Remove(int id)
        {
            var post = database.GetSingleById<Post>(id);

            database.Remove(post);

            database.CommitAsync<Post>();

            return Ok();
        }
    }
}
