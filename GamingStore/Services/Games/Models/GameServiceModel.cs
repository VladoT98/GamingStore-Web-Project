using GamingStore.Models.Games;

namespace GamingStore.Services.Games.Models
{
    public class GameServiceModel : GameBaseModel
    {
        public string Description { get; init; }

        public bool IsApproved { get; set; }

        public int ReviewsCount { get; set; }
    }
}
