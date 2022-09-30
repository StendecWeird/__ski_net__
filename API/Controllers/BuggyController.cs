using System.Net;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : ApiControllerBase
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            var product = _context.Products.Find(42);

            if(product == null)
                return NotFound(new ApiErrorResponse(HttpStatusCode.NotFound));

            return Ok(product);
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(42);

            var desc = product!.Description;

            return Ok(desc);
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(HttpStatusCode.BadRequest));
        }

        [HttpGet("not-validated/{id}")]
        public ActionResult GetNotValidated(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}