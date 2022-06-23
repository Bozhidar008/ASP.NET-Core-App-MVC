using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.InputModels.Section
{
    public class EditSectionInputModel : SectionInputModel
    {
        public string CourseTitle { get; set; }

        public int SectionId { get; set; }

        [Display(Name="Order")]
        public int Order { get; set; }
    }
}
