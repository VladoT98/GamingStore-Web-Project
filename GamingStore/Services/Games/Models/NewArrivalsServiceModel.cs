namespace GamingStore.Services.Games.Models
{
    public class NewArrivalsServiceModel
    {
        public int Id { get; set; }

        public string Title { get; init; }

        public decimal? Price { get; init; }

        public string ImageUrl { get; init; }

        public string PublisherName { get; init; }
    }
}
