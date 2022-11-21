namespace GamingStore.Services.Games.Models
{
    public class GamesBySellerServiceModel : IGameModel
    {
        public int Id { get; set; }

        public string Title { get; init; }

        public string PublisherName { get; init; }

        public string ImageUrl { get; set; }
    }
}
