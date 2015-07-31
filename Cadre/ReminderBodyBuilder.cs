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

            builder.Append("<h2>Cadre Email System Reminder</h2><br />");

            return builder.ToString();
        }
    }
}