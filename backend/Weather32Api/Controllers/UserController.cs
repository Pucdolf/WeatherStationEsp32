using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.DTO;
using Weather32Api.Models;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            var dtoResponseUser = _mapper.Map<List<UserDTO>>(users);
            var response = ApiResponse<IEnumerable<UserDTO>>.Ok(dtoResponseUser, "Users retrieved successfully.");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
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


                return Ok(ApiResponse<UserDTO>.Ok(user, "Records retrieved successfully."));
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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while retrieving user with ID {id}: {e.Message}.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserCreateDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest("User data is required.");
                }

                var duplicatedVilla = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == userDTO.Username.ToLower());

                if (duplicatedVilla != null)
                {
                    return Conflict($"A user with username: {userDTO.Username} already exists.");
                }

                User user = _mapper.Map<User>(userDTO);
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;


                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, _mapper.Map<UserDTO>(user));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while creating user: {e.Message}.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserUpdateDTO>> UpdateUser(int id, UserUpdateDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest("User data is required.");
                }

                if (id != userDTO.Id)
                {
                    return BadRequest("User ID in URL does not match User ID in request body.");
                }

                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} was not found.");
                }

                var duplicatedVilla =
                    _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == userDTO.Username.ToLower() && u.Id != id);

                if (duplicatedVilla != null)
                {
                    return Conflict($"A user with username: {userDTO.Username} already exists.");
                }

                _mapper.Map(userDTO, existingUser);
                existingUser.UpdatedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while updating the user: {e.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} was not found.");
                }

                _dbContext.Users.Remove(existingUser);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while deleting the user: {e.Message}");
                throw;
            }
        }
    }
}
