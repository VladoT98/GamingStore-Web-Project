using System.ComponentModel.DataAnnotations;
using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Reviews.Models;

namespace GamingStore.Models.Reviews
{
    public class ReviewSearchViewModel : Paging
    {
        [Display(Name = "Search by Username")]
        public string SearchByUsername { get; init; }

        [Display(Name = "Search by Game")]
        public string SearchByGame { get; init; }

        [Display(Name = "Order By")]
        public ReviewSorting Sorting { get; init; }

        public IEnumerable<ReviewServiceModel> Reviews { get; init; }
    }
}