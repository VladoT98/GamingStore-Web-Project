using GamingStore.Areas.Admin.Models;
using GamingStore.Areas.Admin.Services.Admin;
using GamingStore.Areas.Admin.Services.Blogs;
using GamingStore.Infrastructure.Enums;
using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.Reviews;
using GamingStore.Services.Games;
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
        private readonly IBlogService blogService;

        public AdminController(IAdminService adminService, IGameService gameService, IBlogService blogService)
        {
            this.adminService = adminService;
            this.gameService = gameService;
            this.blogService = blogService;
        }

        public async Task<IActionResult> AdminGames(string searchByTitle, string publisher, GameSorting sorting, int currentPage = 1)
        {
            var viewModel = await this.gameService.GetFullGameDetails(searchByTitle, publisher, sorting, currentPage, 10, true, false, User.Id());

            return View(viewModel);
        }

        public async Task<IActionResult> AdminReviews(string searchByUsername, string searchByGame, ReviewSorting sorting, int currentPage = 1)
        {
            var reviewsCount = await this.adminService.ReviewsCount(searchByGame, searchByUsername);

            var viewModel = new ReviewSearchViewModel()
            {
                SearchByGame = searchByGame,
                SearchByUsername = searchByUsername,
                Sorting = sorting,
                Reviews = await this.adminService.GetReviewsInfo(searchByUsername, searchByGame, sorting, currentPage, 10),
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

        public async Task<IActionResult> Users(string searchByEmail, string searchByPhoneNumber, UserSorting sorting, int currentPage = 1)
        {
            var usersCount = await adminService.UsersCount(searchByEmail, searchByPhoneNumber, sorting);

            var viewModel = new UserSearchViewModel()
            {
                SearchByEmail = searchByEmail,
                SearchByPhoneNumber = searchByPhoneNumber,
                Sorting = sorting,
                Users = await this.adminService.GetUsersInfo(searchByEmail, searchByPhoneNumber, sorting, currentPage, 5),
                CurrentPage = currentPage,
                TotalItems = usersCount,
                ItemsPerPage = 5,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ApproveGame(int id)
        {
            var isApproved = await this.adminService.ApproveGame(id);

            if (!isApproved) return BadRequest();

            TempData["GlobalMessageKey"] = "Game approved.";

            return RedirectToAction(nameof(AdminGames));
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var isUserDeleted = await this.adminService.DeleteUser(userId);

            if (!isUserDeleted) return BadRequest();

            return RedirectToAction(nameof(Users));
        }
    }
}