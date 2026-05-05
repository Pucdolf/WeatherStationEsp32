using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.Models;
using Weather32Api.Models.DTO;

namespace Weather32Api.Controllers
{
    [ApiController]
    [Route("api/station-features")]
    public class WeatherStationFeaturesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WeatherStationFeaturesController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WeatherStationFeaturesDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<WeatherStationFeaturesDTO>>>> GetWeatherStationFeatures()
        {
            var features = await _dbContext.WeatherStationFeatures.ToListAsync();
            var dtoResponse = _mapper.Map<List<WeatherStationFeaturesDTO>>(features);
            return Ok(ApiResponse<IEnumerable<WeatherStationFeaturesDTO>>.Ok(dtoResponse, "Weather Station Features retrieved successfully."));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<WeatherStationFeaturesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WeatherStationFeaturesDTO>>> GetWeatherStationFeatureById(int id)
        {
            try
            {
                var feature = await _dbContext.WeatherStationFeatures.FirstOrDefaultAsync(s => s.Id == id);
                if (feature == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Weather Station Feature with ID {id} was not found."));
                }

                return Ok(ApiResponse<WeatherStationFeaturesDTO>.Ok(
                    _mapper.Map<WeatherStationFeaturesDTO>(feature),
                    "Weather Station Feature retrieved successfully."));
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500,
                    $"An unexpected error occured while retrieving weather station feature with ID {id}:", e.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<WeatherStationFeaturesDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WeatherStationFeaturesDTO>>> CreateWeatherStationFeature(
            WeatherStationFeaturesCreateDTO weatherStationFeaturesDTO)
        {
            try
            {
                if (weatherStationFeaturesDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather Station Feature data is required."));
                }

                var validWeatherStation = await _dbContext.WeatherStations.AnyAsync(s =>
                    s.Id == weatherStationFeaturesDTO.WeatherStationId);

                if (!validWeatherStation)
                {
                    return NotFound(ApiResponse<object>.NotFound(
                        $"Weather Station with ID {weatherStationFeaturesDTO.WeatherStationId} does not exist."));
                }

                WeatherStationFeatures feature = _mapper.Map<WeatherStationFeatures>(weatherStationFeaturesDTO);
                await _dbContext.AddAsync(feature);
                await _dbContext.SaveChangesAsync();

                var response = ApiResponse<WeatherStationFeaturesDTO>.CreatedAt(
                    _mapper.Map<WeatherStationFeaturesDTO>(feature), "Weather Station Feature created successfully.");
                return CreatedAtAction(nameof(GetWeatherStationFeatureById), new { id = feature.Id }, response);
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500,
                    $"An unexpected error occured while creating Weather Station Feature:", e.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<WeatherStationFeaturesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WeatherStationFeaturesDTO>>> UpdateWeatherStationFeatures(int id, WeatherStationFeaturesUpdateDTO weatherStationFeaturesDTO)
        {
            try
            {
                if (weatherStationFeaturesDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather Station Features data is required."));
                }

                if (id != weatherStationFeaturesDTO.Id)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather Station Feature ID does not match Weather Station Feature ID in request body."));
                }

                var existingFeature = await _dbContext.WeatherStationFeatures.FirstOrDefaultAsync(s => s.Id == id);
                if (existingFeature == null)
                {
                    return NotFound(
                        ApiResponse<object>.NotFound($"Weather Station Feature with ID {id} was not found."));
                }

                _mapper.Map(weatherStationFeaturesDTO, existingFeature);
                await _dbContext.SaveChangesAsync();

                return Ok(ApiResponse<WeatherStationFeaturesDTO>.Ok(
                    _mapper.Map<WeatherStationFeaturesDTO>(existingFeature),
                    "Weather Station Feature updated successfully."));
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500, $"An error occured while updating Weather Station Feature with ID {id}: ", e.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteWeatherStationFeatures(int id)
        {
            try
            {
                var existingFeature = await _dbContext.WeatherStationFeatures.FirstOrDefaultAsync(s => s.Id == id);
                if (existingFeature == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Weather Station Feature with ID {id} was not found."));
                }

                _dbContext.WeatherStationFeatures.Remove(existingFeature);
                await _dbContext.SaveChangesAsync();

                return Ok(ApiResponse<object>.NoContent("Weather Station Feature deleted successfully."));
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500,
                    $"An unexpected error occured while deleting Weather Station Feature:", e.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
