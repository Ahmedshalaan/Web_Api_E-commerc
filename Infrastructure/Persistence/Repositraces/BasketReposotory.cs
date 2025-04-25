//install-package StackExchange.Redis
using StackExchange.Redis;

namespace Presistence.Repositraces
{
    public class BasketReposotory(IConnectionMultiplexer connectionMultiplexer) : IBasketReposotory
    {
        private readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timetolife = null)
        {
          var jsonBasket = JsonSerializer.Serialize(basket); //set json in momery DataBase Convart to json
            var createdOrUpdate = await  _database.StringSetAsync(basket.Id, jsonBasket,timetolife??TimeSpan.FromDays(30)); //set the basket in the redis
            return createdOrUpdate ? await GetBasketAsync(basket.Id) : null; //if the basket is created or updated return it
        }


        public async Task<bool> DeleteBasketAsync(string Id)
        => await _database.KeyDeleteAsync(Id); //delete the basket from the redis

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var data = await _database.StringGetAsync(Id);
            if (data.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(data);
        }
    }
}
