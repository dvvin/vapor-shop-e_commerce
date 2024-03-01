using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecification<Product>
    {
        // Constructor for ProductsWithTypesAndBrandsSpecifications
        public ProductsWithTypesAndBrandsSpecifications()
        {
            // Adding an include for ProductType and ProductBrand
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);
        }
    }

    public class ProductsWithTypesAndBrandsSpecificationsById : BaseSpecification<Product>
    {
        // Constructor for ProductsWithTypesAndBrandsSpecificationsById that takes an id parameter
        public ProductsWithTypesAndBrandsSpecificationsById(int id)
            : base(x => x.Id == id) // Calling the base constructor with a condition for id
        {
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);
        }
    }
}
