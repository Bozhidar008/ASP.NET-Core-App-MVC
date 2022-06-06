using System;
using System.Collections.Generic;
using WebApplication1.Controllers;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UsersService : IUsersService
    {
		private readonly IUsersRepository repository;
		//If dont want to use Repositoty, we inject into dbContext
		//private readonly ApplicationContext dbContext;

		public UsersService(IUsersRepository repository)
		{
			this.repository = repository;
		}


		public List<User> GetAll()
		{
			return this.repository.Get();
		}

		public List<User> Get(UserQueryParameters filterParameters)
		{
			return this.repository.Get(filterParameters);
		}

		public User GetById(int id)
		{
			return this.repository.GetById(id);
		}

		public User GetByEmail(string email)
        {
			return this.repository.GetByEmail(email);				
		}

		public User Create(User user)
		{
			bool duplicateExists = true;

			try
			{
				this.repository.GetByEmail(user.Email);
			}
			catch (Exception)
			{
				duplicateExists = false;
			}

			if (duplicateExists)
			{
				throw new Exception();
			}

			var createdUser = this.repository.Create(user);
			return createdUser;
		}

		public User Update(int id,User userToUpdate,User user)
		{
			if (!user.IsAdmin)
			{
				throw new UnAuthorizedOperationException("You are not an admin");
			}

			bool duplicateExists = true;
			try
			{
				var existingUser = this.repository.GetByUsername(user.UserName);
				if (existingUser.Id == user.Id)
				{
					duplicateExists = false;
				}
			}
			catch (EntityNotFoundException)
			{
				duplicateExists = false;
			}

			if (duplicateExists)
			{
				throw new DuplicateEntityException();
			}

			var updatedUser = this.repository.Update(id, userToUpdate);
			return updatedUser;
		}

		public void Delete(int id)
		{
			this.repository.Delete(id);
		}

        public User GetByUsername(string username)//???
        {
			//return dbContext.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault();
			return this.repository.GetByUsername(username);
        }

        public bool HasUser(string username)
        {
			return repository.HasUser(username);
        }
    }
}

