using System;
using System.ComponentModel.DataAnnotations;

namespace Cadre.Domain.Models
{
    public abstract class Post : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime TimeSubmitted { get; set; }

        public User Submitter { get; set; }

        public string Summary { get; set; }

        public string Details { get; set; }

        public abstract string GetEmailText();
    }
}
