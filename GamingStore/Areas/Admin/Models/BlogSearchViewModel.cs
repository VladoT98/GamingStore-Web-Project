using System.ComponentModel.DataAnnotations;
using GamingStore.Models;

namespace GamingStore.Areas.Admin.Models
{
    public class BlogSearchViewModel : Paging
    {
        [Display(Name = "Search by Title")]
        public string SearchByTitle { get; init; }

        public IEnumerable<BlogViewModel> Blogs { get; init; }
    }
}
