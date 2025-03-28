using System.Data;
using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route(template: "ListAllCourseInfo")]
        public List<Course> ListAllCourseInfo()
        {
            List<Course> Courses = new List<Course>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM courses";

                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {
                        int Id = ResultSet.GetInt32("courseid");
                        string? CourseCode = ResultSet["coursecode"].ToString();
                        int? TeacherId = ResultSet.GetInt32("teacherid");
                        DateTime? StartDate = ResultSet.GetDateTime("startdate");
                        DateTime? FinishDate = ResultSet.GetDateTime("finishdate");
                        string? CourseName = ResultSet["coursename"].ToString();

                        Courses.Add(new Course
                        {
                            Id = Id,
                            CourseCode = CourseCode,
                            TeacherId = TeacherId,
                            StartDate = StartDate,
                            FinishDate = FinishDate,
                            CourseName = CourseName
                        });
                    }
                }
            }
            return Courses;
        }

        [HttpGet]
        [Route(template: "ListCourseInfo")]
        public ActionResult<Course> ListCourseInfo(int CourseId)
        {
            Course CourseInfo = null;
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //To prevent SQL Injection (REFERRED StackOverflow)
                Command.CommandText = "SELECT * FROM courses WHERE courseid = @CourseId";
                Command.Parameters.AddWithValue("@CourseId", CourseId);


                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    if (ResultSet.Read())
                    {

                        int Id = ResultSet.GetInt32("courseid");
                        string? CourseCode = ResultSet["coursecode"].ToString();
                        int? TeacherId = ResultSet.GetInt32("teacherid");
                        DateTime? StartDate = ResultSet.GetDateTime("startdate");
                        DateTime? FinishDate = ResultSet.GetDateTime("finishdate");
                        string? CourseName = ResultSet["coursename"].ToString();

                        CourseInfo = new Course
                        {
                            Id = Id,
                            CourseCode = CourseCode,
                            TeacherId = TeacherId,
                            StartDate = StartDate,
                            FinishDate = FinishDate,
                            CourseName = CourseName
                        };
                    }
                }
                if (CourseInfo == null)
                {
                    return NotFound($"Teacher with ID {CourseId} not found.");
                }
            }
            return Ok(CourseInfo);
        }
    }
}
