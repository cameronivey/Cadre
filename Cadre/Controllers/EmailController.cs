using Cadre.DataAccessLayer;
using Cadre.Domain.Models;
using Cadre.Services;
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
            
            var vmBuilder = new PostViewModelBuilder(database);

            mail.Body = builder.Build(vmBuilder.Build(database.Get<Post>()));
            
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                mail.Body, null, "text/html");

            LinkedResource logo = new LinkedResource(
                "C:/Users/Cameron/Documents/Visual Studio 2015/Projects/Cadre/Cadre/Content/logo_long.png");
            logo.ContentId = "companylogo";
            htmlView.LinkedResources.Add(logo);
            
            mail.AlternateViews.Add(htmlView);

            mail.IsBodyHtml = true;

            SendSmtpEmail(mail);

            return Ok();
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
