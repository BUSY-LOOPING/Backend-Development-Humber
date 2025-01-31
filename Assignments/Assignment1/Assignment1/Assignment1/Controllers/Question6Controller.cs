using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q6")]
    [ApiController]
    public class Question6Controller : ControllerBase
    {
        /// <summary>
        /// Returns the area of a regular hexagon with the given side length.
        /// </summary>
        /// <param name="side">The length of a side of the hexagon. Must be greater than 0.</param>
        /// <returns>The area of the hexagon calculated using the formula (3 * sqrt(3) * side^2) / 2.</returns>
        /// <remarks>
        /// This method calculates the area of a regular hexagon using the formula:
        /// (3 * sqrt(3) * S^2) / 2, where S is the side length of the hexagon.
        /// </remarks>
        /// <example>
        /// if the side length is 4, the area is calculated as:
        /// (3 * sqrt(3) * 4^2) / 2 = 41.569.
        /// </example>
        [HttpGet(template: "hexagon")]
        public double getHexagonArea([FromQuery] double side)
        {
            return (3 * Math.Sqrt(3) * side * side) / 2;
        }
    }
}
