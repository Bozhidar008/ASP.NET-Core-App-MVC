using System.Collections.Generic;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ILikesService
    {
        public List<Like> GetAllLikes();

        public Like GetLikeById(int id);

        public Like Create(Like like, User user);

        public Like Update(int id, Like like, User user);

        public Like Delete (int id, User user);
    }
}
