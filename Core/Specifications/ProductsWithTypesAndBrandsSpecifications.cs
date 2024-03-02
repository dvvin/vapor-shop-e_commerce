using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecifications(ProductSpecParams productParams)
            : base(x =>
                (
                    // Check if the productParams.Search is empty or if the product name contains the search string
                    string.IsNullOrEmpty(productParams.Search)
                    || x.Name!.ToLower().Contains(productParams.Search)
                )
                // Check if the productParams.BrandId is not specified or if the product belongs to the specified brand
                && (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
                // Check if the productParams.TypeId is not specified or if the product is of the specified type
                && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            // Adding an include for ProductType and ProductBrand
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);

            // Adding an order by name
            AddOrderBy(x => x.Name!);

            // Applying paging
            ApplyPaging(
                productParams.PageSize * (productParams.PageIndex - 1),
                productParams.PageSize
            );

            if (!string.IsNullOrEmpty(productParams.Sort)) // Check if the productParams.Sort is not null or empty
            {
                switch (productParams.Sort) // Use a switch statement to handle different sort options
                {
                    case "priceAsc": // If sort option is "priceAsc", add an order by ascending price

                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc": // If sort option is "priceDesc", add an order by descending price

                        AddOrderByDescending(p => p.Price);
                        break;

                    default: // For any other sort option, add an order by name

                        AddOrderBy(n => n.Name!);
                        break;
                }
            }
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
