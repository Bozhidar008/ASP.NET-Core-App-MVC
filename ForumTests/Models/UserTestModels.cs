using System.Collections.Generic;
using WebApplication1.Models;

namespace WEbApplication1Tests.Models
{
    public class UserTestModels
    {
        public List<User> GetUsers()
        {
            return new List<User>()
            {
                new User
                {
                  Id = 1,
                  FirstName = "Georgi",
                  LastName = "Georgiev",
                  UserName = "Gosho",
                  Email = "georgi.georgiev@mail.com",
                  IsAdmin = true,
                  IsBlocked = false
                },
                new User
                {
                  Id = 2,
                  FirstName = "Petar",
                  LastName = "Petrov",
                  UserName = "pesho",
                  Email = "petar.petrov@mail.com",
                  IsAdmin = false,
                  IsBlocked = false               
                }
            };
        }      
    }
}
