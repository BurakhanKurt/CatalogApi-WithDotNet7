using Catalog.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Repository.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10, 2)"); ;
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(50);
        }
    }
}
