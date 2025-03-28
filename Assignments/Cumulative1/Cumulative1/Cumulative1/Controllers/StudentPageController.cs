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

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Student/Index.cshtml");
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Student> students;
            students = _api.ListAllStudentInfo();
            return View("~/Views/Student/List.cshtml", students);
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View("~/Views/Student/Show.cshtml");
        }

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
