namespace Cadre.Domain.Models
{
    public class Announcement : Post
    {
        public override string GetEmailText()
        {
            return "offering";
        }
    }
}
