using System.ComponentModel.DataAnnotations;

namespace GamingStore.Data.Models
{
    public class Genre
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(20)]
        public string Name { get; init; }

        public virtual IEnumerable<Game> Games { get; init; } = new HashSet<Game>();
    }
}
