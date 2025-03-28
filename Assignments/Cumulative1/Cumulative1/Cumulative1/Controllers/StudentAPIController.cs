using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route(template: "ListAllStudentInfo")]
        public List<Student> ListAllStudentInfo()
        {
            List<Student> Students = new List<Student>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM students";

                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {
                        int Id = ResultSet.GetInt32("studentid");
                        string? FirstName = ResultSet["studentfname"].ToString();
                        string? LastName = ResultSet["studentlname"].ToString();
                        string? StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime? EnrollDate = ResultSet.GetDateTime("enroldate");

                        Students.Add(new Student
                        {
                            Id = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            StudentNumber = StudentNumber,
                            EnrollDate = EnrollDate,
                        });
                    }
                }
            }
            return Students;
        }

        [HttpGet]
        [Route(template: "ListStudentInfo")]
        public ActionResult<Student> ListStudentInfo(int StudentId)
        {
            Student StudentInfo = null;
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //To prevent SQL Injection (REFERRED StackOverflow)
                Command.CommandText = "SELECT * FROM students WHERE studentid = @StudentId";
                Command.Parameters.AddWithValue("@StudentId", StudentId);


                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    if (ResultSet.Read())
                    {

                        int Id = ResultSet.GetInt32("studentid");
                        string? FirstName = ResultSet["studentfname"].ToString();
                        string? LastName = ResultSet["studentlname"].ToString();
                        string? StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime? EnrollDate = ResultSet.GetDateTime("enroldate");
                        StudentInfo = new Student
                        {
                            Id = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            StudentNumber = StudentNumber,
                            EnrollDate = EnrollDate
                        };
                    }
                }
                if (StudentInfo == null)
                {
                    return NotFound($"Teacher with ID {StudentId} not found.");
                }
            }
            return Ok(StudentInfo);
        }
    }
}
