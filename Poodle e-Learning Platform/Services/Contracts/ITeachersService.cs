using Poodle_e_Learning_Platform.Models;
using System.Collections.Generic;

namespace Poodle_e_Learning_Platform.Services.Contracts
{
    public interface ITeachersService
    {
        List<Teacher> Get();
        Teacher Get(int id);
        Teacher Get(string firstName);
        bool Exists(string email);
        Teacher Create(Teacher teacher);
    }
}
