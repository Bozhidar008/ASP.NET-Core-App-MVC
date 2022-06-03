using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;

namespace WebApplication1.Repository
{
    public class UsersRepository : IUsersRepository
    {

       // private readonly List<User> users = new List<User>();
        private readonly ApplicationContext context;
        public UsersRepository(ApplicationContext context)
        {
            this.context = context;       
        }

        public List<User> Get()
        {
            return this.context.Users.Include(user => user.Posts).ToList();
        }

        public List<User> Get(UserQueryParameters userParams)
        {
            IEnumerable<User> result = this.context.Users.Include(user => user.Posts);
            if (!string.IsNullOrEmpty(userParams.FirstName))
            {
                result = result.Where(b => b.FirstName.Contains(userParams.FirstName, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(userParams.UserName))
            {
                result = result.Where(b => b.UserName.Contains(userParams.UserName, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(userParams.Email))
            {
                result = result.Where(b => b.Email.Contains(userParams.Email, StringComparison.InvariantCultureIgnoreCase));
            }
            return result.ToList();
        }

        public User GetById(int id)
        {
            var user = this.context.Users.FirstOrDefault(b => b.Id == id);

             return user ?? throw new Exception();
            
        }
        public User GetByEmail(string email)
        {
            var user = this.context.Users.Where(b => b.Email == email).FirstOrDefault();
            return user ?? throw new Exception();

        }
        public User Create(User user)
        {
            var createdBeer = this.context.Users.Add(user);
            this.context.SaveChanges();
            return createdBeer.Entity;
        }

        public User Update(int id, User user)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            //userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;

            this.context.SaveChanges();

            return this.GetById(id);
        }

        public User Delete(int id)
        {
            var userToDelete = this.GetById(id);
            var deletedUser =  this.context.Users.Remove(userToDelete);
            return deletedUser.Entity;
        }
        public User GetByUsername(string username)
        {
            var user = this.context.Users.Where(b => b.UserName == username).FirstOrDefault();
            return user ?? throw new EntityNotFoundException();

        }

        public bool HasUser(string username)
        {
            return context.Users.Any(u => u.UserName.Equals(username));
        }
    }
}
