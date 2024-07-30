using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SeedCountries.Controllers
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
        private readonly CountryService _countryService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, CountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
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

        [HttpPost("Readcountries", Name = "ReadCountries")]
        [RequestSizeLimit(50 * 1024 * 1024)]
        public async Task<IActionResult> ReadCountries(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty or null.");
            }

            var countries = _countryService.GetAllCountriesFromJson(file);
            if (countries == null)
            {
                return BadRequest("Error occurred while deserializing JSON data from file.");
            }
           
            string filePathTimezone = "countriesStatesAndcities.txt";
            var countriesDto = JsonConvert.SerializeObject(countries);
            using (StreamWriter writer = new StreamWriter(filePathTimezone))
            {
                await writer.WriteAsync(countriesDto);
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePathTimezone, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "text/plain", Path.GetFileName(filePathTimezone));
        }
    }
}
