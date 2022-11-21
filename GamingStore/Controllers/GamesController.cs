using AutoMapper;
using GamingStore.Areas.Admin.Controllers;
using GamingStore.Infrastructure.Extensions;
using GamingStore.Models;
using GamingStore.Models.Games;
using GamingStore.Services.Games;
using GamingStore.Services.Sellers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService gameService;
        private readonly ISellerService sellerService;
        private readonly IMapper mapper;

        public GamesController(IGameService gameService, ISellerService sellerService, IMapper mapper)
        {
            this.gameService = gameService;
            this.sellerService = sellerService;
            this.mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            if (!await this.sellerService.IsUserSeller(User.Id()))
                return RedirectToAction(nameof(SellersController.Become), "Sellers");

            return View(new GameFormModel
            {
                Genres = await this.gameService.GameGenres(),
                Platforms = await this.gameService.GamePlatforms()
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(GameFormModel gameFormModel)
        {
            if (!await this.sellerService.IsUserSeller(User.Id()))
                return RedirectToAction(nameof(SellersController.Become), "Sellers");

            if (!await this.gameService.IsGenresExist(gameFormModel))
                ModelState.AddModelError(nameof(gameFormModel.GenreId), "Invalid genre.");

            if (!await this.gameService.IsPublisherExist(gameFormModel))
                return RedirectToAction(nameof(PublishersController.Register), "Publishers");

            if (!await this.gameService.IsPlatformExist(gameFormModel))
                ModelState.AddModelError(nameof(gameFormModel.PlatformId), "Invalid platform.");

            if (!ModelState.IsValid)
            {
                gameFormModel.Genres = await this.gameService.GameGenres();
                gameFormModel.Platforms = await this.gameService.GamePlatforms();

                return View(gameFormModel);
            }

            TempData["GlobalMessageKey"] = "Your game was added and waiting for approval.";

            var gameId = await this.gameService.Add(gameFormModel, User.Id());

            return RedirectToAction(nameof(Details), new { id = gameId });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.sellerService.IsUserSeller(User.Id()) && !User.IsAdmin())
                return RedirectToAction(nameof(SellersController.Become), "Sellers");

            var game = await this.gameService.Details(id);

            if (game == null) return BadRequest();

            if (game.UserId != User.Id() && !User.IsAdmin()) return Unauthorized();

            var gameForm = this.mapper.Map<GameFormModel>(game);

            gameForm.Genres = await this.gameService.GameGenres();
            gameForm.Platforms = await this.gameService.GamePlatforms();

            return View(gameForm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GameFormModel gameFormModel)
        {
            if (!await this.sellerService.IsUserSeller(User.Id()) && !User.IsAdmin())
                return RedirectToAction(nameof(SellersController.Become), "Sellers");

            if (!await this.gameService.IsGenresExist(gameFormModel))
                ModelState.AddModelError(nameof(gameFormModel.GenreId), "Invalid genre.");

            if (!await this.gameService.IsPublisherExist(gameFormModel))
                return RedirectToAction(nameof(PublishersController.Register), "Publishers");

            if (!await this.gameService.IsPlatformExist(gameFormModel))
                ModelState.AddModelError(nameof(gameFormModel.PlatformId), "Invalid platform.");

            var test = ModelState.Values.ToList();

            if (!ModelState.IsValid)
            {
                gameFormModel.Genres = await this.gameService.GameGenres();
                gameFormModel.Platforms = await this.gameService.GamePlatforms();

                return View(gameFormModel);
            }

            var sellerId = await this.sellerService.GetSellerId(User.Id());

            if (!await this.gameService.IsGameBySeller(id, sellerId) && !User.IsAdmin()) return BadRequest();

            var isAdmin = User.IsInRole("Administrator");

            var isGameEdited =
                this.gameService.Edit(gameFormModel, this.gameService.FindById(id).Id, isAdmin);

            if (!await isGameEdited) return BadRequest();

            if (User.IsAdmin())
            {
                TempData["GlobalMessageKey"] = "The game was edited.";
                return RedirectToAction(nameof(AdminController.AdminGames), "Admin");
            }

            TempData["GlobalMessageKey"] = "Your game was edited and waiting for approval.";

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var gameToRemove = this.gameService.FindById(id);

            if (gameToRemove == null) return BadRequest();

            await this.gameService.Delete(gameToRemove);

            TempData["GlobalMessageKey"] = "The game was removed.";

            return !User.IsAdmin()
                ? RedirectToAction(nameof(All))
                : RedirectToAction(nameof(AdminController.AdminGames), "Admin");
        }

        public async Task<IActionResult> All(string searchByTitle, string publisher, GameSorting sorting, bool isMyGames, int currentPage = 1)
        {
            var viewModel = await this.gameService.GetFullGameDetails(searchByTitle, publisher, sorting, currentPage, 5, false, isMyGames, User.Id());

            ViewBag.IsMyGames = isMyGames;

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id, bool isMyGames)
        {
            var game = await this.gameService.Details(id);

            if (game == null) return BadRequest();

            ViewBag.IsMyGames = isMyGames;

            return View(game);
        }

        [Authorize]
        public IActionResult Purchased()
        {
            return View();
        }
    }
}