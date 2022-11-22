using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models.Sellers
{
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; init; }

        [Required]
        [RegularExpression(@"[+][0-9]{3} [0-9]{2} [0-9]{3} [0-9]{3}",
            ErrorMessage = "Phone number must be in format +111 11 111 1111")]
        public string PhoneNumber { get; init; }
    }
}