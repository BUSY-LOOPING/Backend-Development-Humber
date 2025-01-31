using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q1/")]
    [ApiController]
    public class WelcomeController : Controller
    {
        /// <summary>
        /// Question1: Returns a welcome message.
        /// </summary>
        /// <returns>
        /// String: Welcome to 5125!
        /// </returns>
        /// <remarks>
        /// This method returns a welcome message.
        /// </remarks>
        /// <example>
        /// GET http://localhost:xx/api/q1/welcome -> 'Welcome to 5125!'
        /// </example>


        [HttpGet(template:"welcome")]
        public string Get() 
        {
            return "Welcome to 5125!";
        }
        
        
    }
}
