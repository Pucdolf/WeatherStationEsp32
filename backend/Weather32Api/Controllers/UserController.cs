using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.Models;

namespace Weather32Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

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
    }
}
