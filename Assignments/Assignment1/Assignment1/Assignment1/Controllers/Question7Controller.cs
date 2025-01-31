using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q7")]
    [ApiController]
    public class Question7Controller : ControllerBase
    {
        /// <summary>
        /// Returns the date adjusted by the specified number of days from today.
        /// </summary>
        /// <param name="days">The number of days to adjust the current date by. Can be positive or negative.</param>
        /// <returns>The adjusted date in the format "yyyy-MM-dd".</returns>
        /// <remarks>
        /// This method calculates the date by adding the specified number of days to the current date.
        /// </remarks>
        /// <example>
        /// If today's date is 2025-01-30 and the number of days is 5, the adjusted date will be 2025-02-04.
        /// If today's date is 2025-01-30 and the number of days is -3, the adjusted date will be 2025-01-27.
        /// </example>
        [HttpGet(template:"timemachine")]
        public string getAdjustedDate([FromQuery] double days)
        {
            DateTime today = DateTime.Now;
            DateTime adjustedDate = today.AddDays(days);
            return adjustedDate.ToString("yyyy-MM-dd");
        }
    }
}
