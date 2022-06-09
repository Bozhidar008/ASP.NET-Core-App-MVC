using System.Collections.Generic;
using System.Linq;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository likeRepository;

        public LikesService(ILikesRepository likesRepository)
        {
            this.likeRepository = likesRepository;
        }
        // how it works with parameters???
        public List<Like> GetAllLikes()
        {
            return this.likeRepository.Get();
        }

        public Like GetLikeById(int id)
        {
            return this.likeRepository.Get().FirstOrDefault(l => l.Id == id);
        }
        // to create method for IsBlocked check!
        public Like Create(Like like, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            return this.likeRepository.Create(like);
        }

        // is it even Updated???
        public Like Update(int id, Like like, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            var likeToUpdate = CheckAuthor(id, user);
            return this.likeRepository.Update(id, like, likeToUpdate);  
        }
        public Like Delete(int id, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnAuthorizedOperationException("User is blocked!");
            }
            var likeToDelete = CheckAuthor(id, user);
            return this.likeRepository.Delete(id);
        }

        public Like CheckAuthor(int id, User user)
        {
            var like = this.likeRepository.GetById(id);
            if (like.UserId != user.Id)
            {
                throw new UnAuthorizedOperationException("You are not the author of this comment");
            }
            return like;
        }
    }
}
