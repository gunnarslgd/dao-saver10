using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaoSaver;

namespace Charts
{
	/*
	 * NASDAQ100
	 * DAX PERFORMANCE-INDEX
	 * Euronext 100 Index
	 * EURUSD
	 * HANG SENG INDEX
	 * JAPAN225
	 * NZDAUD
	 * Shenzhen Component
	 * SSE Composite Index
	 */

	public class MarketStepCache
	{
		private static Dictionary<string, List<MarketStep>> _cache = new Dictionary<string, List<MarketStep>>();

		public static List<string> MarketKeys = new List<string>()
		{
			"NASDAQ100", 
			"DAX PERFORMANCE-INDEX", 
			"Euronext 100 Index",
			"JAPAN225",
			"HANG SENG INDEX",
			"Shenzhen Component",
			"SSE Composite Index",
			"EURUSD",
			"NZDAUD",
		};

		public static async Task<List<MarketStep>> LoadData(string key, DateTime? startDate = null)
		{
			startDate ??= new DateTime(2002, 2, 8);

			if (_cache.TryGetValue(key, out var data) == false)
			{
				data = await MarketStep.GetSteps(key, startDate.Value);
				_cache[key] = data;
			}

			return data;
		}
	}
}
