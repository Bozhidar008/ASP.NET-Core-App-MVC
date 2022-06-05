using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
   public interface IPostsRepository
    {
        public List<Post> Get();
        public Post GetById(int id);

        public Post Create(Post post);

        public Post Update(int id, Post post, Post postToUpdate);

        public Post Delete(int id);
    }
}
