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
        public async Task<ActionResult<IEnumerable<WeatherData>>> GetWeatherData()
        {
            return Ok(await _dbContext.WeatherRecords.ToListAsync()); //200 ok
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<WeatherData>> GetWeatherDataById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Weather data ID must be greater than 0."); //400
                }

                var weatherData = await _dbContext.WeatherRecords.FirstOrDefaultAsync(u => u.Id == id);

                if (weatherData == null)
                {
                    return NotFound($"Weather data with ID {id} was not found."); //404
                }

                return Ok(weatherData); //200 
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while retrieving weather data with id {id}: {e.Message}."); //500
            }
        }


        [HttpPost]
        public async Task<ActionResult<WeatherData>> CreateWeatherData(WeatherDataCreateDTO weatherDataDTO)
        {
            try
            {
                if (weatherDataDTO == null)
                {
                    return BadRequest("Weather data is required"); //400
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

                await _dbContext.WeatherRecords.AddAsync(weatherData);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateWeatherData), new { id = weatherData.Id}, weatherData); //201 Created
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while creating weather data: {e.Message}."); //500
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
