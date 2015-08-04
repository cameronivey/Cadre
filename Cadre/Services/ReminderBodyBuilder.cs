using Cadre.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace Cadre
{
    public class ReminderBodyBuilder : IEmailBodyBuilder
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
                    "<tr><td style='margin-left: 5 px'><h2>Cadre Email System Reminder</h2></td></tr>" +
                    "<tr bgcolor='000099'><td style='padding: 0 0 0 5px'>" + 
                    "<font color='FFFFFF'><b>WHAT DO YOU NEED THIS WEEK?</b></font></td></tr>" +
                    "<tr><td><a href='localhost:50123/Views/AddPost.html'>Add A Post</a></td></tr>" +


                "</table>");

            return builder.ToString();
        }
    }
}