using GamingStore.Data;
using AutoMapper;
using GamingStore.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using GamingStore.Models.Games;

namespace GamingStore.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public HomeService(GamingStoreDbContext data, IMapper mapper, IMemoryCache cache)
        {
            this.data = data;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<IEnumerable<GameBaseModel>> GetNewArrivals()
        {
            var newArrivalsCacheKey = "NewArrivalsCacheKey";

            var newArrivals = this.cache.Get<List<GameBaseModel>>(newArrivalsCacheKey);

            if (newArrivals == null)
            {
                var games = await this.data.Games
                    .Where(x => x.IsApproved)
                    .OrderByDescending(x => x.Id)
                    .Take(3)
                    .ToListAsync();

                newArrivals = this.mapper.Map<List<GameBaseModel>>(games);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(newArrivalsCacheKey, newArrivals, cacheOptions);
            }

            return newArrivals;
        }

        public async Task<IEnumerable<BlogViewModel>> GetBlogPosts()
        {
            var blogPostsCacheKey = "BlogPostsCacheKey";

            var blogPosts = this.cache.Get<List<BlogViewModel>>(blogPostsCacheKey);

            if (blogPosts == null)
            {
                var blogs = await this.data.Blogs
                    .OrderByDescending(x => x.Id)
                    .Take(3)
                    .ToListAsync();

                blogPosts = this.mapper.Map<List<BlogViewModel>>(blogs);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(blogPostsCacheKey, blogPosts, cacheOptions);
            }

            return blogPosts;
        }
    }
}