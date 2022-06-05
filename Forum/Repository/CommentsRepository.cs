using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationContext context;
        public CommentsRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public List<Comment> Get(int postId)
        {
            return this.context.Comments.Include(comment => comment.User).Where(c => c.PostId == postId).ToList();
        }

        public Comment GetById(int commentId)
        {
            var post = this.context.Comments.Where(C => C.Id == commentId).FirstOrDefault();
            return post ?? throw new EntityNotFoundException($"Comment with id {commentId} does not exist!");
        }

        public Comment Create(Comment comment)
        {
            var createdComment = this.context.Comments.Add(comment);
            this.context.SaveChanges();
            return createdComment.Entity;
        }

        public Comment Update(int id, Comment comment, Comment commentToUpdate)
        {
            commentToUpdate.Content = comment.Content;

            this.context.SaveChanges();

            return this.GetById(id);
        }

        public Comment Delete(int id,Comment commentToDelete)
        {
            var deletedComment = this.context.Comments.Remove(commentToDelete);
            this.context.SaveChanges();
            return deletedComment.Entity;
        }
    }
}
