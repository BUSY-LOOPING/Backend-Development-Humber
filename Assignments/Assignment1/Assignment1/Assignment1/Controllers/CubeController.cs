using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q3")]
    [ApiController]
    public class CubeController : ControllerBase
    {
        /// <summary>
        /// Returns the cube of the given base number.
        /// </summary>
        /// <param name="base_">The base number to be cubed. Must be an integer.</param>
        /// <returns>The cube of the base number.</returns>
        /// <remarks>
        /// This method calculates the cube of the given base number by multiplying the number by itself three times.
        /// </remarks>
        /// <example>
        /// https://localhost:xx/api/q3/cube/3" -> 27 .
        /// https://localhost:xx/api/q3/cube/-4" -> -64 .
        /// </example>

        [HttpGet("cube/{base_}")]
        public int Cube(int base_) //base is reserved keyword
        {
            return base_ * base_ * base_;
        }
    }
}
