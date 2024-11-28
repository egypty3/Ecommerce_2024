using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public BasketRepository(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public async Task<Basket?> GetBasketAsync(string basketId)
        {
            var database = _redisConnection.GetDatabase();
            var basketData = await database.StringGetAsync(GetRedisKey(basketId));
           
            return ( basketData.IsNullOrEmpty)
                        ?  null: JsonSerializer.Deserialize<Basket>(basketData);
        }


        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var database = _redisConnection.GetDatabase();
            await database.StringSetAsync(GetRedisKey(basket.Id), JsonSerializer.Serialize(basket));
            return basket;
        }

        private string GetRedisKey(string basketId)
        {
            return $"Basket-{basketId}";
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var database = _redisConnection.GetDatabase();
            return await database.KeyDeleteAsync(GetRedisKey(basketId));
        }
    }
}
