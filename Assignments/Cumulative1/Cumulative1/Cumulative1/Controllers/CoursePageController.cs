using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly CourseAPIController _api;
        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Course/Index.cshtml");
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Course> courses;
            courses = _api.ListAllCourseInfo();
            return View("~/Views/Course/List.cshtml", courses);
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View("~/Views/Course/Show.cshtml");
        }

        [HttpPost]
        public IActionResult Show(int CourseId)
        {
            ActionResult<Course> result = _api.ListCourseInfo(CourseId);
            if (result.Result is NotFoundObjectResult)
            {
                Console.WriteLine("Error");
                ViewData["ErrorMessage"] = $"Course with ID {CourseId} not found.";
                return View("~/Views/Course/Show.cshtml");
            }

            return View("~/Views/Course/Show.cshtml", ((OkObjectResult)result.Result).Value);
            //first do not take result.Value -> this is null
        }
    }
}
