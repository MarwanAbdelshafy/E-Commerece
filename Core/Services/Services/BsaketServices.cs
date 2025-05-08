using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Basket;
using Shared.Dto_s.BasketDto;

namespace Services
{
    public class BsaketServices(IBsaketRepository Repo,IMapper mapper) : IBsaketServices

    {
        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket =await Repo.GetBasketAsync(Key);

            if (Basket is not null)
                return mapper.Map<CustomerBasket, BasketDto>(Basket);
            else throw new BsaketNotFoundException(Key);
                  


        }
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket=mapper.Map<BasketDto,CustomerBasket>(basket);

            var CreatedOrUpdatedBasket =await  Repo.CreateOrUpdateBassketAsync (CustomerBasket);

            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can't Create Or Update Basket Now! ");
             
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await Repo.DeleteBasketAsync(Key);
        }

        
    }
}
