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

        /// <summary>
        /// Retrieves a list of all courses from the database.
        /// </summary>
        /// <returns>A list of Course objects representing all courses.</returns>
        /// <example>
        /// GET: https://localhost:xx/api/Course/ListAllCourseInfo -> [{...}, {...}, ...]
        /// </example>
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

        /// <summary>
        /// Retrieves detailed information about a specific course based on the provided course ID.
        /// </summary>
        /// <param name="CourseId">The unique identifier of the course.</param>
        /// <returns>An ActionResult object containing the Course object if found;
        /// otherwise, a NotFound result.</returns>
        /// <example>
        /// GET: https://localhost:xx/api/Course/ListCourseInfo?CourseId=2 -> {"id":2,"courseCode":"http5102",...}
        /// </example>
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

        /// <summary>
        /// Retrieves a list of courses taught by a specific teacher.
        /// </summary>
        /// <param name="TeacherId">The unique identifier of the teacher.</param>
        /// <returns>
        /// An ActionResult containing a list of Course objects
        /// representing the courses taught by the specified teacher.
        /// </returns>
        /// <example>
        /// <code>
        /// GET: https://localhost:xx/api/Course/ListCoursesByTeacherId?TeacherId=1
        /// </code>
        /// </example>
        [HttpGet]
        public ActionResult<List<Course>> ListCoursesByTeacherId(int TeacherId)
        {
            List<Course> Courses = new List<Course>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM courses WHERE teacherid = @TeacherId";
                Command.Parameters.AddWithValue("@TeacherId", TeacherId);

                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {
                        int Id = ResultSet.GetInt32("courseid");
                        string? CourseCode = ResultSet["coursecode"].ToString();
                        int? TeacherIdDB = ResultSet.GetInt32("teacherid");
                        DateTime? StartDate = ResultSet.GetDateTime("startdate");
                        DateTime? FinishDate = ResultSet.GetDateTime("finishdate");
                        string? CourseName = ResultSet["coursename"].ToString();

                        Courses.Add(new Course
                        {
                            Id = Id,
                            CourseCode = CourseCode,
                            TeacherId = TeacherIdDB,
                            StartDate = StartDate,
                            FinishDate = FinishDate,
                            CourseName = CourseName
                        });
                    }
                }
            }
            return Ok(Courses);
        }
    }
}
