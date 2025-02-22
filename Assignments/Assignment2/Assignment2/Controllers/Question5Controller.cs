using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J3")]
    [ApiController]
    public class Question5Controller : ControllerBase
    {
        /// <summary>
        /// Determines the best days for a special event based on the availability of participants.
        /// </summary>
        /// <param name="N">The number of participants.</param>
        /// <param name="availabilities">A list of strings representing the availability of each participant for 5 days. Each string contains 'Y' for available and '.' for not available.</param>
        /// <returns>A comma-separated string of the best days (1-5) for the event based on the highest availability.</returns>
        /// <example>
        /// GET https:localhost:xx/api/J3/SpecialEvent?N=3&availabilities=Y..YY,YY.YY,..YYY
        /// 
        /// Result: "4, 5"
        /// </example>
        [HttpGet(template:"SpecialEvent")]
        public string GetDayNumber([FromQuery] int N, [FromQuery] List<string> availabilities)
        {
            int[] scores = new int[5];
            for (int j = 0; j < N; j++)
            {
                string availability = availabilities[j];
                for (int i = 0; i < 5; i++)
                {
                    if (availability[i] == 'Y')
                    {
                        scores[i]++;
                    }
                }
            }
            int maxValue = scores.Max();
            if (maxValue == 0)
            {
                return "";
            }
            var maxIndexes = scores //linq method referenced from stackoverflow
                .Select((value, index) => new { value, index })
                .Where(pair => pair.value == maxValue)
                .Select(pair => pair.index + 1)
                .ToList();
            return string.Join(", ", maxIndexes);
        }
    }
}
