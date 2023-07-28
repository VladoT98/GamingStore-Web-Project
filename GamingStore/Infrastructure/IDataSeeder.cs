using GamingStore.Data;

namespace GamingStore.Infrastructure
{
    public interface IDataSeeder
    {
        public void SeedPlatforms(GamingStoreDbContext data);

        public void SeedPublishers(GamingStoreDbContext data);

        public void SeedGenres(GamingStoreDbContext data);

        public void SeedUser(GamingStoreDbContext data);

        public void SeedSellers(GamingStoreDbContext data);

        public void SeedGames(GamingStoreDbContext data);

        public void SeedReviews(GamingStoreDbContext data);

        public void SeedBlogPosts(GamingStoreDbContext data);

        public void SeedAdministrator(IServiceProvider services);
    }
}