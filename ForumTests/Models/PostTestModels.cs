using System.Collections.Generic;
using WebApplication1.Models;

namespace WEbApplication1Tests.Models
{
    public class PostTestModels
    {
        
        public List<Post> GetPosts(List<Comment> comments, List<Like> likes)
        {
            return new List<Post>
            {
              new Post
              {
                Id = 1,
                Title = "FirstPost",
                Content = "SomeContent",
                UserId = 1
              },
              new Post
              {
                Id = 2,
                Title = "SecondPost",
                Content = "AgainSomeContent",
                UserId = 2
              },
              new Post
              {
                Id = 3,
                Title = "ThirdPost",
                Content = "SomeSomeContent",
                UserId = 1
              }
            };
        }
    }
}
