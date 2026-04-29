using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Weather32Api.Data;
using Weather32Api.DTO;
using Weather32Api.Services;

namespace Weather32Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {

                if (registrationRequestDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Registration data is required."));
                }

                if (await authService.IsEmailExistsAsync(registrationRequestDTO.Email))
                {
                    return Conflict(
                        ApiResponse<object>.Conflict(
                            $"User with email {registrationRequestDTO.Email} already exists."));
                }

                var user = await _authService.RegisterAsync(registrationRequestDTO);

                if (user == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Registration failed."));
                }

                //auth
                var response = ApiResponse<UserDTO>.CreatedAt(user, "User registered successfully.");
                return CreatedAtAction(nameof(Register), new { id = user.Id }, response);
            }
            catch (Exception e)
            {
                var errorResponse =
                    ApiResponse<object>.Error(500, "An unexpected error occurred during registration:", e.Message);
                return StatusCode(500, errorResponse);
            }
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {

                if (loginRequestDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Login data is required."));
                }

                var loginResponse = await _authService.LoginAsync(loginRequestDTO);

                if (loginResponse == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Login failed."));
                }

                //auth
                var response = ApiResponse<LoginResponseDTO>.Ok(loginResponse, "User logged successfully.");
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse =
                    ApiResponse<object>.Error(500, "An unexpected error occurred during login:", e.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
