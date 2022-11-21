using GamingStore.Areas.Admin.Models;
using GamingStore.Services.Games.Models;

namespace GamingStore.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<BlogViewModel> Blogs { get; set; }

        public IEnumerable<NewArrivalsServiceModel> NewArrivals { get; set; }
    }
}
