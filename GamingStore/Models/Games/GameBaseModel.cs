namespace GamingStore.Models.Games
{
    public class GameBaseModel
    {
        public int Id { get; set; }

        public string Title { get; init; }

        public decimal? Price { get; set; }

        public string PublisherName { get; init; }

        public string ImageUrl { get; set; }
    }
}
