using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models.Publishers
{
    public class RegisterPublisherFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Employees { get; set; }
    }
}
