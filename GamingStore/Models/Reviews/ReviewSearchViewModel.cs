using System.ComponentModel.DataAnnotations;
using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Reviews.Models;

namespace GamingStore.Models.Reviews
{
    public class ReviewSearchViewModel : Paging
    {
        [Display(Name = "Search by Username")]
        public string SearchByUsername { get; set; }

        [Display(Name = "Search by Game")]
        public string SearchByGame { get; set; }

        [Display(Name = "Order By")]
        public ReviewSorting Sorting { get; set; }

        public IEnumerable<ReviewServiceModel> Reviews { get; set; }
    }
}
