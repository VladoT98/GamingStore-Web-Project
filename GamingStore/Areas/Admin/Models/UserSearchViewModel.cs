using System.ComponentModel.DataAnnotations;
using GamingStore.Infrastructure.Enums;
using GamingStore.Models;

namespace GamingStore.Areas.Admin.Models
{
    public class UserSearchViewModel : Paging
    {
        [Display(Name = "Search by Email")]
        public string SearchByEmail { get; init; }

        [Display(Name = "Search by Phone Number")]
        public string SearchByPhoneNumber { get; init; }

        [Display(Name = "Order By")]
        public UserSorting Sorting { get; init; }

        public IEnumerable<UserViewModel> Users { get; init; }
    }
}
