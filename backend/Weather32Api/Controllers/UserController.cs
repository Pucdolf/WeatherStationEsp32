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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("User ID must be greater than 0.");
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound($"User with ID {id} was not found.");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while retrieving user with ID {id}: {e.Message}.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest("User data is required.");
                }

                User user = _mapper.Map<User>(userDTO);
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;


                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while creating user: {e.Message}.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UserUpdateDTO userDTO)
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
        public async Task<ActionResult<User>> DeleteUser(int id)
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
