using GamingStore.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var test = 10;
        }

        public async Task<IEnumerable<GameBaseModel>> GetNewArrivals()
        {
            var newArrivalsCacheKey = "NewArrivalsCacheKey";

            var newArrivals = this.cache.Get<List<GameBaseModel>>(newArrivalsCacheKey);

            if (newArrivals == null)
            {
                newArrivals = await this.data.Games
                    .Where(x => x.IsApproved)
                    .OrderByDescending(x => x.Id)
                    .ProjectTo<GameBaseModel>(this.mapper.ConfigurationProvider)
                    .Take(3)
                    .ToListAsync();

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
                blogPosts = await this.data.Blogs
                    .OrderByDescending(x => x.Id)
                    .Take(3)
                    .ProjectTo<BlogViewModel>(mapper.ConfigurationProvider)
                    .ToListAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(blogPostsCacheKey, blogPosts, cacheOptions);
            }

            return blogPosts;
        }
    }
}