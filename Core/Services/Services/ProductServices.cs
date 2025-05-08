using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Products;
using Services.Specifications;
using Shared;
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

        public async Task<PaginatedResult<ProductDto>> GetAllProductAsync(ProductQueryParams productQuery)
        {
            var _Repository = unitOfWorK.GetRepository<Product, int>();
            var spcs = new ProductWithBrandAndTypeSpecification(productQuery);//2 include whitout where
            var products = await _Repository.GetAllAsync(spcs);

            var Mapperproducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            var CountedProduct = products.Count();

            var CountSpcs = new ProductCountSpesification(productQuery);

            var TotalCount =await _Repository.CountAsync(CountSpcs);
            return new PaginatedResult<ProductDto>(productQuery.PageIndex, CountedProduct, TotalCount, Mapperproducts);
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

            var spec = new ProductWithBrandAndTypeSpecification(id);
            var Prpduct = await unitOfWorK.GetRepository<Product, int>().GetByIdAsync(spec);

            if (Prpduct is null)
                throw new ProductNotFoundException(id);

            return mapper.Map<Product, ProductDto>(Prpduct);


        }
    }
}
