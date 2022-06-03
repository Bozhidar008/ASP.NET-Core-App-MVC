using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Poodle_e_Learning_Platform.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } // UNIQUE

        [Required]
        public string Description { get; set; }

        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool IsPrivate { get; set; }

        public virtual IEnumerable<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

        public bool IsStudentEnrolled { get; set; } // student is enrolled with entering course page . "unenroll" from course button on page is needed -> back to main private page
        // "add section" link only visible for teachers on page
        public DateTime Created { get; set; } //?? may be not needed
        public DateTime StartDate { get; set; } // ??
        public DateTime EndDate { get; set; } //??
        public virtual IEnumerable<SectionPage> Sections { get; set; } = new HashSet<SectionPage>();


    }
}
