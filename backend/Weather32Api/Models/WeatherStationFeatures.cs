using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Weather32Api.Models
{
    public class WeatherStationFeatures
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        
        [Required]
        public int WeatherStationId { get; set; }

        [ForeignKey("WeatherStationId")]
        public WeatherStation WeatherStation { get; set; }

    }
}
