using System.Net;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ApiControllerBase
    {
        public IActionResult Error(HttpStatusCode code) => new ObjectResult(new ApiErrorResponse(code)){ StatusCode = (int)code };
    }
}