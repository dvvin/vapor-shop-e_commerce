using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
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
        private readonly IMapper _mapper;

        // Constructor for ProductsController that takes instances of IGenericRepository for Product, ProductBrand, and ProductType
        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductType> typeRepo,
            IMapper mapper
        )
        {
            // Assign the passed-in instances to the corresponding private fields
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        // This method handles the HTTP GET request for retrieving a list of products with optional filtering and pagination
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams
        )
        {
            // Create a specification for retrieving products with their types and brands based on the specified parameters
            var spec = new ProductsWithTypesAndBrandsSpecifications(productParams);

            // Create a specification for counting the total number of products based on the specified parameters
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            // Retrieve the total number of products that match the specified parameters
            var totalItems = await _productRepo.CountAsync(countSpec);

            // Retrieve the list of products that match the specified parameters
            var products = await _productRepo.ListAsync(spec);

            // Map the retrieved products to a list of ProductToReturnDto using AutoMapper
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(
                products
            );

            // Return a 200 OK response with a Pagination object containing the paginated list of ProductToReturnDto
            return Ok(
                new Pagination<ProductToReturnDto>(
                    productParams.PageIndex,
                    productParams.PageSize,
                    totalItems,
                    data
                )
            );
        }

        [HttpGet("{id}")] // Marks this method as handling HTTP GET requests with an "id" parameter.
        [ProducesResponseType(StatusCodes.Status200OK)] // Marks this method as returning a 200 OK response.
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] // Marks this method as returning a 404 Not Found response.
        // Define a method that asynchronously retrieves a product by its ID
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // Create a new instance of the ProductsWithTypesAndBrandsSpecificationsById class, passing in the product ID
            var spec = new ProductsWithTypesAndBrandsSpecificationsById(id);

            // Call the GetEntityWithSpec method of the _productRepo, passing in the spec object, and await the result
            var product = await _productRepo.GetEntityWithSpec(spec);

            // Map the retrieved product to a ProductToReturnDto using the _mapper and return it as an ActionResult
            return _mapper.Map<Product, ProductToReturnDto>(product);
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
