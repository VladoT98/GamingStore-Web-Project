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
        {
            var newArrivals = await this.homeService.GetNewArrivals();
            var blogPosts = await this.homeService.GetBlogPosts();

            var viewModel = new HomeViewModel();
            viewModel.Blogs = blogPosts;
            viewModel.NewArrivals = newArrivals;

            return View(viewModel);
        }

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
