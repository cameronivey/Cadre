using Cadre.Domain.Models;
using System;

namespace Cadre.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {

        }

        public PostViewModel(Post post)
        {
            Id = post.Id;
            TimeSubmitted = post.TimeSubmitted;
            EmailText = post.GetEmailText();
            SubmitterName = post.Submitter.Name;
            SubmitterEmail = post.Submitter.Email;
            Summary = post.Summary;
            Details = post.Details;

        }

        public int Id { get; set; }

        public DateTime TimeSubmitted { get; set; }
        
        public string EmailText { get; set; }

        public string SubmitterName { get; set; }

        public string SubmitterEmail { get; set; }

        public string Summary { get; set; }

        public string Details { get; set; }
    }
}