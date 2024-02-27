using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    // Define a class named ProductConfiguration that implements the IEntityTypeConfiguration interface for the Product entity
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) // Implement the Configure method to configure the entity type
        {
            // Configure the Id property of the Product entity to be required
            builder.Property(p => p.Id).IsRequired();

            // Configure the Name property of the Product entity to be required and have a maximum length of 100
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            // Configure the Description property of the Product entity to be required and have a maximum length of 180
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);

            // Configure the Price property of the Product entity to have a data type of decimal(18,2)
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            // Configure the PictureUrl property of the Product entity to be required
            builder.Property(p => p.PictureUrl).IsRequired();

            // Configure a one-to-many relationship between Product and ProductBrand entities
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);

            // Configure a one-to-many relationship between Product and ProductType entities
            builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}
