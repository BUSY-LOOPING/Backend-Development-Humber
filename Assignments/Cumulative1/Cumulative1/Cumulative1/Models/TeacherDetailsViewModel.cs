namespace Cumulative1.Models
{
    public class TeacherDetailsViewModel
    {
        public required Teacher Teacher { get; set; }
        public required List<Course> Courses { get; set; }
    }

}
