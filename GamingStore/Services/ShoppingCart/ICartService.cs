using GamingStore.Models.Games;

namespace GamingStore.Services.ShoppingCart
{
    public interface ICartService
    {
        List<GameBaseModel> AddToCart(int id, ISession session);

        int IsGameInCart(List<GameBaseModel> cart, int id);
    }
}