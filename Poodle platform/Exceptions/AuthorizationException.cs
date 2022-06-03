using System;

namespace Poodle_e_Learning_Platform.Exceptions
{
    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message) : base(message)
        {

        }
    }
}
