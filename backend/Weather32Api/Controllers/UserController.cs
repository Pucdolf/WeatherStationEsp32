using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.Models;
using Weather32Api.Models.DTO;

namespace Weather32Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            var dtoResponseUser = _mapper.Map<List<UserDTO>>(users);
            var response = ApiResponse<IEnumerable<UserDTO>>.Ok(dtoResponseUser, "Users retrieved successfully.");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<UserDTO>>> GetUserById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound(ApiResponse<object>.NotFound("User ID must be greater than 0."));

                    //return new ApiResponse<UserDTO>()
                    //{
                    //    StatusCode = 400,
                    //    Errors = "User ID must be greater than 0.",
                    //    Success = false,
                    //    Message = "Bad Request",
                    //};

                    //return BadRequest("User ID must be greater than 0.");
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"User with ID {id} was not found."));
                    //return NotFound($"User with ID {id} was not found.");
                }


                return Ok(ApiResponse<UserDTO>.Ok(_mapper.Map<UserDTO>(user), "Records retrieved successfully."));
                //return new ApiResponse<UserDTO>()
                //{
                //    StatusCode = 400,
                //    Success = true,
                //    Message = "Records retrieved successfully.",
                //    Data = _mapper.Map<UserDTO>(user)

                //};
                //return Ok(_mapper.Map<UserDTO>(user));
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500, "An error occured while retrieving user: ", e.Message);
                return StatusCode(500, errorResponse);
                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    $"An error occured while retrieving user with ID {id}: {e.Message}.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateUser(UserCreateDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("User data is required."));
                    //return BadRequest("User data is required.");
                }

                var duplicatedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == userDTO.Username.ToLower());

                if (duplicatedUser != null)
                {
                    return Conflict(
                        ApiResponse<object>.Conflict($"A user with username: {userDTO.Username} already exists."));
                    //return Conflict($"A user with username: {userDTO.Username} already exists.");
                }

                User user = _mapper.Map<User>(userDTO);
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;


                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                var response = ApiResponse<UserDTO>.CreatedAt(_mapper.Map<UserDTO>(user), "User created successfully.");
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, response);
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500, "An error occured while creating user: ", e.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<UserDTO>>> UpdateUser(int id, UserUpdateDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("User data is required."));
                    //return BadRequest("User data is required.");
                }

                if (id != userDTO.Id)
                {
                    return BadRequest(
                        ApiResponse<object>.BadRequest("User ID in URl does not match User ID in request body."));
                    //return BadRequest("User ID in URL does not match User ID in request body.");
                }

                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (existingUser == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"User with ID {id} was not found."));
                    //return NotFound($"User with ID {id} was not found.");
                }

                var duplicatedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == userDTO.Username.ToLower() && u.Id != id);

                if (duplicatedUser != null)
                {
                    return Conflict(
                        ApiResponse<object>.Conflict($"A user with username: {userDTO.Username} already exists."));
                    //return Conflict($"A user with username: {userDTO.Username} already exists.");
                }

                _mapper.Map(userDTO, existingUser);
                existingUser.UpdatedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                var response = ApiResponse<UserDTO>.Ok(_mapper.Map<UserDTO>(existingUser), "User updated successfully.");
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500, $"An error occured while updating user with ID {id}: ", e.Message);
                return StatusCode(500, errorResponse);
                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    $"An error occured while updating the user: {e.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<object>>> DeleteUser(int id)
        {
            try
            {
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"User with ID {id} was not found."));
                    //return NotFound($"User with ID {id} was not found.");
                }

                _dbContext.Users.Remove(existingUser);
                await _dbContext.SaveChangesAsync();

                var response = ApiResponse<object>.NoContent("User deleted succesfully.");
                return Ok(response);
                //return NoContent();
            }
            catch (Exception e)
            {
                var errorResponse =
                    ApiResponse<object>.Error(500, "An error occured while deleting the user: ", e.Message);
                return StatusCode(500, errorResponse);

            }
        }
    }
}
