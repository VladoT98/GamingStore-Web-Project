namespace GamingStore.Services.Reviews.Models
{
    public class ReviewServiceModel
    {
        public int Id { get; init; }

        public string From { get; init; }

        public string ImageUrl { get; init; }

        public string Content { get; init; }

        public string UserId { get; init; }

        public string Game { get; init; }
    }
}
