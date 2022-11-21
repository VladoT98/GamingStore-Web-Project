using GamingStore.Areas.Admin.Models;
using GamingStore.Models.Games;

namespace GamingStore.Services.Home
{
    public interface IHomeService
    {
        Task<IEnumerable<GameBaseModel>> GetNewArrivals();

        Task<IEnumerable<BlogViewModel>> GetBlogPosts();
    }
}