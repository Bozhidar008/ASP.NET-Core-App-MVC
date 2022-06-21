using Poodle_e_Learning_Platform.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Poodle_e_Learning_Platform.Data
{
    public static class ModelBuilderExtensions
    {

        public static IEnumerable<Course> courses;
        
        static ModelBuilderExtensions()
        {

            //teachers = SeedModels.GetTeachers();
           // students = SeedModels.GetStudents();
           // admins = SeedModels.GetAdmins();
            //courses = SeedModels.GetCourses();
           
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {                     
            modelBuilder.Entity<Course>().HasData(courses);            
        }
    }
}
