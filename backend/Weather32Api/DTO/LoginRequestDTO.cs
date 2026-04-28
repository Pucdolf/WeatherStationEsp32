using System.ComponentModel.DataAnnotations;

namespace Weather32Api.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public required string Password { get; set; }
    }
}
