using Microsoft.EntityFrameworkCore.ChangeTracking;
using Poodle_e_Learning_Platform.Data;
using Poodle_e_Learning_Platform.Exceptions;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Poodle_e_Learning_Platform.Repository
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly ApplicationDbContext context;

        public AdminsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public List<Admin> Get()
        {
            return this.GetAdmins().ToList();
        }

        public Admin Get(int id)
        {
            return this.GetAdmins().Where(t => t.Id == id).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public Admin Get(string email)
        {
            return this.GetAdmins().Where(t => t.Email == email).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public bool Exists(string email)
        {
            return this.context.Admins.Any(u => u.Email == email);
        }
        public Admin Create(Admin admin)
        {
            EntityEntry<Admin> createdAdmin = this.context.Admins.Add(admin);
            this.context.SaveChanges();
            return createdAdmin.Entity;
        }

        private IQueryable<Admin> GetAdmins()
        {
            return this.context.Admins;// ToList()??
        }
    }
}
