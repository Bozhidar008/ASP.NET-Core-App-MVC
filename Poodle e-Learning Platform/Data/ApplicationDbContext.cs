using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Data.Contracts;
using Poodle_e_Learning_Platform.Models;


namespace Poodle_e_Learning_Platform.Data
{
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SectionPage> SectionPages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // to check??
            modelBuilder.Entity<Student>().HasMany(s => s.Courses);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.teacher) 
                .HasForeignKey();


            modelBuilder.Entity<Course>().HasMany(c => c.Students).WithMany(s => s.Courses);


            modelBuilder.Seed();
        }

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }
    }
}
