using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather32Api.Models.DTO;

public class WeatherStationFeaturesUpdateDTO
{

    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }

    [Required]
    public int WeatherStationId { get; set; }


}