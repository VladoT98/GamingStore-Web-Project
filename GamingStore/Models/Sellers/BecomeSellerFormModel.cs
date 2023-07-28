using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models.Sellers
{
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; init; }

        [Required]
        [RegularExpression(@"^\+\d{3}\s\d{2}\s\d{3}\s\d{4}$",
            ErrorMessage = "Phone number must be in format +111 11 111 1111")]
        public string PhoneNumber { get; init; }
    }
}