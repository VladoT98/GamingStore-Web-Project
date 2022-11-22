using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models.Publishers
{
    public class RegisterPublisherFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; init; }

        public string Description { get; init; }

        public int? Employees { get; init; }
    }
}