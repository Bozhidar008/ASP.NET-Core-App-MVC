using System.Collections.Generic;
using WebApplication1.Models;

namespace WEbApplication1Tests.Models
{
    public class CommentTestModels
    {
        public List<Comment> GetComments(List<Post> posts)
        {
            return new List<Comment>()
            {
              new Comment()
              {
                Id = 1,
                Content = "First comment",
                UserId = 1,
                //PostId = posts[0].Id ???
                PostId = 1
              },
              new Comment()
              {
                Id = 2,
                Content = "Second comment",
                UserId = 2,
                PostId = 2
              },
              new Comment()
              {
                Id = 3,
                Content = "Third comment",
                UserId = 1,
                PostId = 3
              }
            };
        }
    }
}
