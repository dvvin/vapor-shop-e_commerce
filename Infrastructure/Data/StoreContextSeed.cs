using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed // Class to seed the database with initial data
    {
        // Define a static method to asynchronously seed the database with initial data
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any()) // Check if the ProductBrands table is empty
                {
                    // Read the brands data from the JSON file
                    var brandsData = File.ReadAllText(
                        "../Infrastructure/Data/SeedData/brands.json"
                    );

                    // Deserialize the JSON data into a list of ProductBrand objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands != null) // If the deserialization was successful, add each brand to the ProductBrands table
                    {
                        foreach (var item in brands)
                        {
                            context.ProductBrands.Add(item);
                        }
                    }

                    await context.SaveChangesAsync(); // Save the changes to the database
                }

                if (!context.ProductTypes.Any()) // Check if the ProductTypes table is empty
                {
                    // Read the types data from the JSON file
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    // Deserialize the JSON data into a list of ProductType objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types != null) // If the deserialization was successful, add each type to the ProductTypes table
                    {
                        foreach (var item in types)
                        {
                            context.ProductTypes.Add(item);
                        }
                    }

                    await context.SaveChangesAsync(); // Save the changes to the database
                }

                if (!context.Products.Any()) // Check if the Products table is empty
                {
                    // Read the products data from the JSON file
                    var productsData = File.ReadAllText(
                        "../Infrastructure/Data/SeedData/products.json"
                    );

                    // Deserialize the JSON data into a list of Product objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products != null) // If the deserialization was successful, add each product to the Products table
                    {
                        foreach (var item in products)
                        {
                            context.Products.Add(item);
                        }
                    }

                    await context.SaveChangesAsync(); // Save the changes to the database
                }

                if (!context.DeliveryMethods.Any()) // Check if the DeliveryMethods table is empty
                {
                    var dmData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) // Catch any exceptions that may occur during the seed process
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(
                    ex,
                    "An error occurred during the seed process: {ErrorMessage}",
                    ex.Message
                );
            }
        }
    }
}
