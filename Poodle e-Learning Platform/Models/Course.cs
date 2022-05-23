using System;
using System.Linq;

namespace Poodle_e_Learning_Platform.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; } // UNIQUE

        public string Description { get; set; }

        public int TeacherId { get; set; }

        public bool IsPublic { get; set; } // default

        public bool IsPrivate { get; set; } // if private teachers must see "enroll students" link when at course page

        public int SectionId { get; set; }

        public virtual SectionPage sectionPage { get; set; }

        public int StudentId { get; set; }
        public bool IsStudentEnrolled { get; set; } // student is enrolled with enterering course page . "unenroll" from course button on page is needed -> back to main private page
        // "add section" link only visible for teachers on page
        public DateTime Created { get; set; } //?? may be not needed
        public DateTime StartDate { get; set; } // ??need to connect with  student access to course??
        public DateTime EndDate { get; set; } //??
        public IQueryable<SectionPage> Sections { get; set; } // where to initialize? Do I need it ?

    }
}
