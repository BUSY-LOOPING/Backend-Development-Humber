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


        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teacher/Index.cshtml");
        }

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

        [HttpGet]
        public IActionResult Show()
        {
            return View("~/Views/Teacher/Show.cshtml");
        }

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
