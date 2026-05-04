using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class WeatherStationCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
