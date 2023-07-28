using GamingStore.Data;
using GamingStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static readonly IDataSeeder seeder;

        static ApplicationBuilderExtensions()
            => seeder = new DataSeeder();

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var data = scopedServices.ServiceProvider.GetService<GamingStoreDbContext>();

            data.Database.Migrate();

            seeder.SeedPlatforms(data);
            seeder.SeedPublishers(data);
            seeder.SeedGenres(data);
            seeder.SeedUser(data);
            seeder.SeedSellers(data);
            seeder.SeedGames(data);
            seeder.SeedReviews(data);
            seeder.SeedBlogPosts(data);
            seeder.SeedAdministrator(services);

            return app;
        }
    }
}