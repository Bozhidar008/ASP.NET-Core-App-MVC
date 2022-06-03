using System;
using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.Models
{
    public class SectionPage
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } // UNIQUE

        [Required]
        public string Content { get; set; } // HTML ?? difference with text?

        public int Order { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; } // ??

        public int StudentId { get; set; }
        public virtual Student Student { get; set; } // ?? or StudentId??

        // do i need teacher who create it?
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public DateTime dateTime { get; set; } // restricted by date or for specific users or no restrictions??
    }
}
