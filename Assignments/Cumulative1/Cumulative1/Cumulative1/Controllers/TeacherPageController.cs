using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        private readonly CourseAPIController _courseAPI;
        public TeacherPageController(TeacherAPIController api, CourseAPIController courseAPI)
        {
            _api = api;
            _courseAPI = courseAPI;
        }

        /// <summary>
        /// Displays the Teacher index view.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the Teacher index view.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/TeacherPage/Index -> "Index view rendered"
        /// </example>
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teacher/Index.cshtml");
        }

        /// <summary>
        /// Retrieves a list of teachers, optionally filtered by a hire date range, and displays them in the Teacher List view.
        /// </summary>
        /// <param name="startDate">The optional start date to filter teachers by their hire date.</param>
        /// <param name="endDate">The optional end date to filter teachers by their hire date.</param>
        /// <returns>
        /// An IActionResult containing a view with a list of Teacher objects.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/TeacherPage/List?startDate=2020-01-01&endDate=2020-12-31 -> "Rendered List Page"
        /// </example>
        [HttpGet]
        public IActionResult List(DateTime? startDate, DateTime? endDate)
        {
            List<Teacher> teachers = new List<Teacher>();

            if (startDate.HasValue && endDate.HasValue)
            {
                ActionResult<List<Teacher>> result = _api.ListAllTeacherInfoBtwDates(startDate.Value, endDate.Value);

                if (result.Result is NotFoundObjectResult)
                {
                    ViewData["ErrorMessage"] = $"No entries found between {startDate.Value.ToShortDateString()} and {endDate.Value.ToShortDateString()}.";
                }
                else if (result.Result is OkObjectResult okResult && okResult.Value is List<Teacher> teacherList)
                {
                    teachers = teacherList;
                }
            }
            else
            {
                teachers = _api.ListAllTeacherInfo();
            }

            return View("~/Views/Teacher/List.cshtml", teachers);
        }

        /// <summary>
        /// Displays the Teacher search view where a teacher can be looked up by their ID.
        /// </summary>
        /// <returns>
        /// AnIActionResult representing the Teacher search form view.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/TeacherPage/Show -> "Search form rendered"
        /// </example>
        [HttpGet]
        public IActionResult Show()
        {
            return View("~/Views/Teacher/Show.cshtml");
        }

        /// <summary>
        /// Retrieves detailed teacher information along with the list of courses taught by that teacher
        /// </summary>
        /// <param name="TeacherId">The unique identifier of the teacher to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the Teacher details view with associated courses if found; 
        /// otherwise, the search view with an error message.
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/TeacherPage/Show
        /// DATA: TeacherId=1
        /// </example>
        [HttpPost]
        public IActionResult Show(int TeacherId)
        {
            ActionResult<Teacher> teacherResult = _api.ListTeacherInfo(TeacherId);
            if (teacherResult.Result is NotFoundObjectResult)
            {
                Console.WriteLine("Error");
                ViewData["ErrorMessage"] = $"Teacher with ID {TeacherId} not found.";
                return View("~/Views/Teacher/Show.cshtml");
            }

            var teacher = ((OkObjectResult)teacherResult.Result).Value as Teacher;

            var coursesResult = _courseAPI.ListCoursesByTeacherId(TeacherId);
            var courses = ((OkObjectResult)coursesResult.Result).Value as List<Course>;

            var viewModel = new TeacherDetailsViewModel
            {
                Teacher = teacher,
                Courses = courses
            };

            return View("~/Views/Teacher/Show.cshtml", viewModel);
            //first do not take result.Value -> this is null
        }
    }
}
