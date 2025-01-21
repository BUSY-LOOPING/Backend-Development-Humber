using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q2/greeting")]  //this will change the endpoint from Greetings to greeting
    [ApiController]
    public class GreetingsController : ControllerBase
    {

        //this will work but the named parameter won't be work like https://url?name=dhruv
        //[HttpGet("{name}")]
        //public string Get(string name)
        //{
        //    return $"Hi {name}!";
        //}

        /// <summary>
        /// Question2: Returns the message along with the name
        /// </summary>
        /// <param name="name">name that we want to display</param>
        /// <returns>String: Greeting with the input name</returns>
        /// <remarks>FromRoute tells ASP.NET Core to bind the route parameter name from the URL to the name parameter in the method.</remarks>
        [HttpGet]
        public string Get([FromQuery] string name)
        {
            return $"Hi {name}!";
        }
    }
}
