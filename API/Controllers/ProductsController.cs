using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {
        private readonly IProductRepository _repo = repo; // Declares a read-only field for the repository.

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync(); // Asynchronously gets all products.
            return Ok(products); // Returns the products with a 200 OK response.
        }

        [HttpGet("{id}")] // Marks this method as handling HTTP GET requests with an "id" parameter.
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id); // Asynchronously gets a product by its ID.
            if (product == null)
                return NotFound(); // Returns a 404 NotFound response if the product is not found.
            return product; // Returns the found product with a 200 OK response.
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _repo.GetProductBrandsAsync(); // Asynchronously gets all product brands.
            return Ok(brands); // Returns the product brands with a 200 OK response.
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _repo.GetProductTypesAsync(); // Asynchronously gets all product types.
            return Ok(types); // Returns the product types with a 200 OK response.
        }
    }
}
