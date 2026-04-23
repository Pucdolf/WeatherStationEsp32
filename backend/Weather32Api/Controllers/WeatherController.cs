using Microsoft.AspNetCore.Mvc;

namespace Weather32Api.Controllers
{
    [Route("api/weatherdata")]
    //[Route("api/[controller]")] //Adres to api/weather
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //Endpoint
        //[HttpGet]
        //public string GetWeatherData()
        //{
        //    return "Get all weather data";
        //}

        [HttpGet("{id:int}")]
        public string GetWeatherDataById([FromRoute] int id)
        {
            return $"Get weather data: {id}";
        }

        //[HttpGet("{id:int}/{name}")] //Route
        [HttpGet()] //Query
        //public string GetWeatherDataById([FromRoute] int id, [FromRoute] string name) //Route
        public string GetWeatherDataByIdAndName([FromQuery] int id, [FromQuery] string name) //Query
        {
            return $"Get weather data: {id} : {name}";
        }
    }
}
