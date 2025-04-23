using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class DbInializer(StoreDBContext context) : IDbInializer
    {
        public  async Task InializeAsync()
        {
            if ((await context.Database.GetAppliedMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            try
            {
                if (!context.Set<ProductBrand>().Any())
                {
                    var Data = await File.ReadAllTextAsync(@"..\Infrastructrue\Presistence\Data\Seeds\brands.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductBrand>>(Data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductBrand>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<ProductType>().Any())
                {
                    var Data = await File.ReadAllTextAsync(@"..\Infrastructrue\Presistence\Data\Seeds\types.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductType>>(Data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductType>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<Product>().Any())
                {
                    var Data = await File.ReadAllTextAsync(@"..\Infrastructrue\Presistence\Data\Seeds\products.json");
                    var Objects = JsonSerializer.Deserialize<List<Product>>(Data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<Product>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO
                throw;
            }

            

        }


    }
}
