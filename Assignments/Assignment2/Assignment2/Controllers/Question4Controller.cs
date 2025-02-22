using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J2")]
    [ApiController]
    public class Question4Controller : ControllerBase
    {
        /// <summary>
        /// Determines the first day on which the total number of people who have had the disease is greater than a given number.
        /// </summary>
        /// <param name="P">The total number of people to exceed.</param>
        /// <param name="N">The number of people who have the disease on Day 0.</param>
        /// <param name="R">The number of people each infected person infects the next day.</param>
        /// <returns>The number of the first day on which the total number of people who have had the disease is greater than P.</returns>
        /// <example>
        /// GET /api/J2/Epidemiology?P=750&N=1&R=5
        /// 
        /// Result: 4
        /// </example>
        [HttpGet(template:"Epidemiology")]
        public int GetDayNumber([FromQuery] int P, [FromQuery] int N, [FromQuery] int R)
        {
            int dayNumber = 0;
            int totalInfected = N;  
            int newInfected = N;    

            while (totalInfected <= P)
            {
                dayNumber++;
                newInfected *= R;  
                totalInfected += newInfected;  
            }

            return dayNumber;
        }
    }
}
