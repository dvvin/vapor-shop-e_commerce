namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        // Constructor for ApiValidationErrorResponse class, setting the status code to 400
        public ApiValidationErrorResponse()
            : base(400) { }

        // Property to store a collection of error messages, with the option for null values
        public IEnumerable<string>? Errors { get; set; }
    }
}
