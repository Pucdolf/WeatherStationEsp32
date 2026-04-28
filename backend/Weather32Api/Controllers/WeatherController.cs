using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.DTO;
using Weather32Api.Models;

namespace Weather32Api.Controllers
{
    [Route("api/weatherdata")]
    //[Route("api/[controller]")] //Adres to api/weather
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WeatherController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //Endpoint
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WeatherDataDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<WeatherDataDTO>>>> GetWeatherData()
        {
            var weatherData = await _dbContext.WeatherRecords.ToListAsync();
            var dtoResponseWeatherData = _mapper.Map<List<WeatherDataDTO>>(weatherData);
            return Ok(ApiResponse<IEnumerable<WeatherDataDTO>>.Ok(dtoResponseWeatherData, "Weather data retrieved successfully."));

            //return Ok(await _dbContext.WeatherRecords.ToListAsync()); //200 ok
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<WeatherDataDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<WeatherDataDTO>>> GetWeatherDataById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather data ID must be greater than 0."));
                    //return BadRequest("Weather data ID must be greater than 0."); //400
                }

                var weatherData = await _dbContext.WeatherRecords.FirstOrDefaultAsync(u => u.Id == id);

                if (weatherData == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Weather data with ID {id} was not found."));
                    //return NotFound($"Weather data with ID {id} was not found."); //404
                }

                return Ok(ApiResponse<WeatherDataDTO>.Ok(_mapper.Map<WeatherDataDTO>(weatherData),
                    "Records were retrieved successfully.")); //200 
            }
            catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Error(500,
                    $"An error occured while retrieving weather data with id {id}: ", e.Message);
                return StatusCode(500, errorResponse); //500
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<WeatherDataDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<WeatherDataDTO>>> CreateWeatherData(WeatherDataCreateDTO weatherDataDTO)
        {
            try
            {
                if (weatherDataDTO == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Weather data is required."));
                    //return BadRequest("Weather data is required."); //400
                }

                //WeatherData weatherData = new ()
                //{
                //    DhtTemperatureC = weatherDataDTO.DhtTemperatureC,
                //    DhtTemperatureF = weatherDataDTO.DhtTemperatureF,
                //    DhtTemperatureK = weatherDataDTO.DhtTemperatureK,
                //    Humidity = weatherDataDTO.Humidity,

                //    DsTemperatureC = weatherDataDTO.DsTemperatureC,
                //    DSTemperatureF = weatherDataDTO.DSTemperatureF,
                //    DSTemperatureK = weatherDataDTO.DSTemperatureK,
                //    TimeStamp = DateTime.UtcNow
                //};

                WeatherData weatherData = _mapper.Map<WeatherData>(weatherDataDTO);
                weatherData.TimeStamp = DateTime.UtcNow;

                await _dbContext.WeatherRecords.AddAsync(weatherData);
                await _dbContext.SaveChangesAsync();


                var response = ApiResponse<WeatherDataDTO>.CreatedAt(_mapper.Map<WeatherDataDTO>(weatherData),
                    "User created successfully.");
                return CreatedAtAction(nameof(CreateWeatherData), new { id = weatherData.Id }, response); //201 Created
            }
            catch (Exception e)
            {
                var errorResponse =
                    ApiResponse<object>.Error(500, $"An error occured while creating weather data: ", e.Message);
                return StatusCode(500, errorResponse); //500
            }
        }


        ////[HttpGet("{id:int}/{name}")] //Route
        //[HttpGet()] //Query
        ////public string GetWeatherDataById([FromRoute] int id, [FromRoute] string name) //Route
        //public string GetWeatherDataByIdAndName([FromQuery] int id, [FromQuery] string name) //Query
        //{
        //    return $"Get weather data: {id} : {name}";
        //}
    }
}
