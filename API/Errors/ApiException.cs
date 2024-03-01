namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        // Create a constructor for ApiException that takes in statusCode, message, and details
        public ApiException(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message) // Call the base class constructor with statusCode and message
        {
            Details = details; // Set the Details property to the provided details
        }

        public string? Details { get; set; } // Define a property Details that can hold a string value
    }
}
