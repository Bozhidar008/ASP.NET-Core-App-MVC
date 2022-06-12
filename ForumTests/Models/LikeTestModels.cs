using System.Collections.Generic;
using WebApplication1.Models;

namespace WEbApplication1Tests.Models
{
    public class LikeTestModels
    {
        
        public List<Like> GetLikes(List<User> users, List<Post> posts)
        {
            return new List<Like>()
            {
               new Like()
               {
                Id = 1,
                UserId = 1,
                PostId = 1
               },
               new Like()
               {
                Id = 2,
                UserId = 2,
                PostId = 2
               },
               new Like()
               {
                Id = 3,
                UserId = 1,
                PostId = 3
               }
            };
        }
    }
}
