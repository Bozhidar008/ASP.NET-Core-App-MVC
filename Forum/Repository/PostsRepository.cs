using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class PostsRepository : IPostsRepository
    {

        private readonly ApplicationContext context;
        public PostsRepository(ApplicationContext context)
        {
            this.context = context;
        }
       
        public List<Post> Get()
        {
            return this.context.Posts.Include(post => post.User).ToList();
        }

        public Post GetById(int id)
        {
            var post = this.context.Posts.Where(b => b.Id == id).FirstOrDefault();
            return post ?? throw new EntityNotFoundException();
        }
        public Post Create(Post post)
        {
            var createdPost = this.context.Posts.Add(post);
            this.context.SaveChanges();
            return createdPost.Entity;
        }

        public Post Update(int id, Post post, Post postToUpdate)
        {

            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;

            this.context.SaveChanges();

            return this.GetById(id);
        }

        public Post Delete(int id)
        {
            var postToDelete = this.GetById(id);
            var deletedPost = this.context.Posts.Remove(postToDelete);
            this.context.SaveChanges();
            return deletedPost.Entity;
        }


    }
}
