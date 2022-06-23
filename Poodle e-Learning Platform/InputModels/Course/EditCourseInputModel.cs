using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.InputModels.Course
{
    public class EditCourseInputModel : CourseInputModel
    {
        public int CourseId { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
    }
}
