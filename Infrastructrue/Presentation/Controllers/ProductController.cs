using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServicesManager servicesManager): ControllerBase
    {
        //Get All Product

        [HttpGet]
        //Get  BaseUrl/api/Product
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
        {
            var products =await servicesManager.ProductServices.GetAllProductAsync();

            return Ok(products);
        }


        //Get All Brands

        [HttpGet("brands")]
        //Get  BaseUrl/api/Product/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await servicesManager.ProductServices.GetAllBrandsAsync();

            return Ok(brands);
        }

        //Get All Tyeps

        [HttpGet("types")]
        //Get  BaseUrl/api/Product/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await servicesManager.ProductServices.GetAllTypesAsync();

            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductById(int id)
        {
            var product = await servicesManager.ProductServices.GetProductByIdAsync(id);

            return Ok(product);

        }

    }
}
