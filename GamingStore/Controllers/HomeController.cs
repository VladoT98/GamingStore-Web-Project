using GamingStore.Models.Home;
using Microsoft.AspNetCore.Mvc;
using GamingStore.Services.Home;

namespace GamingStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
            => this.homeService = homeService;

        public async Task<IActionResult> Index()
            => View(new HomeViewModel()
            {
                Blogs = await this.homeService.GetBlogPosts(),
                NewArrivals = await this.homeService.GetNewArrivals()
            });

        public IActionResult Privacy()
            => View();

        public IActionResult ContactUs()
            => View();

        public IActionResult AboutUs()
            => View();

        public IActionResult ContactedResponse()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View();
    }
}
