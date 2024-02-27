using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository // Interface for product repository.
    {
        Task<Product?> GetProductByIdAsync(int id); // Asynchronous method for retrieving a product by its ID.
        Task<IReadOnlyList<Product>> GetProductsAsync(); // Asynchronous method for retrieving all products.
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(); // Asynchronous method for retrieving all product brands.
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync(); // Asynchronous method for retrieving all product types.
    }
}
