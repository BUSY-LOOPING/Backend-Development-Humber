using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q5")]
    [ApiController]
    public class Question5Controller : ControllerBase
    {
        /// <summary>
        /// This method takes in a secret code from the request header and returns it in a correctly formatted output string
        /// </summary>
        /// <param name="secret">Integer parameter that we want to be displayed in the output string, taken from the request header.</param>
        /// <returns>String formatted with the secret code.</returns>
        /// <remarks>
        /// This method is mapped to the HTTP POST request with the route "api/q5/secret".
        /// The secret code is expected to be provided in the request header.
        /// </remarks>
        /// <example>
        /// POST http://localhost:xx/api/q5/secret 
        /// Body: 123
        /// Response: 'Shh.. the secret is 123'
        /// </example>

        [HttpPost(template: "secret")]
        public string getSecret([FromBody] int secret)
        {
            return $"Shh.. the secret is {secret}";
        }

    }
}
