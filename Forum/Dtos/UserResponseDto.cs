using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Dtos
{
    public class UserResponseDto
    {

        public UserResponseDto(User userModel)
        {

            this.FirstName = userModel.FirstName;
            this.LastName = userModel.LastName;
            this.Email = userModel.Email;
            this.UserName = userModel.UserName;
            foreach (var post in userModel.Posts)
            {
                this.Posts.Add(post.Title);
            }
        }

   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<string> Posts { get; set; } = new List<string>(); 
    }
}
