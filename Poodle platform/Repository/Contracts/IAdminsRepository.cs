using Poodle_e_Learning_Platform.Models;
using System.Collections.Generic;

namespace Poodle_e_Learning_Platform.Repository.Contracts
{
    public interface IAdminsRepository
    {
        List<Admin> Get();

        Admin Get(int id);

        Admin Get(string email);

        bool Exists(string email); // username == email
        Admin Create(Admin admin);
    }
}
