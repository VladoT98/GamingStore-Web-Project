using GamingStore.Models.Publishers;
using GamingStore.Services.Publishers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;

        public PublishersController(IPublisherService publisherService)
            => this.publisherService = publisherService;

        [Authorize]
        public IActionResult Register()
            => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterPublisherFormModel publisherModel)
        {
            if (!ModelState.IsValid) return View(publisherModel);

            await this.publisherService.Register(publisherModel);

            TempData["GlobalMessageKey"] = "Publisher added successfully!";

            return RedirectToAction(nameof(GamesController.Add), "Games");
        }
    }
}
