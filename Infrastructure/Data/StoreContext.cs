using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
    {
        // Define DbSets for Product, ProductBrand, and ProductType entities
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // Override the OnModelCreating method to apply entity configurations from the executing assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base method
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Apply entity configurations
        }
    }
}
