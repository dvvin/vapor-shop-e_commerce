using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Define the route for the controller and specify to ignore it in the API explorer
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        // Define the Error action method which takes an integer code as a parameter
        public IActionResult Error(int code)
        {
            // Return an ObjectResult with a new instance of ApiResponse using the provided code
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
