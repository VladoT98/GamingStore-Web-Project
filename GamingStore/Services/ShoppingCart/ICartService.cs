using GamingStore.Models.ShoppingCart;

namespace GamingStore.Services.ShoppingCart
{
    public interface ICartService
    {
        List<CartItemViewModel> GetCartItems(int id, ISession session);

        int IsGameInCart(List<CartItemViewModel> cart, int id);
    }
}