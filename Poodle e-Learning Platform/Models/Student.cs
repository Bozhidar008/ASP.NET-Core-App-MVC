using System.Linq;

namespace Poodle_e_Learning_Platform.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; } // = username

        public string Password { get; set; }

        public string ProfilePicture { get; set; }

        public IQueryable<Course> Courses { get; set; } // List/IEnumerable?? where to initialize? student courses in list?

        public string EmailConfirmed { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}
