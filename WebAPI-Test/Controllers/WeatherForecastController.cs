using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Test.Controllers
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
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("GetData/{number}")]
        public WeatherForecast GetData(int number)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost]
        [Route("PostData")]
        public ActionResult<WeatherForecast> PostData([FromBody]WeatherForecast weatherForecast)
        {
            if (weatherForecast == null || weatherForecast.TemperatureC is 0) { return BadRequest(); }

            var result = new WeatherForecast 
            { 
                Date = weatherForecast.Date, 
                Summary =  weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC                
            };

            return Ok(result);
        }

    }
}