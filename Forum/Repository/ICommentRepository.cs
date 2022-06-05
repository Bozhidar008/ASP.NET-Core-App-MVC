using System.Collections.Generic;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICommentRepository
    {
       
        public List<CommentInfoDto> Get();

        public Comment GetById(int id);

        public Comment Create(Comment comment);

        public Comment Update(int id,Comment comment, Comment commentToUpdate);

        public Comment Delete(int id);

        public IEnumerable<CommentDto> GetPostComments(int postId);
    }
}
