using System.Collections.Generic;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ICommentsService
    {
        
        public IEnumerable<CommentInfoDto> GetAllCommentsForPost(int userId);
        public CommentInfoDto GetCommentById(int id);

        public  Comment Create(Comment comment, User user);

        public Comment UpdateComment(int id, Comment comment, User user);

        public Comment DeleteComment(int id, User user);

        public IEnumerable<CommentDto> GetPostComments(int postId);
    }
}
