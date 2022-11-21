using System.ComponentModel.DataAnnotations;

namespace GamingStore.Areas.Admin.Models
{
    public class BlogFormModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; init; }

        [Required]
        [MinLength(10)]
        public string Content { get; init; }

        [Required]
        public string ImageUrl { get; init; }
    }
}