using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi24.Models;

namespace Taxi24.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class _WeatherForecastController : ControllerBase
    {/*
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<_WeatherForecastController> _logger;

        public _WeatherForecastController(ILogger<_WeatherForecastController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<_WeatherForecast> Get() {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new _WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}
