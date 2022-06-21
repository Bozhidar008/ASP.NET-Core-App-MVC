using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.InputModels.ApplicationUser
{
    public class ApplicationUserInputModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile Image{ get; set; }
    }
}
