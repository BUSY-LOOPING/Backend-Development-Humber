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

    }
}
