using System.ComponentModel.DataAnnotations;

namespace Weather32Api.DTO
{
    public class UserDTO
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
