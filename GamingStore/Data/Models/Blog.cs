using System.ComponentModel.DataAnnotations;

namespace GamingStore.Data.Models
{
    public class Blog
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
