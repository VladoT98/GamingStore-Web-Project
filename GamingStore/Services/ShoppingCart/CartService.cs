using GamingStore.Infrastructure.Extensions;
using GamingStore.Models.Games;
using GamingStore.Services.Games;

namespace GamingStore.Services.ShoppingCart
{
    public class CartService : ICartService
    {
        private readonly IGameService gameService;

        public CartService(IGameService gameService)
            => this.gameService = gameService;

        public List<GameBaseModel> AddToCart(int id, ISession session)
        {
            var game = this.gameService.FindById(id);

            if (game == null) return null;

            var cart = session.GetObjectFromJson<List<GameBaseModel>>("cart");

            if (cart == null) cart = new List<GameBaseModel>();

            cart.Add(new GameBaseModel
            {
                Id = id,
                Title = game.Title,
                ImageUrl = game.ImageUrl,
                Price = game.Price
            });

            return cart;
        }

        public int IsGameInCart(List<GameBaseModel> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id.Equals(id)) return i;
            }

            return -1;
        }
    }
}