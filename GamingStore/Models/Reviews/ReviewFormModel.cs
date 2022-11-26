using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models.Reviews
{
    public class ReviewFormModel
    {
        [Display(Name = "Image URL")]
        [Url]
        public string? ImageUrl { get; init; }

        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "The text should be at least 10 characters long.")]
        public string Content { get; init; }

        public int GameId { get; set; }

        public bool IsAdd { get; init; }
    }
}