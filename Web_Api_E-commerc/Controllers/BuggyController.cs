using Microsoft.AspNetCore.Mvc;
using Presistence.Data;

namespace Web_Api_E_commerc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Primary Constractor
    public class BuggyController(ApplicationDbcontext _dbcontext) : ControllerBase
    {
        [HttpGet("NotFound")] //Get : /api/Buggy/NotFound
        public ActionResult GetNotFountRequst()
        {
            var Product = _dbcontext.Products.Find(100);
            if (Product is null)
                return NotFound();
            else
                return Ok(Product);
        }

        [HttpGet("ServerError")] //Get : /api/Buggy/ServerError
        public ActionResult GetServerError()
        {
            var Product = _dbcontext.Products.Find(100);
            var ProductToReturn = Product.ToString();

            return Ok(Product);
        }

        [HttpGet("BadRequest ")] //Get : /api/Buggy/BadRequest 
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("Badrequst/{id}")] //Get : /api/Buggy/Badrequst/5
        public ActionResult GetValidationErorr(int id)
      => Ok();

        [HttpGet("unauthorized")] // GET: /api/Buggy/unauthorized
        public ActionResult GetUnauthorized(int id)
        {
            return Unauthorized(); // This will return a 401 Unauthorized response
        }

    }
}
