using System.ComponentModel.DataAnnotations;
using GamingStore.Services.Games.Models;

namespace GamingStore.Models.Games
{
    public class GameFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The title should be between 5 and 20 characters long.")]
        public string Title { get; init; }

        public decimal? Price { get; init; }

        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "The description should be at least 10 characters long.")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Range(1998, 2022)]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; init; }

        [Required]
        [Display(Name = "Is Cross Play")]
        public string IsCrossPlay { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Publisher name should be between 3 and 20 characters long.")]
        [Display(Name = "Publisher")]
        public string PublisherName { get; init; }

        [Display(Name = "Platform")]
        public int PlatformId { get; init; }

        public IEnumerable<GameGenreServiceModel>? Genres { get; set; }

        public IEnumerable<GamePlatformServiceModel>? Platforms { get; set; }
    }
}