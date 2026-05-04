using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.Models.DTO;

namespace Weather32Api.Controllers
{
    [ApiController]
    [Route("api/station-features")]
    public class WeatherStationFeaturesController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WeatherStationFeaturesController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<WeatherStationFeaturesDTO>>>> GetWeatherStationFeatures()
        {
            var features = await _dbContext.WeatherStationFeatures.ToListAsync();
            var dtoResponse = _mapper.Map<List<WeatherDataDTO>>(features);
            return Ok(ApiResponse<IEnumerable<WeatherDataDTO>>.Ok(dtoResponse, "Weather Station Features retreived successfully."));
        }
    }
}