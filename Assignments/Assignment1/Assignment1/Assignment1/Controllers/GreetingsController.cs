using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q2")]  
    [ApiController]
    public class GreetingsController : ControllerBase
    {

        /// <summary>
        /// Returns a greeting message with the given name.
        /// </summary>
        /// <param name="name">The name to be included in the greeting message.</param>
        /// <returns>A greeting message that includes the provided name.</returns>
        /// <remarks>
        /// This method returns a greeting message that includes the name provided as a query parameter.
        /// </remarks>
        /// <example>
        /// GET http://localhost:xx/api/q2/greeting?name=John -> 'Hi John!'
        /// </example>
        [HttpGet(template: "greeting")]
        public string Get([FromQuery] string name)
        {
            return $"Hi {name}!";
        }
    }
}
