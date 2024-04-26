using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    // This class contains extension methods for IServiceCollection and IApplicationBuilder related to Swagger documentation
    public static class SwaggerServiceExtensions
    {
        // Adds Swagger documentation to the specified IServiceCollection
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            // Configures Swagger generation with a specific API version and title
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vapor Shop API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement {{securitySchema, new[] {"Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }

        // Uses Swagger documentation in the specified IApplicationBuilder
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(); // Enables the Swagger middleware

            // Configures the Swagger user interface to display the specified API version and title
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vapor Shop API v1")
            );

            return app;
        }
    }
}
