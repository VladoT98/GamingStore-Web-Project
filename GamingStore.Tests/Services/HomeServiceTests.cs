using AutoMapper;
using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Services.Home;
using GamingStore.Tests.Mocks;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class HomeServiceTests
    {
        private GamingStoreDbContext data;
        private IMapper mapper;
        private IMemoryCache cache;

        public HomeServiceTests()
        {
            this.data = DatabaseMock.Instance;
            this.mapper = MapperMock.Instance;
            this.cache = new MemoryCache(new MemoryCacheOptions());
        }

        [Fact]
        public async Task GetNewArrivalsShouldReturnTheLastThreeAddedGamesOrderedDescendingById()
        {
            this.data.Games.AddRange(Enumerable.Range(0, 10).Select(x => new Game()
            {
                Title = "",
                Description = "",
                ImageUrl = "",
                TrailerUrl = "",
                Publisher = new Publisher() { Name = "", Description = ""},
                IsApproved = true
            }));

            await this.data.SaveChangesAsync();

            var homeService = new HomeService(this.data, MapperMock.Instance, this.cache);

            var result = await homeService.GetNewArrivals();

            Assert.Equal(3, result.Count());
            Assert.Equal(10, result.First().Id);
        }

        [Fact]
        public async Task GetBlogPostsShouldReturnTheLastThreeAddedBlogsOrderedDescendingById()
        {
            this.data.Blogs.AddRange(Enumerable.Range(0, 10).Select(x => new Blog()
            {
                ImageUrl = "",
                Title = "",
                Content = ""
            }));

            await this.data.SaveChangesAsync();

            var homeService = new HomeService(this.data, this.mapper, this.cache);

            var result = await homeService.GetBlogPosts();

            Assert.Equal(3, result.Count());
            Assert.Equal(10, result.First().Id);
        }
    }
}