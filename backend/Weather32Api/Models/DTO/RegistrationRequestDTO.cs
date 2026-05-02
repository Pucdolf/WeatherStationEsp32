using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [MaxLength(50)]
        public string Role { get; set; } = "User";

    }
}
