using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Basket;

namespace Domain.Contracts
{
    public interface IBsaketRepository
    {
        Task<CustomerBasket?> GetBasketAsync (string Key);

        Task<CustomerBasket> CreateOrUpdateBassketAsync(CustomerBasket customerBasket,TimeSpan? timetolive =null); 

        Task<bool> DeleteBasketAsync (string Key);

    }
}
