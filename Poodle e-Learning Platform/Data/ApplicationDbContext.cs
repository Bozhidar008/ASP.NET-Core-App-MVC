using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Data.Contracts;
using Poodle_e_Learning_Platform.Models;
using System;

namespace Poodle_e_Learning_Platform.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<SectionPage> SectionPages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(e => e.ApplicationUserId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.ApplicationUserId);

            modelBuilder.Entity<SectionPage>()
                .HasOne(sp => sp.Course)
                .WithMany(c => c.Sections)
                .HasForeignKey(sp => sp.CourseId);

            modelBuilder.Entity<Course>(c =>
            {
                c.HasIndex(cc => cc.Title).IsUnique();
            });

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.CourseId, e.ApplicationUserId });

            //becouse  cant find ApplicationUserId - Error
            modelBuilder.Entity<ApplicationUser>()
                .HasKey(ap => ap.Id);
        }

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }
    }
}
