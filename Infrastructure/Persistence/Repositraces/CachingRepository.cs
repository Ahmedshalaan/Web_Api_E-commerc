using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositraces
{
    public class CachingRepository(IConnectionMultiplexer connectionMultiplexer) : ICachingRepository
    {
        private readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
           var value =await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();

        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var SerilizedOption = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
             
            };
            var serializedValue = JsonSerializer.Serialize(value, SerilizedOption);
            await _database.StringSetAsync(key, serializedValue, duration);
        }
    }
}
