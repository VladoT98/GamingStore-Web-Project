using GamingStore.Areas.Admin.Models;
using GamingStore.Areas.Admin.Services.Admin;
using GamingStore.Areas.Admin.Services.Blogs;
using GamingStore.Infrastructure.Enums;
using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.Reviews;
using GamingStore.Services.Games;
using GamingStore.Services.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IGameService gameService;
        private readonly IReviewService reviewService;
        private readonly IBlogService blogService;

        public AdminController(IAdminService adminService, IGameService gameService, IReviewService reviewService, IBlogService blogService)
        {
            this.adminService = adminService;
            this.gameService = gameService;
            this.reviewService = reviewService;
            this.blogService = blogService;
        }

        public async Task<IActionResult> AdminGames(string searchByTitle, string publisher, GameSorting sorting, int currentPage = 1)
        {
            var viewModel = await this.gameService.GetFullGameDetails(searchByTitle, publisher, sorting, currentPage, 10, true, false, User.Id());
            return View(viewModel);
        }

        public async Task<IActionResult> AdminReviews(string searchByUsername, string searchByGame, ReviewSorting sorting, int currentPage = 1)
        {
            var reviewsCount = await reviewService.ReviewsCount(searchByGame, searchByUsername);

            var viewModel = new ReviewSearchViewModel()
            {
                SearchByGame = searchByGame,
                SearchByUsername = searchByUsername,
                Sorting = sorting,
                Reviews = this.adminService.AdminReviews(searchByUsername, searchByGame, sorting, currentPage, 10),
                CurrentPage = currentPage,
                TotalItems = reviewsCount,
                ItemsPerPage = 10,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AdminBlogs(string searchByTitle, int currentPage = 1)
        {
            var blogsCount = await this.blogService.BlogsCount(searchByTitle);

            var viewModel = new BlogSearchViewModel()
            {
                SearchByTitle = searchByTitle,
                Blogs = await this.blogService.GetAll(),
                CurrentPage = currentPage,
                TotalItems = blogsCount,
                ItemsPerPage = 10,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var isApproved = await this.adminService.ApproveGame(id);

            if (!isApproved) return BadRequest();

            TempData["GlobalMessageKey"] = "Game approved.";

            return RedirectToAction(nameof(AdminGames));
        }
    }
}