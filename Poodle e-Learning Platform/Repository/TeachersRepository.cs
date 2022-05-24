using Poodle_e_Learning_Platform.Exceptions;
using Poodle_e_Learning_Platform.Models;

namespace Poodle_e_Learning_Platform.Repository
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly ApplicationDbContext context;

        public TeachersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public List<Teacher> Get()
        {
            return this.GetTeachers().ToList();
        }

        public Teacher Get(int id)
        {
            return this.GetTeachers().Where(t => t.Id == id).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public Teacher Get(string email)
        {
            return this.GetTeachers().Where(t => t.Email == email).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public bool Exists(string email)
        {
            return this.context.Teachers.Any(u => u.Email == email);
        }
        public Teacher Create(Teacher teacher)
        {
            EntityEntry<Teacher> createdTeacher = this.context.Teachers.Add(teacher);
            this.context.SaveChanges();
            return createdTeacher.Entity;
        }

        private IQueryable<Teacher> GetTeachers()
        {
            return this.context.Teachers.Include(t => t.Courses);
        }
    }
}

}
