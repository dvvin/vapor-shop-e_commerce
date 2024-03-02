using Core.Entities;

namespace Core.Specifications
{
    // This class represents a specification for counting products with applied filters
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(x =>
                (
                    // If the search parameter is empty or the product name contains the search string
                    string.IsNullOrEmpty(productParams.Search)
                    || x.Name!.ToLower().Contains(productParams.Search)
                )
                // And the product either does not have a brand id or the brand id matches the specified brand id
                && (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
                // And the product either does not have a type id or the type id matches the specified type id
                && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            ) { }
    }
}
