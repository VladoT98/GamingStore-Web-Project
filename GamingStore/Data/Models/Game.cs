using System.ComponentModel.DataAnnotations;

namespace GamingStore.Data.Models
{
    public class Game
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public decimal? Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string TrailerUrl { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public bool IsCrossPlay { get; set; }

        public bool IsApproved { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; init; }

        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; init; }

        public int PlatformId { get; set; }
        public virtual Platform Platform { get; init; }

        public int SellerId { get; set; }
        public virtual Seller Seller { get; init; }

        public virtual IEnumerable<GameReview> Reviews { get; set; } = new HashSet<GameReview>();

        public virtual IEnumerable<Platform> Platforms { get; set; } = new HashSet<Platform>();
    }
}
