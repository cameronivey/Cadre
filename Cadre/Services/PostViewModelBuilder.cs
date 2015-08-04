using Cadre.DataAccessLayer;
using Cadre.Domain.Models;
using Cadre.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Cadre.Services
{
    public class PostViewModelBuilder
    {
        private readonly IPostDatabase database;

        public PostViewModelBuilder(IPostDatabase database)
        {
            this.database = database;
        }

        public List<PostViewModel> Build(IQueryable<Post> posts)
        {
            var viewModels = new List<PostViewModel>();

            viewModels.AddRange(posts.Select(post => new PostViewModel()
            {
                Id = post.Id,
                TimeSubmitted = post.TimeSubmitted,
                SubmitterName = post.Submitter.Name,
                SubmitterEmail = post.Submitter.Email,
                Summary = post.Summary,
                Details = post.Details
            }));

            foreach (var viewModel in viewModels)
            {
                viewModel.EmailText = database.GetSingleById<Post>(viewModel.Id).GetEmailText();
            }

            return viewModels;
        }
    }
}