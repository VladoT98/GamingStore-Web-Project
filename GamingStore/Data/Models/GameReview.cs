using System.ComponentModel.DataAnnotations;

namespace GamingStore.Data.Models
{
    public class GameReview
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(20)]
        public string From { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; init; }
    }
}
