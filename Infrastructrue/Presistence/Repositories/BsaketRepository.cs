using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.Basket;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    public class BsaketRepository(IConnectionMultiplexer connection) : IBsaketRepository
    {
        private readonly IDatabase _database=connection.GetDatabase() ;
        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
           var Basket=await _database.StringGetAsync(Key) ;  

            if (Basket.IsNullOrEmpty)
                return null ;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!) ;
        }


        public async Task<CustomerBasket> CreateOrUpdateBassketAsync(CustomerBasket customerBasket, TimeSpan? timetolive = null)
        {
            var jsonBasket=JsonSerializer.Serialize(customerBasket) ;

            var isCreatedOrUpdated = await _database.StringSetAsync(customerBasket.Id,jsonBasket,timetolive?? TimeSpan.FromDays(30));

            if (isCreatedOrUpdated)
                return await GetBasketAsync(customerBasket.Id);
            else
                return null;
        }


        public async Task<bool> DeleteBasketAsync(string Key)
        {
           return await _database.KeyDeleteAsync(Key) ;
        }

       
    }
}
