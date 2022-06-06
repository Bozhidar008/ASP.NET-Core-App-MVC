
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPostsService
    {
        public List<Post> Get();

        public Post Create(Post post, User user);//?? User user

        public Post Update(int id, Post post, User user);

        public Post Delete(int id, User user);
    }
}
