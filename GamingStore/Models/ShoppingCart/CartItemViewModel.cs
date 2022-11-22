using GamingStore.Data.Models;

namespace GamingStore.Models.ShoppingCart
{
    public class CartItemViewModel
    {
        public Game Game { get; init; }

        public int Quantity { get; set; }
    }
}