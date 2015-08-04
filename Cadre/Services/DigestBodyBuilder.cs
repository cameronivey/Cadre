using Cadre.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace Cadre {

    public class DigestBodyBuilder : IEmailBodyBuilder
    {
        public string Build(List<PostViewModel> viewModels)
        {
            var builder = new StringBuilder();
            var count = 1;

            builder.Append(
                "<table align='center' cellpadding='0' width='600'>" +
                    "<tr>" +
                        "<td align='center' style='padding: 40px 0 30px 0'>" +
                            "<img src='cid:companylogo' alt='logo' style='display: block' />" +
                         "</td>" +
                    "</tr>" +
                    "<tr><td style='margin-left: 5 px'><h2>Cadre Email System Digest</h2></td></tr>" +
                    "<tr bgcolor='000099'><td style='padding: 0 0 0 5px'><font color='FFFFFF'><b>QUICK LOOK</b></font></td></tr>" +
                    "<tr>" +
                        "<td>" +
                            "<table align='left' cellpadding='0' style='margin-left: 10px'>");

            foreach (PostViewModel post in viewModels)
            {
                builder.Append("<tr><td><b>" + count + ". " + post.SubmitterName + "</b> is "
                    + post.EmailText + " " + post.Summary + "</td></tr>"
                );
                count++;
            }

            builder.Append("</table>" +
                         "</td>" +
                     "</tr>" +
                     "<tr bgcolor='000099'><td style='padding: 0 0 0 5px'><font color='FFFFFF'><b>REQUESTS<b></font></td></tr>" +
                     "<tr>" +
                        "<td>" +
                            "<table align='left' cellpadding='0' style='margin-left: 10px'>");

            foreach (PostViewModel post in viewModels)
            {
                builder.Append("<tr><td><b>" + post.SubmitterName + " is " + post.EmailText + " </b>" + post.Summary + "</td></tr>" + 
                    "<tr><td style='padding: 0 0 0 20px'>" + post.Details + "</td></tr>" +
                    "<tr><td style='padding: 0 0 30px 20px'><a href='mailto:" + post.SubmitterEmail + "?subject=Reply'>Reply</a></td></tr>"
                );
            }

            builder.Append("</table>");
 
            return builder.ToString();
        }
    }
}