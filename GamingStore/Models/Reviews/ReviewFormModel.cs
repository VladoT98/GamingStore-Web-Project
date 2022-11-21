using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models.Reviews
{
    public class ReviewFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The name should be between 5 and 20 characters long.")]
        public string From { get; init; }

        [Display(Name = "Image URL")]
        [Url]
        public string? ImageUrl { get; init; }

        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "The text should be at least 10 characters long.")]
        public string Content { get; init; }

        public int GameId { get; set; }

        public bool IsAdd { get; set; }
    }
}
