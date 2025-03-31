using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;
        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        /// <summary>
        /// Displays the Student Index view.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the Student index view.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/StudentPage/Index -> "Index view rendered"
        /// </example>
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Student/Index.cshtml");
        }

        /// <summary>
        /// Retrieves all students from the API and displays them in the Student List view.
        /// </summary>
        /// <returns>
        /// An IActionResult containing a view with a list of Student objects.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/StudentPage/List -> "List View Rendered"
        /// </example>
        [HttpGet]
        public IActionResult List()
        {
            List<Student> students;
            students = _api.ListAllStudentInfo();
            return View("~/Views/Student/List.cshtml", students);
        }

        /// <summary>
        /// Displays the Student Search view where a student can be looked up by their ID.
        /// </summary>
        /// <returns>
        /// An IActionResult representing the Student search form view.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/StudentPage/Show -> "Search form rendered"
        /// </example>
        [HttpGet]
        public IActionResult Show()
        {
            return View("~/Views/Student/Show.cshtml");
        }

        /// <summary>
        /// Retrieves student information for the specified student ID and displays it in the Student Show view.
        /// </summary>
        /// <param name="StudentId">The unique identifier of the student to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the student details view if found; otherwise, the search view with an error message.
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/StudentPage/Show
        /// DATA : StudentId = 1
        /// </example>
        [HttpPost]
        public IActionResult Show(int StudentId)
        {
            ActionResult<Student> result = _api.ListStudentInfo(StudentId);
            if (result.Result is NotFoundObjectResult)
            {
                Console.WriteLine("Error");
                ViewData["ErrorMessage"] = $"Student with ID {StudentId} not found.";
                return View("~/Views/Student/Show.cshtml");
            }

            return View("~/Views/Student/Show.cshtml", ((OkObjectResult)result.Result).Value);
            //first do not take result.Value -> this is null
        }

    }
}
