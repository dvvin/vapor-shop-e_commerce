namespace API.Dtos
{
    public class ProductToReturnDto // class to store data about a product
    {
        public int Id { get; set; } // property to store the ID of the product
        public string? Name { get; set; } // property to store the name of the product
        public string? Description { get; set; } // property to store the description of the product
        public decimal Price { get; set; } // property to store the price of the product
        public string? PictureUrl { get; set; } // property to store the URL of the product image
        public string? ProductType { get; set; } // property to store the type of the product
        public string? ProductBrand { get; set; } // property to store the brand of the product
    }
}
