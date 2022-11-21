using System.ComponentModel.DataAnnotations;
using GamingStore.Services.Games.Models;

namespace GamingStore.Models.Games
{
    public class GameSearchViewModel : Paging
    {
        [Display(Name = "Search by Title")]
        public string SearchByTitle { get; init; }

        [Display(Name = "Order By")]
        public GameSorting Sorting { get; init; }

        public string Publisher { get; init; }

        public int ReviewsCount { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<GameServiceModel> Games { get; init; }

        public IEnumerable<string> Publishers { get; set; }
    }
}
