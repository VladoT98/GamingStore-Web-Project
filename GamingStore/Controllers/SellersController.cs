using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.Sellers;
using GamingStore.Services.Sellers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Controllers
{
    public class SellersController : Controller
    {
        private readonly ISellerService sellerService;

        public SellersController(ISellerService sellerService)
            => this.sellerService = sellerService;

        [Authorize]
        public IActionResult Become() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Become(BecomeSellerFormModel sellerModel)
        {
            var userId = User.Id();

            if (await this.sellerService.IsUserSeller(userId)) return BadRequest();

            if (!ModelState.IsValid) return View(sellerModel);

            await this.sellerService.Become(sellerModel, userId);

            TempData["GlobalMessageKey"] = "You just became a seller.";

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
