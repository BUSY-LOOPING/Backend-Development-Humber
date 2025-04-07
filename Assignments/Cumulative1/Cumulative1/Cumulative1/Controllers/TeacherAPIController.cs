using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all teachers from the database.
        /// </summary>
        /// <returns>
        /// A list of Teacher objects representing all teachers.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/api/Teacher/ListAllTeacherInfo -> [{...}, {...}, ...]
        /// </example>
        [HttpGet]
        [Route(template: "ListAllTeacherInfo")]
        public List<Teacher> ListAllTeacherInfo()
        {
            List<Teacher> Teachers = new List<Teacher>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM teachers";

                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {
                        int Id = ResultSet.GetInt32("teacherid");
                        string? FirstName = ResultSet["teacherfname"].ToString();
                        string? LastName = ResultSet["teacherlname"].ToString();
                        string? EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime? HireDate = ResultSet.GetDateTime("hiredate");
                        decimal? Salary = ResultSet.GetDecimal("salary");

                        Teachers.Add(new Teacher
                        {
                            Id = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            EmployeeNumber = EmployeeNumber,
                            HireDate = HireDate,
                            Salary = Salary
                        });
                    }
                }
            }
            return Teachers;
        }

        /// <summary>
        /// Retrieves a list of teachers whose hire dates fall between the specified start and end dates.
        /// </summary>
        /// <param name="FilterDateStart">The start date for filtering teacher hire dates.</param>
        /// <param name="FilterDateEnd">The end date for filtering teacher hire dates.</param>
        /// <returns>
        /// An ActionResult containing a list of Teacher objects if found; otherwise, a NotFound result with a message.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/api/Teacher/ListAllTeacherInfoBtwDates?FilterDateStart=2016-01-01&FilterDateEnd=2017-12-03 -> [{...}, {...}, ...]
        /// </example>
        [HttpGet]
        [Route(template: "ListAllTeacherInfoBtwDates")]
        public ActionResult<List<Teacher>> ListAllTeacherInfoBtwDates(DateTime FilterDateStart, DateTime FilterDateEnd)
        {
            List<Teacher> Teachers = new List<Teacher>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                // SQL Query with filtering by hiredate between dates.
                Command.CommandText = "SELECT * FROM teachers WHERE hiredate BETWEEN @FilterDateStart AND @FilterDateEnd";
                Command.Parameters.AddWithValue("@FilterDateStart", FilterDateStart);
                Command.Parameters.AddWithValue("@FilterDateEnd", FilterDateEnd);

                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {
                        int Id = ResultSet.GetInt32("teacherid");
                        string? FirstName = ResultSet["teacherfname"].ToString();
                        string? LastName = ResultSet["teacherlname"].ToString();
                        string? EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime? HireDate = ResultSet.GetDateTime("hiredate");
                        decimal? Salary = ResultSet.GetDecimal("salary");

                        Teachers.Add(new Teacher
                        {
                            Id = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            EmployeeNumber = EmployeeNumber,
                            HireDate = HireDate,
                            Salary = Salary
                        });
                    }
                }
            }
            if (Teachers.Count == 0)
            {
                return NotFound("No entries found.");
            }
            return Ok(Teachers);
        }

        /// <summary>
        /// Retrieves information for a specific teacher by the given TeacherId.
        /// </summary>
        /// <param name="TeacherId">The unique identifier of the teacher.</param>
        /// <returns>
        /// An ActionResult containing the Teacher object if found; otherwise, a NotFound result with an error message.
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/api/Teacher/ListTeacherInfo?TeacherId=1 -> {...}
        /// </example>
        [HttpGet]
        [Route(template: "ListTeacherInfo")]
        public ActionResult<Teacher> ListTeacherInfo(int TeacherId)
        {
            Teacher TeacherInfo = null;
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open a connection
                Connection.Open();

                //Establish a new command
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                //Command.CommandText = "SELECT * FROM teachers WHERE teacherid = " + TeacherId;

                //To prevent SQL Injection (REFERRED StackOverflow)
                Command.CommandText = "SELECT * FROM teachers WHERE teacherid = @TeacherId";
                Command.Parameters.AddWithValue("@TeacherId", TeacherId);


                //Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    if (ResultSet.Read())
                    {

                        int Id = ResultSet.GetInt32("teacherid");
                        string? FirstName = ResultSet["teacherfname"].ToString();
                        string? LastName = ResultSet["teacherlname"].ToString();
                        string? EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime? HireDate = ResultSet.GetDateTime("hiredate");
                        decimal? Salary = ResultSet.GetDecimal("salary");
                        TeacherInfo = new Teacher
                        {
                            Id = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            EmployeeNumber = EmployeeNumber,
                            HireDate = HireDate,
                            Salary = Salary
                        };
                    }
                }
                if (TeacherInfo == null)
                {
                    return NotFound($"Teacher with ID {TeacherId} not found.");
                }
            }
            return Ok(TeacherInfo);
        }

        [HttpPost]
        [Route("AddTeacher")]
        public ActionResult<Teacher> AddTeacher([FromForm] Teacher teacher)
        {
            try
            {
                using (MySqlConnection connection = _context.AccessDatabase())
                {
                    connection.Open();

                    // Check if a teacher with the same EmployeeNumber already exists
                    MySqlCommand checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = "SELECT COUNT(*) FROM teachers WHERE employeenumber = @EmployeeNumber";
                    checkCommand.Parameters.AddWithValue("@EmployeeNumber", teacher.EmployeeNumber);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                    {
                        return Conflict($"A teacher with Employee Number {teacher.EmployeeNumber} already exists.");
                    }

                    // Insert new teacher
                    MySqlCommand insertCommand = connection.CreateCommand();
                    insertCommand.CommandText =
                        @"INSERT INTO teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) 
              VALUES (@TeacherId, @FirstName, @LastName, @EmployeeNumber, @HireDate, @Salary)";
                    insertCommand.Parameters.AddWithValue("@TeacherId", teacher.Id);
                    insertCommand.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    insertCommand.Parameters.AddWithValue("@LastName", teacher.LastName);
                    insertCommand.Parameters.AddWithValue("@EmployeeNumber", teacher.EmployeeNumber);
                    insertCommand.Parameters.AddWithValue("@HireDate", teacher.HireDate);
                    insertCommand.Parameters.AddWithValue("@Salary", teacher.Salary);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Return 201 == Created successfully
                        // return CreatedAtAction(nameof(ListTeacherInfo), new { TeacherId = teacher.Id }, teacher);
                        return StatusCode(201, teacher);
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while adding the teacher.");
                    }
                }
            }catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route(template:"DeleteTeacher")]
        public ActionResult DeleteTeacher(int Id)
        {
            try
            {
                using (MySqlConnection connection = _context.AccessDatabase())
                {
                    connection.Open();

                    // Check if the teacher exists
                    MySqlCommand checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = "SELECT COUNT(*) FROM teachers WHERE teacherid = @TeacherId";
                    checkCommand.Parameters.AddWithValue("@TeacherId", Id);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        return NotFound($"No teacher found with ID {Id}.");
                    }
                        
                    // Delete the teacher
                    MySqlCommand deleteCommand = connection.CreateCommand();
                    deleteCommand.CommandText = "DELETE FROM teachers WHERE teacherid = @TeacherId";
                    deleteCommand.Parameters.AddWithValue("@TeacherId", Id);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Teacher with ID {Id} has been deleted."); //status code 200. can also use status code 204
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while deleting the teacher.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }
    }
}
