using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPostgreApp.Server.Data;

namespace ReactPostgreApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}", Name = "GetWeatherForecastById")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var forecast = new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            if (id < 1 || id > 5)
            {
                return NotFound();
            }

            return Ok(forecast);
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> Post(WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("Forecast is null");
            }

            // Optional: Validate the incoming data
            if (forecast.Date == default || string.IsNullOrWhiteSpace(forecast.Summary))
            {
                return BadRequest("Invalid forecast data");
            }

            return CreatedAtAction(nameof(Get), new { id = forecast.Date }, forecast);
        }
    }
}
