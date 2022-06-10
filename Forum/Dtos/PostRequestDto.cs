using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class PostRequestDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [StringLength(64, MinimumLength = 16, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [StringLength(8192, MinimumLength = 32, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Content { get; set; }

    }
}
