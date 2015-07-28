namespace Cadre.Domain.Models
{
    public class Request : Post
    {
        public override string GetEmailText()
        {
            return "on a mission to";
        }
    }
}
