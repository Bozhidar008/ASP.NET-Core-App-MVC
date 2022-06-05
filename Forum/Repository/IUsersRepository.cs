using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUsersRepository
    {
        public List<User> Get();

        public List<User> Get(UserQueryParameters userParams);

        public User GetById(int id);
        public User GetByEmail(string email);
        public User GetByUsername(string username);
        public User Create(User user);

        public User Update(int id, User user);

        public User Delete(int id);

        bool HasUser(string username);

    }
}
