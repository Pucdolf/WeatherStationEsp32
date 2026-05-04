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

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<WeatherStationDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<WeatherStationDTO>>> CreateWeatherStation(WeatherStationCreateDTO weatherStationDTO)
        {
            try
            {
                if (weatherStationDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather Station data is required."));
                }

                WeatherStation weatherStation = _mapper.Map<WeatherStation>(weatherStationDTO);

                // Temporary fix: Assigning to the first user in the system until JWT logic is implemented
                weatherStation.UserId = 1;

                await _dbContext.WeatherStations.AddAsync(weatherStation);
                await _dbContext.SaveChangesAsync();

                var response =
                    ApiResponse<WeatherStationDTO>.CreatedAt(_mapper.Map<WeatherStationDTO>(weatherStation), "Weather Station created successfully.");

                return CreatedAtAction(nameof(CreateWeatherStation), new { id = weatherStation.Id }, response);
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500,
                    "An unexpected error occurred while creating Weather Station.", e.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<WeatherStation>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<WeatherStationDTO>>> UpdateWeatherStation(int id, WeatherStationUpdateDTO weatherStationDTO)
        {
            try
            {
                if (weatherStationDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather Station data is required."));
                }

                var existingStation = await _dbContext.WeatherStations.FirstOrDefaultAsync(s => s.Id == id);

                if (existingStation == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Weather Station with ID {id} was not found."));
                }

                _mapper.Map(weatherStationDTO, existingStation);
                await _dbContext.SaveChangesAsync();

                return Ok(ApiResponse<WeatherStationDTO>.Ok(_mapper.Map<WeatherStationDTO>(existingStation),
                    "Weather Station updated successfully."));

            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500, "An error occured while updating Weather Station: ", e.Message);
                return StatusCode(500, errorResponse);

            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteWeatherStation(int id)
        {
            try
            {
                var existingWeatherStation = await _dbContext.WeatherStations.FirstOrDefaultAsync(s => s.Id == id);
                if (existingWeatherStation == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Weather Station with ID {id} was not found."));
                }

                _dbContext.WeatherStations.Remove(existingWeatherStation);
                await _dbContext.SaveChangesAsync();

                return Ok(ApiResponse<object>.NoContent("Weather data deleted successfully."));
            }
            catch (Exception e)
            {
                var errorResponse =
                    ApiResponse<object>.Error(500, "An error occured while deleting the Weather Station: ", e.Message);
                return StatusCode(500, errorResponse);

            }

        }

    }
}
