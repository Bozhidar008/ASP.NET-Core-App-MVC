using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICommentsRepository
    {
        public List<Comment> Get(int postId);
        public Comment GetById(int commentId);

        public Comment Create(Comment comment);

        public Comment Update(int id, Comment comment, Comment commentToUpdate);

        public Comment Delete(int id,Comment commentToDelete);


    }
}
