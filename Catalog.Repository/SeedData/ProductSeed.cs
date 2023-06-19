using Catalog.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Repository.SeedData
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var products = new List<Product>();

            for(int i = 1; i <= 100; i++)
            {
                products.Add(
                    new Product
                    {
                        Id = i,
                        Name = $"Product {i}",
                        Stock = 1000,
                        Price = 10.50m,
                        CategoryId = (i % 10)+1,
                        CreatedDate = DateTime.Now,
                    }
                );
            }

            builder.HasData(products);
        }
    }
}
