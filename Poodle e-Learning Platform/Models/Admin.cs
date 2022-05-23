namespace Poodle_e_Learning_Platform.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string EmailConfirmed { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
