namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; } // property to store the name of the product
        public string? Description { get; set; } // property to store the description of the product
        public decimal Price { get; set; } // property to store the price of the product
        public string? PictureUrl { get; set; } // property to store the URL of the product image
        public ProductType? ProductType { get; set; } // property to store the type of the product
        public int ProductTypeId { get; set; } // property to store the ID of the product type
        public ProductBrand? ProductBrand { get; set; } // property to store the brand of the product
        public int ProductBrandId { get; set; } // property to store the ID of the product brand
    }
}
