   
using System.Threading.Tasks;
using Abstraction;
using Domain.Contracts;
using Domain.Models.Identity;
using E_Commerece.CustomMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using Presistence.Data;
using Presistence.Repositories;
using Services;
using Shared.ErrorModels;
using StackExchange.Redis;

namespace E_Commerece
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region DI container service
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddDbContext<StoreDBContext>(Options =>
            {
                var Connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

                Options.UseSqlServer(Connectionstring);
            });


            builder.Services.AddDbContext<StoreIdentityDbContext>(Options =>
            {
                var Connectionstring = builder.Configuration.GetConnectionString("IdentityConnection");

                Options.UseSqlServer(Connectionstring);
            });

            //security
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                             .AddEntityFrameworkStores<StoreIdentityDbContext>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDbInializer, DbInializer>();
            builder.Services.AddScoped<IUnitOfWorK, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyReferences).Assembly);
            builder.Services.AddScoped<IServicesManager, ServicesManager>();
            builder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(M => M.Value.Errors.Any())
                        .Select(M => new ValidationError()
                        {
                                                   field = M.Key,
                                                   errors=M.Value.Errors.Select(E=>E.ErrorMessage)
                        });
                    //-----------------------------------------------------
                    var response = new ValidationErrorToReturn()
                    {
                       
                       ValidationErrors= errors

                    };
                    //--------------
                    return new BadRequestObjectResult(response);
                };

            });
            builder.Services.AddScoped<IBsaketRepository,BsaketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));
            });
            #endregion


            var app = builder.Build();

            await InializeDbAsync(app);


            #region Middleware--pipeline
            // Configure the HTTP request pipeline.

            app.UseMiddleware<CustomExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();
            #endregion

            app.Run();
        }


        public static async Task InializeDbAsync(WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var dbInializer = Scope.ServiceProvider.GetRequiredService<IDbInializer>();
            await dbInializer.InializeAsync();
            await dbInializer.IdentityInializeAsync();

        }

    }
}
