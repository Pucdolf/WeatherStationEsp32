using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class WeatherDataCreateDTO
    {
       
        //DHT11
        public float? DhtTemperatureC { get; set; }
        public float? DhtTemperatureF { get; set; }
        public float? DhtTemperatureK { get; set; }
        public float? Humidity { get; set; }

        //DS18B20
        public float? DsTemperatureC { get; set; }
        public float? DSTemperatureF { get; set; }
        public float? DSTemperatureK { get; set; }

        [Required]
        public int WeatherStationId { get; set; }
    }
}
