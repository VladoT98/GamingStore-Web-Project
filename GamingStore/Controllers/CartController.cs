using GamingStore.Infrastructure.Extensions;
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
            var cart = this.cartService.GetCartItems(id, HttpContext.Session);

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

            var cart = this.cartService.GetCartItems(id, HttpContext.Session);

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
            var items = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("cart");

            var gamesInCart = new GamesInCartViewModel { Items = items };

            if (items != null)
            {
                var price = items.Sum(x => x.Game.Price * x.Quantity);
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