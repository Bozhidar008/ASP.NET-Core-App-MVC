using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Poodle_e_Learning_Platform.Data;
using Poodle_e_Learning_Platform.Exceptions;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Poodle_e_Learning_Platform.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext context;

        public StudentsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Student> Get()
        {
            return this.GetStudents().ToList();
        }

        public Student Get(int id)
        {
            return this.GetStudents().Where(t => t.Id == id).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public Student Get(string email)
        {
            return this.GetStudents().Where(t => t.Email == email).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public bool Exists(string email)
        {
            return this.context.Students.Any(u => u.Email == email);
        }
        public Student Create(Student student)
        {
            EntityEntry<Student> createdStudent = this.context.Students.Add(student);
            this.context.SaveChanges();
            return createdStudent.Entity;
        }

        private IQueryable<Student> GetStudents()
        {
            return this.context.Students.Include(t => t.Courses);
        }
    }
}
