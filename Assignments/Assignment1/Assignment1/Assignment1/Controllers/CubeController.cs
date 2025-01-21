using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q3/cube")]
    [ApiController]
    public class CubeController : ControllerBase
    {
        [HttpGet("{base_}")]
        public int Cube(int base_) //base is reserved keyword
        {
            return base_ * base_ * base_;
        }

        //[HttpGet("{base_}")]
        //public IActionResult Cube(int base_) //This return type is more flexible (allows for error)
        //{
        //    return Ok(base_ * base_ * base_);
        //}
    }
}
