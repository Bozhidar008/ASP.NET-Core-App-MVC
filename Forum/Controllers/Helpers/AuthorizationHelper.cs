using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Helpers
{
    public class AuthorizationHelper
    {
        private readonly IUsersService usersService;

        public AuthorizationHelper(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public User TryGetUser(string username)
        {
            try
            {
                return this.usersService.GetByUsername(username);
            }
            catch (Exception)
            {
                throw new Exception("Invalid Username");
            }
        }
    }
}
