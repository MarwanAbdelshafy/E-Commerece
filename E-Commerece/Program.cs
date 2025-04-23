
using System.Threading.Tasks;
using Abstraction;
using Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using Presistence.Data;
using Presistence.Repositories;
using Services;

namespace E_Commerece
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddDbContext<StoreDBContext>(Options =>
            {
                var Connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

                Options.UseSqlServer(Connectionstring);
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            builder.Services.AddScoped<IDbInializer, DbInializer>();
            builder.Services.AddScoped<IUnitOfWorK,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyReferences).Assembly);
            builder.Services.AddScoped<IServicesManager, ServicesManager>();



            var app = builder.Build();

            await InializeDbAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();
            app.Run();
        }



        public static async Task InializeDbAsync(WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var dbInializer = Scope.ServiceProvider.GetRequiredService<IDbInializer>();
            await dbInializer.InializeAsync();

        }

    }
}
