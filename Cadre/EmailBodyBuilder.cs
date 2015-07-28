using Cadre.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace Cadre {

    public class EmailBodyBuilder : IEmailBodyBuilder
    {
        public string Build(List<PostViewModel> viewModels)
        {
            var builder = new StringBuilder();
            var count = 1;

            builder.Append("<h2>Cadre Email System</h2><br />");

            builder.Append("<h4>Summary</h4><br />");
            foreach (PostViewModel post in viewModels)
            {
                builder.Append("<span><b>" + count + ". " + post.SubmitterName +
                    "(" + post.SubmitterEmail + ")</b> is " + post.EmailText + " " +
                    post.Summary + "</span><br />"
                );
                count++;
            }

            builder.Append("<h4>Details</h4>");
            foreach (PostViewModel post in viewModels)
            {
                builder.Append("<span><b>" + post.SubmitterName + " is " + post.EmailText + " </b>" +
                    post.Summary + "</span><br />" + "<span>" + post.Details + "</span><br />" +
                    "<a href='mailto:" + post.SubmitterEmail + "?subject=Reply'>Reply</a><br /><br />"
                );
            }

            return builder.ToString();
        }
    }
}