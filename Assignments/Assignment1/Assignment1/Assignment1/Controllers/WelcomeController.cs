using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q1/[controller]")]
    [ApiController]
    public class WelcomeController : Controller
    {
        /// <summary>
        ///  Question1: Returns a welcome message
        /// </summary>
        /// <returns>
        ///  String: Welcome to 5125!
        /// </returns>
        /// <param>None</param>
        [HttpGet]
        public string Get() 
        {
            return "Welcome to 5125!";
        }
        
        
    }
}
