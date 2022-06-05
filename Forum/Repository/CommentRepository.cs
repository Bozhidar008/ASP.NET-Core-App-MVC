using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext dbContext;

        public CommentRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public List<CommentInfoDto> Get()
        {
            List<CommentInfoDto> comments = dbContext
                .Comments
                .Select(c => new CommentInfoDto
                {

                    Id = c.Id,
                    Content = c.Content,
                    PostTitle = c.Post.Title,
                    Author = c.User.UserName,
                })
                .ToList();

            return comments;

            /*
            return this.dbContext.Comments
                .Include(c => c.Post.Title)
                .Include(c=>c.User)
                .ToList();// what to include??/
            */
        }

        public Comment GetById(int id)
        {
            var comment = this.dbContext.Comments.FirstOrDefault(b => b.Id == id);
            return comment ?? throw new EntityNotFoundException();
        }

        public Comment Create(Comment comment)
        {
            var createdComment = this.dbContext.Comments.Add(comment);
            this.dbContext.SaveChanges();
            return createdComment.Entity;
        }

        public Comment Update(int id, Comment comment, Comment commentToUpdate)
        {
            commentToUpdate.Content = comment.Content;

            this.dbContext.SaveChanges();
            return this.GetById(id);
        }

        public Comment Delete(int id)
        {
            var commentToDelete = this.GetById(id);
            var deletedComment = this.dbContext.Comments.Remove(commentToDelete);
            this.dbContext.SaveChanges();
            return deletedComment.Entity;
        }

        public IEnumerable<CommentDto> GetPostComments(int postId)
        {
            List<CommentDto> comments = dbContext
                .Comments
                .Where(c => c.PostId == postId)
                .Select(c => new CommentDto
                {
                    Content = c.Content,
                    Author = c.User.UserName,
                })
                .ToList();

            return comments;
        }
    }
}
