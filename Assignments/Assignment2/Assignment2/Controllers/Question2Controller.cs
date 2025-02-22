using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J1")]
    [ApiController]
    public class Question2Controller : ControllerBase
    {
        /// <summary>
        /// Calculates the happiness of Barley based on the number of small, medium, and large treats.
        /// </summary>
        /// <param name="SmallTreats">The number of small treats given to Barley.</param>
        /// <param name="MediumTreats">The number of medium treats given to Barley.</param>
        /// <param name="LargeTreats">The number of large treats given to Barley.</param>
        /// <returns>A string indicating whether Barley is "happy" or "sad" based on the total happiness score.</returns>
        /// <example>
        /// GET /api/J1/BarleyHappiness?SmallTreats=2&MediumTreats=3&LargeTreats=1 -> "happy"
        /// </example>
        /// <example>
        /// GET /api/J1/BarleyHappiness?SmallTreats=2&MediumTreats=3&LargeTreats=0 -> "sad"
        /// </example>
        [HttpGet(template:"BarleyHappiness")]
        public string GetHappiness([FromQuery] int SmallTreats,[FromQuery] int MediumTreats,[FromQuery] int LargeTreats)
        {
            int happinessScore = 1 * SmallTreats + 2 * MediumTreats + 3 * LargeTreats;
            return happinessScore >= 10 ? "happy" : "sad";
        }
    }
}
