using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoopController : ControllerBase
    {
        [HttpGet("whileLoopExample")]
        public string whileLoopExample(int ceiling)
        {
            string message = "";
            int start = 1;
            int iterator = start;
            while (iterator <= ceiling)
            {
                message += iterator.ToString();
                if (iterator != ceiling)
                {
                    message += ", ";
                }
                iterator += 1;
            }

            return message;
        }
    }
}
