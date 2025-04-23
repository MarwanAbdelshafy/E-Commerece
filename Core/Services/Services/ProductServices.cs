using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Products;
using Services.Specifications;
using Shared.Dto_s;

namespace Services
{
    public class ProductServices (IUnitOfWorK unitOfWorK,IMapper mapper): IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var _Repository =unitOfWorK.GetRepository<ProductBrand,int>();

            var Brands =await _Repository.GetAllAsync();

            var MapperBrans=mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);

            return MapperBrans;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var _Repository = unitOfWorK.GetRepository<Product, int>();
            var spcs = new ProductWithBrandAndTypeSpecification();//2 include whitout where
            var products = await _Repository.GetAllAsync(spcs);

            var Mapperproducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            return Mapperproducts;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var _Repository = unitOfWorK.GetRepository<ProductType, int>();

            var Types = await _Repository.GetAllAsync();

            var MapperTypes = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);

            return MapperTypes;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Prpduct = await unitOfWorK.GetRepository<Product, int>().GetByIdAsync(id);

            return mapper.Map<Product, ProductDto>(Prpduct);


        }
    }
}
