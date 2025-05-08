using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.Identity;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class DbInializer(StoreDBContext context,UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole> roleManager,StoreIdentityDbContext IdentityDbContext ) : IDbInializer
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

        public async Task IdentityInializeAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!userManager.Users.Any())
                {
                    var user1 = new ApplicationUser()
                    {
                        Email = "marwan@gmail.com",
                        DisplayName = "Marwan Abdelshafy",
                        PhoneNumber = "01204229019",
                        UserName = "MarwanAbdelshafy"
                    };
                    var user2 = new ApplicationUser()
                    {
                        Email = "ali@gmail.com",
                        DisplayName = "Ali Ahmed",
                        PhoneNumber = "01220631274",
                        UserName = "AliAhmed"
                    };

                    await userManager.CreateAsync(user1, "P@ssw0rd");
                    await userManager.CreateAsync(user2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "SuperAdmin");

                }

                await IdentityDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

    }
}
