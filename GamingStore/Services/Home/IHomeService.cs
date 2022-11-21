using GamingStore.Areas.Admin.Models;
using GamingStore.Services.Games.Models;

namespace GamingStore.Services.Home
{
    public interface IHomeService
    {
        Task<IEnumerable<NewArrivalsServiceModel>> GetNewArrivals();

        Task<IEnumerable<BlogViewModel>> GetBlogPosts();
    }
}