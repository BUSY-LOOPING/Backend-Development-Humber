using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q4/")]
    [ApiController]
    public class KnockKnockController : ControllerBase
    {
        [HttpGet(template: "knockknock")]
        public string getKnockKnock()
        {
            return "Who's there?";
        }
    }
}
