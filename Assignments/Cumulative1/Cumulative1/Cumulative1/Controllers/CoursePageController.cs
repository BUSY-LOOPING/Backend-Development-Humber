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

        /// <summary>
        /// Displays the Course Index view.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the Course index view.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/CoursePage/Index -> "Index view rendered"
        /// </example>
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Course/Index.cshtml");
        }

        /// <summary>
        /// Retrieves all courses from the API and displays them in the Course List view.
        /// </summary>
        /// <returns>
        /// An IActionResult containing a view with a list of Course objects.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/CoursePage/List -> "List View rendered"
        /// </example>
        [HttpGet]
        public IActionResult List()
        {
            List<Course> courses;
            courses = _api.ListAllCourseInfo();
            return View("~/Views/Course/List.cshtml", courses);
        }

        /// <summary>
        /// Displays the Course Search view where a course can be looked up by its ID.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the Course search form view.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/CoursePage/Show -> "Search form rendered"
        /// </example>
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
