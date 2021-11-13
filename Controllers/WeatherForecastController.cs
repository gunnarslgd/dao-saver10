using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.WindowsAzure.Storage;

namespace DaoSaver.Controllers
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

		[HttpGet]
		public async Task<IEnumerable<Transaction>> Get()
		{
			return await Transaction.GetTransactions();
			// var rng = new Random();
			// return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			// {
			// 	Date = DateTime.Now.AddDays(index),
			// 	TemperatureC = rng.Next(-20, 55),
			// 	Summary = Summaries[rng.Next(Summaries.Length)]
			// })
			// .ToArray();
		}
	}
}
