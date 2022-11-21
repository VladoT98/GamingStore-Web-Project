using System.ComponentModel.DataAnnotations;

namespace GamingStore.Data.Models
{
    public class Platform
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(50)]
        public string Name { get; init; }

        public virtual IEnumerable<Game> Games { get; init; } = new HashSet<Game>();
    }
}