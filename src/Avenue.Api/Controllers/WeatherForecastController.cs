//using Avenue.Library.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Avenue.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		//private readonly IBlogRepository _blogRepository;
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(
			ILogger<WeatherForecastController> logger
			/*, IBlogRepository blogRepository*/
			)
		{
			_logger = logger;
			//_blogRepository = blogRepository;
			//_blogRepository.Test4b();
			_ = "";
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
	}
}