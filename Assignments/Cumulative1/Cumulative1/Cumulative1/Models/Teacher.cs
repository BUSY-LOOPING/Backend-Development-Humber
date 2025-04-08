namespace Cumulative1.Models
{
    /// <summary>
    /// Represents a teacher in the school system.
    /// </summary>
    public class Teacher
    {
        /// Gets or sets the unique identifier for the teacher.
        public int Id { get; set; }

        /// Gets or sets the first name of the teacher.
        public string? FirstName { get; set; }

        /// Gets or sets the last name of the teacher.
        public string? LastName { get; set; }

        /// Gets or sets the unique employee number of the teacher.
        public string? EmployeeNumber { get; set; }

        /// Gets or sets the hire date of the teacher.
        public DateTime? HireDate { get; set; }

        /// Gets or sets the salary of the teacher.
        public decimal? Salary { get; set; }
    }
}
