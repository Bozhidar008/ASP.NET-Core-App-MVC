using System.ComponentModel.DataAnnotations;

namespace Poodle_e_Learning_Platform.Contracts
{
    public interface User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string ProfilePicture { get; set; }


    }
}
