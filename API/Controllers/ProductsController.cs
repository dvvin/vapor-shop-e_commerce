using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Define a public class ProductsController that extends the BaseApiController
    public class ProductsController : BaseApiController
    {
        // Declare three private fields to hold instances of IGenericRepository for Product, ProductBrand, and ProductType
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;

        // Constructor for ProductsController that takes instances of IGenericRepository for Product, ProductBrand, and ProductType
        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductType> typeRepo
        )
        {
            // Assign the passed-in instances to the corresponding private fields
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            // Initializes a new ProductsWithTypesAndBrandsSpecifications instance.
            var spec = new ProductsWithTypesAndBrandsSpecifications();
            var products = await _productRepo.ListAsync(spec); // Asynchronously gets all products.
            return Ok(products); // Returns the products with a 200 OK response.
        }

        [HttpGet("{id}")] // Marks this method as handling HTTP GET requests with an "id" parameter.
        [ProducesResponseType(StatusCodes.Status200OK)] // Marks this method as returning a 200 OK response.
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] // Marks this method as returning a 404 Not Found response.
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Initializes a new ProductsWithTypesAndBrandsSpecificationsById instance with the specified product ID.
            var spec = new ProductsWithTypesAndBrandsSpecificationsById(id);
            var product = await _productRepo.GetEntityWithSpec(spec); // Asynchronously gets the product with the specified ID.

            // If the product is not found, returns a 404 Not Found response. Otherwise, returns the product with a 200 OK response.
            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _brandRepo.ListAllAsync(); // Asynchronously gets all product brands.
            return Ok(brands); // Returns the product brands with a 200 OK response.
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _typeRepo.ListAllAsync(); // Asynchronously gets all product types.
            return Ok(types); // Returns the product types with a 200 OK response.
        }
    }
}
