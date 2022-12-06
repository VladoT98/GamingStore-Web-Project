using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.Games;
using GamingStore.Models.ShoppingCart;
using GamingStore.Services.Games;
using GamingStore.Services.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IGameService gameService;

        public CartController(ICartService cartService, IGameService gameService)
        {
            this.cartService = cartService;
            this.gameService = gameService;
        }

        [Authorize]
        public IActionResult Add(int id, bool directAdd)
        {
            var cart = this.cartService.AddToCart(id, HttpContext.Session);

            if (cart == null) return BadRequest();

            HttpContext.Session.SetObjectAsJson("cart", cart);

            TempData["GlobalMessageKey"] = "Game added to cart.";

            return directAdd
                ? RedirectToAction(nameof(ViewCart))
                : RedirectToAction(nameof(GamesController.Details), "Games", new { id });
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            if (id == -1)
            {
                HttpContext.Session.SetObjectAsJson("cart", null);
                return Ok();
            }

            var game = this.gameService.FindById(id);

            if (game == null) return BadRequest();

            var cart = HttpContext.Session.GetObjectFromJson<List<GameBaseModel>>("cart");

            int index = this.cartService.IsGameInCart(cart, id);

            cart.RemoveAt(index);

            TempData["GlobalMessageKey"] = "Game removed from cart.";

            HttpContext.Session.SetObjectAsJson("cart", cart);

            return cart.Any()
                ? RedirectToAction(nameof(ViewCart), "Cart")
                : RedirectToAction(nameof(GamesController.All), "Games");
        }

        [Authorize]
        public IActionResult ViewCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<GameBaseModel>>("cart");

            var gamesInCart = new GamesInCartViewModel { Items = cart };

            if (cart != null)
            {
                var price = cart.Sum(x => x.Price);
                gamesInCart.TotalPrice = price ?? 0m;
            }

            return View(gamesInCart);
        }

        [Authorize]
        public IActionResult Checkout()
        {
            this.Remove(-1);
            return RedirectToAction(nameof(GamesController.Purchased), "Games");
        }
    }
}