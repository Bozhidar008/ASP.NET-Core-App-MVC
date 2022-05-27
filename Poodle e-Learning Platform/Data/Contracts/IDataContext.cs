using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Models;

namespace Poodle_e_Learning_Platform.Data.Contracts
{
    public interface IDataContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<SectionPage> SectionPages { get; set; }

        void SaveChanges();
    }
}
