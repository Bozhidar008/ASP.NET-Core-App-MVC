using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Repository.Contracts;
using Poodle_e_Learning_Platform.Services.Contracts;
using System.Collections.Generic;

namespace Poodle_e_Learning_Platform.Services
{
    public class TeachersService : ITeachersService
    {
		private readonly ITeachersRepository repository;

		public TeachersService(ITeachersRepository repository)
		{
			this.repository = repository;
		}

		public List<Teacher> Get()
		{
			return this.repository.Get();
		}

		public Teacher Get(int id)
		{
			return this.repository.Get(id);
		}

		public Teacher Get(string firstName)
		{
			return this.repository.Get(firstName);
		}

		public bool Exists(string email)
		{
			return this.repository.Exists(email);
		}

		public Teacher Create(Teacher teacher)
		{
			return this.repository.Create(teacher);
		}
	}
}
