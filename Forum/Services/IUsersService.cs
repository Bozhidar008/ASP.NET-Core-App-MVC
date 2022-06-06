using System.Collections.Generic;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
   public interface IUsersService
    {
		public List<User> GetAll();

		public List<User> Get(UserQueryParameters filterParameters);

		public User GetByUsername(string username);
		public User GetById(int id);

		public User GetByEmail(string email);

		public User Create(User user);

		public User Update(int id, User userToUpdate, User user);

		public void Delete(int id);

		bool HasUser(string userName);
		
	}
}