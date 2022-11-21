using GamingStore.Data;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static readonly IDataSeeder seeder;

        static ApplicationBuilderExtensions()
            => seeder = new DataSeeder();

        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var data = scopedServices.ServiceProvider.GetService<GamingStoreDbContext>();

            await data.Database.MigrateAsync();

            //await seeder.SeedPlatforms(data);
            //await seeder.SeedPublishers(data);
            //await seeder.SeedGenres(data);
            //await seeder.SeedSellers(data);
            //await seeder.SeedGames(data);
            //await seeder.SeedReviews(data);
            //await seeder.SeedAdministrator(services);

            return app;
        }
    }
}