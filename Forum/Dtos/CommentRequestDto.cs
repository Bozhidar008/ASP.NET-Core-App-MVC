using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class CommentRequestDto
    {
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [StringLength(8192, MinimumLength = 32, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}
