namespace GamingStore.Models.Games
{
    public class GameBaseModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public decimal? Price { get; init; }

        public string PublisherName { get; init; }

        public string ImageUrl { get; init; }
    }
}