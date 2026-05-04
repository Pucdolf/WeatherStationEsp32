using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO;

public class WeatherStationUpdateDTO
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public int UserId{ get; set; }

}