using System;

namespace Poodle_e_Learning_Platform.Exceptions
{
    public class AuthenticationException : ApplicationException
    {
        public AuthenticationException()
        {

        }
        public AuthenticationException(string message) : base(message)
        {

        }
    }
}
