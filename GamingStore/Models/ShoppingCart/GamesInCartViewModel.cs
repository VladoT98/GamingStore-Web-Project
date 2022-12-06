using GamingStore.Models.Games;

namespace GamingStore.Models.ShoppingCart
{
    public class GamesInCartViewModel
    {
        public decimal TotalPrice { get; set; }

        public List<GameBaseModel> Items { get; init; }
    }
}