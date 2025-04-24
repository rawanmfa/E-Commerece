using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace persistence.Repositories
{
	public class CashRepository(IConnectionMultiplexer connectionMultiplexer) : ICashRepository
	{
		private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
		public async Task<string?> GetAsync(string key)
		{
			var value = await _database.StringGetAsync(key);
			return value.IsNullOrEmpty ? value : default;
		}

		public async Task SetAsync(string key, object value, TimeSpan duration)
		{
			var serlizerObject = JsonSerializer.Serialize(value);
			await _database.StringSetAsync(key, serlizerObject, duration);
		}
	}
}
