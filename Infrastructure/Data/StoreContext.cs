using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
    {
        // Define DbSets for Product, ProductBrand, and ProductType entities
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        // Override the OnModelCreating method to apply entity configurations from the executing assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base method
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Apply entity configurations

            // Check if the database provider is SQLite using Entity Framework Core
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // Iterate over all entity types in the model
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    // Get all properties of type decimal for the current entity type
                    var properties = entityType
                        .ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(decimal));

                    var dateTimeProperties = entityType
                        .ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    // Iterate over the properties
                    foreach (var property in properties)
                    {
                        // Convert the property to double using Entity Framework Core
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        // Convert the property to DateTimeOffset using Entity Framework Core
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
