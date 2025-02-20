using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J1")]
    [ApiController]
    public class Question1Controller : ControllerBase
    {
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
