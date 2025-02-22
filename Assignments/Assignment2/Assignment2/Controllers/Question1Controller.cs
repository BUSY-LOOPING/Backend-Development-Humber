using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J1")]
    [ApiController]
    public class Question1Controller : ControllerBase
    {
        /// <summary>
        /// Calculates the final score for a delivery droid based on the number of collisions and deliveries.
        /// </summary>
        /// <param name="Collisions">The number of collisions the droid had.</param>
        /// <param name="Deliveries">The number of successful deliveries made by the droid.</param>
        /// <returns>The final score calculated based on the given collisions and deliveries.</returns>
        /// <example>
        /// POST /api/J1/Delivedroid
        /// Content-Type: application/x-www-form-urlencoded
        /// 
        /// Collisions=2&Deliveries=10
        /// 
        /// Result: 980
        /// </example>
        [HttpPost(template: "Delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]
        public int FinalScore([FromForm] int Collisions, [FromForm] int Deliveries)
        {
            int score = (Deliveries * 50) + (Collisions * -10);
            score = Deliveries > Collisions ? score + 500: score;
            return score;
        }
    }
}
