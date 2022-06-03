using Microsoft.AspNetCore.Identity;
using Poodle_e_Learning_Platform.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set ; }
        
        public string ProfilePicture { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; } = new HashSet<Course>();

        public virtual IEnumerable<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

        public virtual IEnumerable<IdentityUserRole<int>> UserRoles { get; set; } = new HashSet<IdentityUserRole<int>>();
    }
}
