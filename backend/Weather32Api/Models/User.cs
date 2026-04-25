using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
