using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.Models;
using Weather32Api.Models.DTO;

namespace Weather32Api.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class WeatherStationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WeatherStationController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WeatherStation>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WeatherStationDTO>>> GetWeatherStations()
        {
            var weatherStations = await _dbContext.WeatherStations.ToListAsync();
            var dtoResponseWeatherStation = _mapper.Map<List<WeatherStationDTO>>(weatherStations);
            return Ok(ApiResponse<IEnumerable<WeatherStationDTO>>.Ok(dtoResponseWeatherStation, "Weather Stations retrieved successfully."));
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<WeatherStationDTO>>> GetWeatherStationById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound(ApiResponse<object>.NotFound("Weather Station ID must be greater than 0."));
                }
                var weatherStation = await _dbContext.WeatherStations.FirstOrDefaultAsync(u => u.Id == id);

                if (weatherStation == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Weather Station with ID {id} was not found."));
                }

                return Ok(ApiResponse<WeatherStationDTO>.Ok(_mapper.Map<WeatherStationDTO>(weatherStation),
                    "Records retrieved successfully."));
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500,
                    "An unexpected error occurred while retrieving Weather Station.", e.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
