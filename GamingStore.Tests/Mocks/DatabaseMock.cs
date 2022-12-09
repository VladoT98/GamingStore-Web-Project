using GamingStore.Data;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static GamingStoreDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<GamingStoreDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new GamingStoreDbContext(dbContextOptions);
            }
        }
    }
}