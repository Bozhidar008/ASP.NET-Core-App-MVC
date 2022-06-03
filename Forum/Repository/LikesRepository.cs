using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class LikesRepository : ILikesRepository
    {
        private readonly ApplicationContext dbContext;

        public LikesRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Like> Get()
        {
            return this.dbContext.Likes
                .Include(l => l.Post)
                .Include(l => l.User)//User or UserId or Username???
                .ToList();
        }

        public Like GetById(int id)
        {
            var like = this.dbContext.Likes.FirstOrDefault(l => l.Id == id);
            return like ?? throw new EntityNotFoundException();
        }
        public Like Create(Like like)
        {
            var createdLIke = this.dbContext.Likes.Add(like);
            this.dbContext.SaveChanges();
            return createdLIke.Entity;
        }

        // Is it needed option??? is it possible to Update even???
        public Like Update(int id, Like like, Like likeToUpdate)
        {
           likeToUpdate.Id = like.Id;
            this.dbContext.SaveChanges();
            return this.GetById(id);

        }
        public Like Delete(int id)
        {
            var likeToDelete = this.GetById(id);
            var deletedLike = this.dbContext.Likes.Remove(likeToDelete);
            this.dbContext.SaveChanges();
            return deletedLike.Entity;
        }
    }
}
