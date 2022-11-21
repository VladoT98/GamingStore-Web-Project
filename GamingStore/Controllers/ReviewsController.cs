using AutoMapper;
using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.Reviews;
using GamingStore.Services.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IMapper mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add(int id)
            => View(new ReviewFormModel() { GameId = id, IsAdd = true });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(ReviewFormModel reviewFormModel, int id)
        {
            reviewFormModel.GameId = id;

            if (!await this.reviewService.IsGameIdValid(reviewFormModel.GameId))
                ModelState.AddModelError(nameof(reviewFormModel.GameId), "Invalid game.");

            if (!ModelState.IsValid) return View(reviewFormModel);

            await this.reviewService.Add(reviewFormModel, User.Id());

            TempData["GlobalMessageKey"] = "Your review was added successfully.";

            return RedirectToAction(nameof(GamesController.Details), "Games", new { id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var gameId = await this.GetGameId(id);

            var reviewToRemove = await this.reviewService.FindById(id);

            if (reviewToRemove == null) return BadRequest();

            await this.reviewService.Delete(reviewToRemove);

            TempData["GlobalMessageKey"] = "Your review was removed.";

            return RedirectToAction(nameof(GamesController.Details), "Games", new { id = gameId });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await this.reviewService.FindById(id);

            if (review == null) return BadRequest();

            if (review.UserId != User.Id() && !User.IsAdmin()) return Unauthorized();

            var reviewForm = this.mapper.Map<ReviewFormModel>(review);

            return View(reviewForm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReviewFormModel reviewFormModel)
        {
            var gameId = await this.GetGameId(id);

            if (!await this.reviewService.IsGameIdValid(gameId))
                ModelState.AddModelError(nameof(gameId), "Invalid game.");

            if (!ModelState.IsValid) return BadRequest();

            var review = await this.reviewService.FindById(id);
            var reviewId = review.Id;

            var isReviewEdited = await this.reviewService.Edit(reviewFormModel, reviewId);

            if (!isReviewEdited) return BadRequest();

            TempData["GlobalMessageKey"] = !User.IsInRole("Administrator")
                ? "Your review was edited successfully."
                : "Review edited successfully.";

            return User.IsAdmin()
                ? Redirect("/Admin/AdminReviews?area=Admin")
                : RedirectToAction(nameof(GamesController.Details), "Games", new { id = gameId });
        }

        private async Task<int> GetGameId(int id)
        {
            var review = await this.reviewService.FindById(id);
            return review.GameId;
        }
    }
}