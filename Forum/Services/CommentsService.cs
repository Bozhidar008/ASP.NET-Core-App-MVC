using System.Collections.Generic;
using System.Linq;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Models.Mappers;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentRepository commentRepository;

        public CommentsService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        // not working properly? use queryParam?
        public IEnumerable<CommentInfoDto> GetAllCommentsForPost(int userId)
        {
            return this.commentRepository.Get();
        }

        public CommentInfoDto GetCommentById(int id)
        {
            return this.commentRepository.Get().FirstOrDefault(c => c.Id == id); 
        }
        
        public Comment Create(Comment comment, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            
            return this.commentRepository.Create(comment);
        }
        public Comment UpdateComment(int id, Comment comment, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            var commentToUpdate = CheckAuthor(id, user);
            return this.commentRepository.Update(id, comment, commentToUpdate);
        }

        public Comment DeleteComment(int id, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            var commentToDelete = CheckAuthor(id, user);
            return this.commentRepository.Delete(id); 
        }

        public Comment CheckAuthor(int id, User user)
        {
            var comment = this.commentRepository.GetById(id);
            if (comment.UserId != user.Id)
            {
                throw new UnAuthorizedOperationException("You are not the author of this comment");
            }
            return comment;
        }

        public IEnumerable<CommentDto> GetPostComments(int postId)
        {
            return commentRepository.GetPostComments(postId);
        }
    }
}
