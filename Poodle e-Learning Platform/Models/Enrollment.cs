namespace Poodle_e_Learning_Platform.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public double Grade { get; set; } // optional with class Results for grade display
    }
}
