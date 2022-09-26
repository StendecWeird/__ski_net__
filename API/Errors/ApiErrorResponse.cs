using System.Net;
namespace API.Errors
{
    public class ApiErrorResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public ApiErrorResponse(HttpStatusCode statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string GetDefaultMessage(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "Bad Request",
                HttpStatusCode.NotFound => "Not Found",
                HttpStatusCode.InternalServerError => "Sorry, something was wrong",
                _ => "Not Supported"
            };
        }
    }
}