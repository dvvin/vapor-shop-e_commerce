using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            // Register the ProductService for dependency injection and associate it with the IProductService interface.
            services.AddScoped<ITokenService, TokenService>();

            // Register the ProductRepository for dependency injection and associate it with the IProductRepository interface.
            services.AddScoped<IProductRepository, ProductRepository>();

            // Register the BasketRepository for dependency injection and associate it with the IBasketRepository interface.
            services.AddScoped<IBasketRepository, BasketRepository>();

            // Register the GenericRepository for dependency injection and associate it with the IGenericRepository interface.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register the OrderService for dependency injection and associate it with the IOrderService interface.
            services.AddScoped<IOrderService, OrderService>();

            // Register the UnitOfWork for dependency injection and associate it with the IUnitOfWork interface.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register the PaymentService for dependency injection and associate it with the IPaymentService interface.
            services.AddScoped<IPaymentService, PaymentService>();

            // Register the ProductBrandRepository for dependency injection and associate it with the IProductBrandRepository interface.
            services.AddAutoMapper(typeof(MappingProfiles));

            // Configuring the behavior of the API
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Setting the response for invalid model state
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // Getting all the errors from the model state
                    var errors = actionContext
                        .ModelState.Where(e => e.Value != null && e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    // Creating a response object with the errors
                    var errorResponse = new ApiValidationErrorResponse { Errors = errors };

                    // Returning a BadRequest response with the error response object
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services; // Returning the modified services after configuring the API behavior
        }
    }
}
