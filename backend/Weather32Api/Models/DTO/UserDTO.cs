using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class UserDTO
    {

        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;

        //public string Password { get; set; }

        public string Role { get; set; } = default!;

    }
}
