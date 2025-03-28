using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
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
            ActionResult<Teacher> result = _api.ListTeacherInfo(TeacherId);
            if (result.Result is NotFoundObjectResult)
            {
                Console.WriteLine("Error");
                ViewData["ErrorMessage"] = $"Teacher with ID {TeacherId} not found.";
                return View("~/Views/Teacher/Show.cshtml");
            }
            
            return View("~/Views/Teacher/Show.cshtml", ((OkObjectResult)result.Result).Value); 
            //first do not take result.Value -> this is null
        }
    }
}
