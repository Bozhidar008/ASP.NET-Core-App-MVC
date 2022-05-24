using Poodle_e_Learning_Platform.Models;

namespace Poodle_e_Learning_Platform.Helpers
{
    public class AuthorizationHelper
    {
        private const string AuthorizationErrorMessage = "Only admins can create, update or delete a teacher.";
        private const string AuthenticationErrorMessage = "Authentication failed. Wrong credentials.";
        private readonly ITeachersService teacherService;

        public AuthorizationHelper(ITeachersService teacherService)
        {
            this.teacherService = teacherService;
        }

        public Teacher tryGetTeacher(string email)
        {
            try
            {
                return this.teacherService.Get(email);
            }
            catch (EntityNotFoundException)
            {

                throw new AuthorizationException("Invalid email");
            }
        }
    }
}
