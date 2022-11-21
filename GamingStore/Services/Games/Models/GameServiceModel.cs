namespace GamingStore.Services.Games.Models
{
    public class GameServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public decimal? Price { get; init; }

        public string ImageUrl { get; init; }

        public string Description { get; init; }

        public string PublisherName { get; init; }

        public bool IsApproved { get; set; }

        public int ReviewsCount { get; set; }
    }
}
