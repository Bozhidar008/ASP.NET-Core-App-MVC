using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ILikesRepository
    {
        public List<Like> Get();
        public Like GetById(int id);

        public Like Create(Like like);

        public Like Update(int id,Like like, Like likeToUpdate);

        public Like Delete(int id);
    }
}
