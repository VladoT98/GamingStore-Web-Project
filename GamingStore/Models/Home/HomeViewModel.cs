using GamingStore.Areas.Admin.Models;
using GamingStore.Models.Games;

namespace GamingStore.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<BlogViewModel> Blogs { get; set; }

        public IEnumerable<GameBaseModel> NewArrivals { get; set; }
    }
}
