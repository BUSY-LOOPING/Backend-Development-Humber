using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q4")]
    [ApiController]
    public class KnockKnockController : ControllerBase
    {
        /// <summary>
        /// Returns the response to a "knock knock" joke.
        /// </summary>
        /// <returns>A string response "Who's there?".</returns>
        /// <remarks>
        /// This method returns a standard response to the "knock knock" joke setup.
        /// </remarks>
        /// <example>
        /// http://localhost:xx/api/q4/knockknock -> 'Who’s there?'
        /// </example>

        [HttpGet(template: "knockknock")]
        public string getKnockKnock()
        {
            return "Who's there?";
        }
    }
}
