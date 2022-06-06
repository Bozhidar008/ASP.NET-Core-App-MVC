using System.Collections.Generic;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository repository;
        public PostsService(IPostsRepository repository)
        {

            this.repository = repository;
        }

        public List<Post> Get()
        {
            return this.repository.Get();
        }

        public Post Create(Post post, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            return this.repository.Create(post);
        }

        public Post Update(int id, Post post, User user)
        {
            var postToUpdate = CheckAuthor(id, user);
            return this.repository.Update(id, post, postToUpdate);
        }

        public Post Delete(int id, User user)
        {
            var postToDelete = CheckAuthor(id, user);
            return this.repository.Delete(id);
        }

        public Post CheckAuthor(int id, User user)
        {
            var post = this.repository.GetById(id);
            if (post.UserId != user.Id)
            {
                throw new UnAuthorizedOperationException("You are not the author of this post");
            }
            return post;
        }



    }
}
