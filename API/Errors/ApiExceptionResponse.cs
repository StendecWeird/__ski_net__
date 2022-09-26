using System.Net;
namespace API.Errors
{
    public class ApiExceptionResponse : ApiErrorResponse
    {
        public string? Details { get; }

        public ApiExceptionResponse(
            HttpStatusCode statusCode, 
            string? message = null, 
            string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}