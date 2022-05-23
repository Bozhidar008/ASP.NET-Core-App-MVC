using System.Linq;

namespace Poodle_e_Learning_Platform.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } // = username
        public string Password { get; set; }

        public IQueryable<Course> Courses { get; set; } // List/IEnumerable?? where to initialize? teacher create courses  but need list??
        public string ProfilePicture { get; set; }
        public string EmailConfirmed { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
