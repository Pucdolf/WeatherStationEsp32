using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class WeatherDataDTO
    {
        public int Id { get; set; }

        //DHT11
        public float? DhtTemperatureC { get; set; }
        public float? DhtTemperatureF { get; set; }
        public float? DhtTemperatureK { get; set; }
        public float? Humidity { get; set; }

        //DS18B20
        public float? DsTemperatureC { get; set; }
        public float? DSTemperatureF { get; set; }
        public float? DSTemperatureK { get; set; }
        public DateTime TimeStamp { get; set; }
        public int WeatherStationId { get; set; }
    }
}
