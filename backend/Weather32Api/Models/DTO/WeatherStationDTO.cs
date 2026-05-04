using System.ComponentModel.DataAnnotations;

namespace Weather32Api.Models.DTO
{
    public class WeatherStationDTO

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int UserId { get; set; }
    }
}
