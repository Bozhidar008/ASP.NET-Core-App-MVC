using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {

        public User()
        {

        }

        public User(int id, string firstName, string lastName, string email,string userName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = userName;
        }
        public int Id { get; set; }

        public string UserName { get; set; }

      

        public string FirstName { get; set; }


        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

        public List<Post> Posts { get; set; }

        public List<Like> Likes { get; set; }

        public List<Comment> Comments { get; set; }



    }
}
