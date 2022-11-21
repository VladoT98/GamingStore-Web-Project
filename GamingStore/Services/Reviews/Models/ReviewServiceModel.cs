namespace GamingStore.Services.Reviews.Models
{
    public class ReviewServiceModel
    {
        public int Id { get; set; }

        public string From { get; init; }

        public string ImageUrl { get; init; }

        public string Content { get; init; }

        public string UserId { get; set; }

        public string Game { get; set; }
    }
}
