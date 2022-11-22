namespace GamingStore.Models.ShoppingCart
{
    public class GamesInCartViewModel
    {
        public decimal TotalPrice { get; set; }

        public List<CartItemViewModel> Items { get; init; }
    }
}