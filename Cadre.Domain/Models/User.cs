using System.ComponentModel.DataAnnotations;

namespace Cadre.Domain.Models
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
