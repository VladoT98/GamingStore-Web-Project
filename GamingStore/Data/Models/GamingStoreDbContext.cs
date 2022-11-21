using GamingStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Data
{
    public class GamingStoreDbContext : IdentityDbContext
    {
        public GamingStoreDbContext(DbContextOptions<GamingStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; init; }

        public DbSet<Genre> Genres { get; init; }

        public DbSet<Publisher> Publishers { get; init; }

        public DbSet<Platform> Platforms { get; init; }

        public DbSet<Seller> Sellers { get; init; }

        public DbSet<GameReview> GameReviews { get; init; }

        public DbSet<Blog> Blogs { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Game>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Game>()
                .HasOne(x => x.Publisher)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Game>()
                .HasOne(x => x.Platform)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.PlatformId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Game>()
                .HasOne(x => x.Seller)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Seller>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Seller>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GameReview>().ToTable("GameReviews");

            base.OnModelCreating(builder);
        }
    }
}