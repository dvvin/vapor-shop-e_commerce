namespace Core.Entities
{
    public class ProductType : BaseEntity // This class inherits Id from the BaseEntity class.
    {
        public string? Name { get; set; } // Property to store the name of the product type.
    }
}
