using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class UserRequestDto
    {
		[Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
		[StringLength(32, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string FirstName { get; set; }

		[StringLength(32, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
		public string LastName{ get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }


	}
}
