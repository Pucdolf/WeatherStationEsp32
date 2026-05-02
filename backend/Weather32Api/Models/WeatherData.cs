using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather32Api.Models;

public class WeatherData
{
    [Key]
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

    [Required]
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    [Required]
    public int WeatherStationId { get; set; }

    [ForeignKey("WeatherStationId")]
    public WeatherStation WeatherStation { get; set; }

}