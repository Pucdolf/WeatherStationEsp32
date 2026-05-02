using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather32Api.Models
{
    public class WeatherStation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<WeatherStationFeatures> Features { get; set; }
        public ICollection<WeatherData> WeatherRecords { get; set; }
    }
}
