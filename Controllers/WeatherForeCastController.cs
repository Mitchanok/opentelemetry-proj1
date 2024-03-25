using Microsoft.AspNetCore.Mvc;

/* namespace weatherapp_telemetry.Controllers
{
    [ApiController]
    public class WeatherForeCastController : ControllerBase
    {
        private static readonly string[] Summaries =
        {
              "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> Get([FromRoute] string city, [FromQuery] int days = 5)
        {
            if (Random.Shared.Next(1, 1000) == 10)
            {
                throw new Exception("It is False");
            }

            var forecasts = Enumerable.Range(1, days).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            await Task.Delay(Random.Shared.Next(5, 100));

            return Ok(new WeatherForecastResponse
            { 
              city = city,
              Forecasts = forecasts
            });
        }
        
    }
}
*/