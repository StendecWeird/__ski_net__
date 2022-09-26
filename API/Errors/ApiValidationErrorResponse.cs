using System.Net;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiValidationErrorResponse(IEnumerable<string> errors) : base(HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }
    }
}