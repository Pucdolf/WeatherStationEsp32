using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Weather32Api.Data;
using Weather32Api.DTO;

namespace Weather32Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
                   
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            //auth
            var response = ApiResponse<UserDTO>.Ok(null, "User created successfully.");
            return Ok(response);
        }
        
    }
}
