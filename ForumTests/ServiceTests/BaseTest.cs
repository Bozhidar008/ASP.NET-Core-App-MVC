using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WEbApplication1Tests.ServicesTests
{
    public abstract class BaseTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            var options = GetOptions();
            var context = new ApplicationContext(options);
            context.Database.EnsureDeleted();
        }

        public static DbContextOptions<ApplicationContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("InMemoryDB")
                .Options;
            return options;
        }
    }
}
