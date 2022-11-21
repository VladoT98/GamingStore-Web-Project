using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.ShoppingCart;
using GamingStore.Services.Games;

namespace GamingStore.Services.ShoppingCart
{
    public class CartService : ICartService
    {
        private readonly IGameService gameService;

        public CartService(IGameService gameService)
            => this.gameService = gameService;

        public List<CartItemViewModel> GetCartItems(int id, ISession session)
        {
            List<CartItemViewModel> cart = null;

            if (session.GetObjectFromJson<List<CartItemViewModel>>("cart") == null)
            {
                cart = new List<CartItemViewModel>();
                this.AddGameToCart(id, cart);
            }
            else
            {
                cart = session.GetObjectFromJson<List<CartItemViewModel>>("cart");
                var index = this.IsGameInCart(cart, id);
                if (index != -1) cart[index].Quantity++;
                else this.AddGameToCart(id, cart);
            }

            return cart;
        }

        private void AddGameToCart(int id, List<CartItemViewModel> cart)
        {
            var game = this.gameService.FindById(id);
            cart.Add(new CartItemViewModel { Game = game, Quantity = 1 });
        }

        public int IsGameInCart(List<CartItemViewModel> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Game.Id.Equals(id)) return i;
            }

            return -1;
        }
    }
}