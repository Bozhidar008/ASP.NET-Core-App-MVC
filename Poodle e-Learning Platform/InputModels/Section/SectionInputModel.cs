using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.InputModels.Section
{
    public class SectionInputModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; } 
    }
}
