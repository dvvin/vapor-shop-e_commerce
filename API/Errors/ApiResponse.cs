namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode; // Initializes the StatusCode property with the provided value

            // Initializes the Message property with the provided value, or a default message if none is provided
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; } // Property to store the status code of the response

        public string? Message { get; set; } // Property to store the message of the response

        // Private method to get the default message for a given status code
        private static string? GetDefaultMessageForStatusCode(int statusCode)
        {
            // Returns a default message based on the provided status code
            return statusCode switch
            {
                400 => "Bad request",
                401 => "Unauthorized",
                404 => "Resource not found",
                500 => "Errors from the server",
                _ => null
            };
        }
    }
}
