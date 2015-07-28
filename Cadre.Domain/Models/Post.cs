using System.ComponentModel.DataAnnotations;

namespace Cadre.Domain.Models
{
    public abstract class Post
    {
        [Key]
        public int Id { get; set; }

        public User Submitter { get; set; }

        public string Summary { get; set; }

        public string Details { get; set; }

        public abstract string GetEmailText();
    }
}
