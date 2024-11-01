using DeskMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Tests
{
    public class DbContextTextFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public DbContextTextFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            Context = new ApplicationDbContext(options);
        }
        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
