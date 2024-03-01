using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    // Class for handling exceptions in the middleware
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // Reference to the next middleware in the pipeline
        private readonly ILogger<ExceptionMiddleware> _logger; // Logger for logging exceptions
        private readonly IHostEnvironment _env; // Environment variable

        // Constructor for ExceptionMiddleware
        public ExceptionMiddleware(
            RequestDelegate next, // Injecting the next middleware in the pipeline
            ILogger<ExceptionMiddleware> logger, // Injecting the logger
            IHostEnvironment env // Injecting the environment variable
        )
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) // Invokes the middleware and provides the current HTTP context
        {
            try
            {
                await _next(context); // Call the next middleware in the pipeline
            }
            catch (Exception ex) // Catch any exceptions
            {
                _logger.LogError(ex, ex.Message); // Log the exception
                context.Response.ContentType = "application/json"; // Set the response content type to JSON
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Set the status code to 500 (Internal Server Error)

                var response = _env.IsDevelopment() // Create a response object based on the environment
                    ? new ApiException(
                        (int)HttpStatusCode.InternalServerError, // Set the status code to 500 (Internal Server Error)
                        ex.Message, // Set the exception message
                        ex.StackTrace?.ToString() // Set the exception stack trace
                    )
                    : new ApiException((int)HttpStatusCode.InternalServerError); // Set the status code to 500 (Internal Server Error)

                var options = new JsonSerializerOptions // Configure JSON serialization options
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Set the property naming policy to camelCase
                };

                var json = JsonSerializer.Serialize(response, options); // Serialize the response object to JSON
                await context.Response.WriteAsync(json); // Write the JSON response to the HTTP context
            }
        }
    }
}
