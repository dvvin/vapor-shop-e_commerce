using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    // The IValueResolver interface maps a source type to a destination type
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config; // Declare a private field to store the IConfiguration instance

        // Constructor for the ProductUrlResolver class that takes an IConfiguration instance as a parameter
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config; // Assign the IConfiguration instance to the _config field
        }

        // Resolve method that takes in the source object, destination object, destination member, and resolution context
        public string Resolve(
            Product source,
            ProductToReturnDto destination,
            string destMember,
            ResolutionContext context
        )
        {
            // Check if the PictureUrl property of the source object is not empty or null
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                // Return the concatenation of the ApiUrl from the configuration and the PictureUrl from the source object
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null; // If the PictureUrl is empty or null, return null
        }
    }
}
