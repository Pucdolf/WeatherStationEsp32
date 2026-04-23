using Microsoft.AspNetCore.Mvc;

namespace Weather32Api.Controllers
{
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //Endpoint
        [HttpGet]
        [Route("weatherdata/")] //Adres to api/weather
        //[Route("api/[controller]")] //Adres to api/weather
        public string GetWeatherData()
        {
            return "Get all weather data";
        }
    }
}
