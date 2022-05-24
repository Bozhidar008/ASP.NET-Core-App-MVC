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
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext context;

        public CoursesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Course> Get()
        {
            return this.GetCourses().ToList();
        }

        public Course Get(int id)
        {
            return this.GetCourses().Where(t => t.Id == id).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public Course Get(string title)
        {
            return this.GetCourses().Where(t => t.Title == title).FirstOrDefault() ?? throw new EntityNotFoundException();
        }

        public bool Exists(string title)
        {
            return this.context.Courses.Any(u => u.Title == title);
        }
        public Course Create(Course course)
        {
            EntityEntry<Course> createdCourse = this.context.Courses.Add(course);
            this.context.SaveChanges();
            return createdCourse.Entity;
        }

        private IQueryable<Course> GetCourses()
        {
            return this.context.Courses.Include(t => t.Student.FirstName + " " + t.Student.LastName);
            // to add all courses student have??? or Id?
        }
    }
}
