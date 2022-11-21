using System.ComponentModel.DataAnnotations;

namespace GamingStore.Data.Models
{
    public class Seller
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(50)]
        public string Name { get; init; }

        [Required]
        public string PhoneNumber { get; init; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Game> Games { get; init; } = new HashSet<Game>();
    }
}
