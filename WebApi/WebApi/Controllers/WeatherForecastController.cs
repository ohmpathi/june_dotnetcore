using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleSorts;

namespace WebApi.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("sort")]
        public IActionResult GetSortedList()
        {
            var list = new List<int> { 5, 4, 3, 2, 1 };
            var sortedList = new SlectionSort().Sort(list);
            return Ok(new
            {
                randon = list,
                sorted = sortedList
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            int x = 6;
            int y = 0;
            int z = x / y;

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("secret")]
        [Authorize(Policy = "accesSecret")]
        public IEnumerable<WeatherForecast> GetSecret()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post()
        {
            int x = 6;
            int y = 0;
            int z = x / y;
            return Ok(z);
        }
    }
}
