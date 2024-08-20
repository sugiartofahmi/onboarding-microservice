using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotNetService.Seeders
{
    public class ProductSeeder
    {
        public ProductSeeder(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Models.Product>()
                .HasData(
                    new Models.Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Laptop",
                        Description = "High-performance laptop for professionals",
                        Price = 1500000,
                        Stock = 50,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Models.Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Smartphone",
                        Description = "Latest model smartphone with advanced features",
                        Price = 800000,
                        Stock = 100,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Models.Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Headphones",
                        Description = "Wireless noise-cancelling headphones",
                        Price = 300000,
                        Stock = 75,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                );
        }
    }
}
