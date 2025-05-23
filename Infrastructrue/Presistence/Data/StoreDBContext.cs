﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class StoreDBContext(DbContextOptions<StoreDBContext> options):DbContext(options) //CTOR
    {
       
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> brands { get; set; }
        public DbSet<ProductType> Types { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

    }
}
