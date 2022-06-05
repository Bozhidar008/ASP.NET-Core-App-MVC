using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
   public interface IPostsRepository
    {
        public List<Post> Get(PostQueryParameters filterParameters);
        public int GetHomeInfo();

        public List<Post> GetPostsInfo1();
        public List<Post> GetPostsInfo2();


        public Post GetById(int id);
        public bool Like(User user, int id);
        public Post Create(Post post);

        public Post Update(int id, Post post, Post postToUpdate);

        public Post Delete(int id, Post postToDelete);


    }
}
