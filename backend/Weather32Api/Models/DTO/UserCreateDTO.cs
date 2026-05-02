using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
