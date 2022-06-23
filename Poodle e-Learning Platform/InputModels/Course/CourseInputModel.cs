using System;
using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.InputModels.Course
{
    public class CourseInputModel
    {
        [Required]
        [Display(Name = "CourseName")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} have to be between {2} and {1} chars")]
        public string Title { get; set; } // UNIQUE

        [Required]
        [Display(Name = "Description")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "{0} have to be between {2} and {1} chars")]
        public string Description { get; set; }

        public int ApplicationUserId { get; set; }

        [Display(Name = "Is private")]
        public bool IsPrivate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
