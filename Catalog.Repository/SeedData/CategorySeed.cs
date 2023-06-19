using Catalog.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Repository.SeedData
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>();

            for (int i = 1; i <= 10; i++)
            {
                categories.Add(new Category { 
                    Id = i, 
                    Name = $"Category {i}",
                    CreatedDate = DateTime.Now 
                }); 
            }

            builder.HasData(categories);
        }
    }
}
