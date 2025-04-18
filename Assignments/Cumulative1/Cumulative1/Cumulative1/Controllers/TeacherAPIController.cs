using System.Text.RegularExpressions;
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

        /// <summary>
        /// Adds a new teacher to the database.
        /// </summary>
        /// <param name="FirstName">The first name of the teacher.</param>
        /// <param name="LastName">The last name of the teacher (optional).</param>
        /// <param name="EmployeeNumber">The unique employee number of the teacher.</param>
        /// <param name="HireDate">The hire date of the teacher (optional).</param>
        /// <param name="Salary">The salary of the teacher (optional).</param>
        /// <returns>
        /// An ActionResult indicating the result of the operation. Returns a 201 status code if the teacher is added successfully,
        /// a 409 status code if a teacher with the same employee number already exists, or a 500 status code if an error occurs.
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/api/Teacher/AddTeacher
        /// Body: { "FirstName": "John", "LastName": "Doe", "EmployeeNumber": "T123", "HireDate": "2025-04-07", "Salary": 30.55 }
        /// RESPONSE : New Teacher Added!
        /// </example>
        [HttpPost]
        [Route("AddTeacher")]
        public ActionResult AddTeacher(
            string? FirstName, 
            string? LastName,
            string? EmployeeNumber,
            DateTime? HireDate,
            decimal? Salary)
        {
            try
            {
                //CODE 400 : Bad Request
                if (string.IsNullOrEmpty(FirstName))
                {
                    return StatusCode(400, "Missing First Name!");
                }
                if (string.IsNullOrEmpty(EmployeeNumber))
                {
                    return StatusCode(400, "Missing Employee Number!");
                }
                if (!EmployeeNumber.StartsWith("T"))
                {
                    return StatusCode(400, "Employee Number must start with 'T'!");
                }
                if (!Regex.IsMatch(EmployeeNumber.Substring(1), @"^\d+$"))
                {
                    return StatusCode(400, "Employee Number must be followed by numeric digits only.");
                }
                if (Salary != null && !Regex.IsMatch(Salary.ToString(), @"^\d+(\.\d{1,2})?$"))
                {
                    return StatusCode(400, "Salary must only be in numeric!");
                }
                if (HireDate != null && HireDate.Value > DateTime.Now)
                {
                    return StatusCode(400, "Hire Date cannot be in future!");
                }



                    using (MySqlConnection connection = _context.AccessDatabase())
                {
                    connection.Open();

                    // Check if a teacher with the same EmployeeNumber already exists
                    MySqlCommand checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = "SELECT COUNT(*) FROM teachers WHERE employeenumber = @EmployeeNumber";
                    checkCommand.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                    {
                        return Conflict($"A teacher with Employee Number {EmployeeNumber} already exists.");
                    }

                    // Insert new teacher
                    MySqlCommand insertCommand = connection.CreateCommand();
                    insertCommand.CommandText =
                        @"INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) 
              VALUES (@FirstName, @LastName, @EmployeeNumber, @HireDate, @Salary)";
                    insertCommand.Parameters.AddWithValue("@FirstName", FirstName);
                    insertCommand.Parameters.AddWithValue("@LastName", LastName);
                    insertCommand.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);
                    insertCommand.Parameters.AddWithValue("@HireDate", HireDate);
                    insertCommand.Parameters.AddWithValue("@Salary", Salary);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Return 201 == Created successfully
                        return StatusCode(201, "New Teacher Added!");
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

        /// <summary>
        /// Deletes a teacher from the database based on the teacher ID.
        /// </summary>
        /// <param name="Id">The unique ID of the teacher to be deleted.</param>
        /// <returns>
        /// An ActionResult indicating the result of the operation. Returns a 200 status code if the teacher is deleted successfully,
        /// a 404 status code if no teacher with the specified ID is found, or a 500 status code if an error occurs.
        /// </returns>
        /// <example>
        /// DELETE: https://localhost:xx/api/Teacher/DeleteTeacher?Id=1
        /// RESPONSE : Teacher with ID 1 has been deleted.
        /// </example>
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

        /// <summary>
        /// Updates the details of an existing teacher in the database.
        /// </summary>
        /// <param name="teacher">A Teacher object containing the updated details of the teacher.</param>
        /// <returns>
        /// An ActionResult indicating the result of the operation. Returns:
        /// - A 200 status code if the teacher is updated successfully.
        /// - A 400 status code if any validation fails (e.g., missing required fields, invalid data).
        /// - A 404 status code if no teacher with the specified ID is found.
        /// - A 500 status code if an error occurs during the update process.
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/api/Teacher/UpdateTeacher
        /// Body: 
        /// {
        ///   "Id": 11,
        ///   "FirstName": "John",
        ///   "LastName": "Doe",
        ///   "EmployeeNumber": "T123",
        ///   "HireDate": "2020-01-01",
        ///   "Salary": 50.00
        /// }
        /// RESPONSE: Teacher with ID 11 updated successfully.
        [HttpPost]
        [Route(template: "UpdateTeacher")]
        public ActionResult UpdateTeacher([FromForm] Teacher teacher)
        {
            try
            {
                if (string.IsNullOrEmpty(teacher.FirstName))
                {
                    return StatusCode(400, "First Name is required.");
                }
                if (teacher.HireDate != null && teacher.HireDate.Value > DateTime.Now)
                {
                    return StatusCode(400, "Hire Date cannot be in future!");
                }
                if (teacher.Salary != null && teacher.Salary < 0)
                {
                    return StatusCode(400, "Salary cannot be negative!");
                }
                using (MySqlConnection connection = _context.AccessDatabase())
                {
                    connection.Open();

                    // Check if the teacher exists
                    MySqlCommand checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = "SELECT COUNT(*) FROM teachers WHERE teacherid = @TeacherId";
                    checkCommand.Parameters.AddWithValue("@TeacherId", teacher.Id);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        return NotFound($"No teacher found with ID {teacher.Id}.");
                    }

                    // Update the teacher
                    MySqlCommand updateCommand = connection.CreateCommand();
                    updateCommand.CommandText = @"
                        UPDATE teachers 
                        SET teacherfname = @FirstName, 
                            teacherlname = @LastName, 
                            employeenumber = @EmployeeNumber, 
                            hiredate = @HireDate, 
                            salary = @Salary 
                        WHERE teacherid = @TeacherId";

                    updateCommand.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    updateCommand.Parameters.AddWithValue("@LastName", teacher.LastName);
                    updateCommand.Parameters.AddWithValue("@EmployeeNumber", teacher.EmployeeNumber);
                    updateCommand.Parameters.AddWithValue("@HireDate", teacher.HireDate);
                    updateCommand.Parameters.AddWithValue("@Salary", teacher.Salary);
                    updateCommand.Parameters.AddWithValue("@TeacherId", teacher.Id);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Teacher with ID {teacher.Id} updated successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while updating the teacher.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong");
            }
        }

    }
}
