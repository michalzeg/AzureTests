using AzureTest.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AzureTest.Controllers
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
        private readonly IConfiguration _configuration;
        private readonly TestContext _testContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, TestContext testContext)
        {
            _logger = logger;
            _configuration = configuration;
            _testContext = testContext;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var configValue = _configuration["SomeData:Value"];

            await _testContext.AddAsync(new Test() { Type = "Webapp", LastUpdate = DateTime.Now });
            await _testContext.SaveChangesAsync();
            var dbValue = await _testContext.Tests.OrderByDescending(e=>e.Id).FirstOrDefaultAsync();
            await Task.Delay(Random.Shared.Next(50, 5000));

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = $"{Summaries[Random.Shared.Next(Summaries.Length)]} ConfigValue {configValue}Last Update {dbValue?.LastUpdate}"
            })
            .ToArray();
        }
    }
}