using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.BasketDto;

namespace Presentation.Controllers
{
    public class BasketController(IServicesManager servicesManager) : ApiBaseController
    {
        //Get Basket
        //GET BaseURL/api/Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket=await servicesManager.BasketServices.GetBasketAsync(key);
            return Ok(basket);
        }

        //Create Or Updarte Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdarteBasket(BasketDto basket)
        {
            var Basket = await servicesManager.BasketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        //Delete basket
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result =await servicesManager.BasketServices.DeleteBasketAsync(key);
            return Ok(Result);
        }



    }
}
