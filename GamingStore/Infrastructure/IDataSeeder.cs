using GamingStore.Data;

namespace GamingStore.Infrastructure
{
    public interface IDataSeeder
    {
        public Task SeedPlatforms(GamingStoreDbContext data);

        public Task SeedPublishers(GamingStoreDbContext data);

        public Task SeedGenres(GamingStoreDbContext data);

        public Task SeedSellers(GamingStoreDbContext data);

        public Task SeedGames(GamingStoreDbContext data);

        public Task SeedReviews(GamingStoreDbContext data);

        public Task SeedAdministrator(IServiceProvider services);
    }
}