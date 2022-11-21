using GamingStore.Data;
using GamingStore.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly GamingStoreDbContext data;

        public StatisticsController(GamingStoreDbContext data)
            => this.data = data;

        [HttpGet]
        public async Task<StatisticsApiModel> GetStatistics()
        {
            return new StatisticsApiModel()
            {
                GamesCount = await this.data.Games.CountAsync(),
                GenresCount = await this.data.Genres.CountAsync(),
                PublishersCount = await this.data.Publishers.CountAsync(),
                PlatformsCount = await this.data.Platforms.CountAsync(),
                BlogsCount = await this.data.Blogs.CountAsync(),
                ReviewsCount = await this.data.GameReviews.CountAsync()
            };
        }
    }
}