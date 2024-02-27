using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext context) : IProductRepository // Implementation of the IProductRepository interface.
    {
        private readonly StoreContext _context = context; // Declares a private readonly field for the StoreContext.

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context // Asynchronously retrieves all products from the database.
                .Products // Asynchronously retrieves a product by its ID.
                .Include(p => p.ProductBrand) // Includes the product brand in the query.
                .Include(p => p.ProductType) // Includes the product type in the query.
                .SingleOrDefaultAsync(p => p.Id == id); // Returns the product if found, otherwise returns null.
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context // Asynchronously retrieves all products from the database.
                .Products // Asynchronously retrieves all products.
                .Include(p => p.ProductBrand) // Includes the product brand in the query.
                .Include(p => p.ProductType) // Includes the product type in the query.
                .ToListAsync(); // Returns a list of all products.
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync(); // Returns a list of all product brands.
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync(); // Returns a list of all product types.
        }
    }
}
