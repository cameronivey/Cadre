using Cadre.DataAccessLayer;
using Cadre.Domain.Models;
using Cadre.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Http;

namespace Cadre.Controllers
{
    [RoutePrefix("api/Email")]
    public class EmailController : ApiController
    {
        //private readonly IEmailBodyBuilder builder;
        private readonly IPostDatabase database;

        public EmailController(IPostDatabase database)
        {
            this.database = database;
        }
        
        [Route("SendEmail/{isDigest:bool}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult SendEmail(bool isDigest)
        {
            var addresses = new List<string>();
            addresses.Add("cameron.ivey@excella.com");
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("cameron.excella@gmail.com");

            foreach (var address in addresses)
            {
                mail.To.Add(address);
            }

            IEmailBodyBuilder builder;
            if (isDigest)
            {
                mail.Subject = "Cadre Digest Email";
                builder = new DigestBodyBuilder();
            }
            else
            {
                mail.Subject = "Cadre Reminder Email";
                builder = new ReminderBodyBuilder();
            }

            mail.Body = BuildEmailBody(builder);
            mail.IsBodyHtml = true;

            SendSmtpEmail(mail);

            return Ok(mail);
        }

        private string BuildEmailBody(IEmailBodyBuilder builder)
        {
            var viewModels = new List<PostViewModel>();
            var posts = database.Get<Post>();

            viewModels.AddRange(posts.Select(post => new PostViewModel()
            {
                Id = post.Id,
                SubmitterName = post.Submitter.Name,
                SubmitterEmail = post.Submitter.Email,
                Summary = post.Summary,
                Details = post.Details
            }));

            foreach (var viewModel in viewModels)
            {
                viewModel.EmailText = database.Get<Post>().SingleOrDefault(post => post.Id == viewModel.Id).GetEmailText();
            }

            return builder.Build(viewModels);
        }

        private void SendSmtpEmail(MailMessage mail)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("cameron.excella@gmail.com", "M@ryland15");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
