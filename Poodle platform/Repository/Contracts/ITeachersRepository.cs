using Poodle_e_Learning_Platform.Models;
using System.Collections.Generic;

namespace Poodle_e_Learning_Platform.Repository.Contracts
{
    public interface ITeachersRepository
    {
        List<Teacher> Get();

        Teacher Get(int id);

        Teacher Get(string email); 

        bool Exists(string email); // username == email
        Teacher Create(Teacher teacher);
    }
}
