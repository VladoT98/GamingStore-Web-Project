using GamingStore.Models.Games;

namespace GamingStore.Services.Games.Models
{
    public class GameServiceModel : GameBaseModel
    {
        public string Description { get; init; }

        public bool IsApproved { get; init; }

        public int ReviewsCount { get; init; }
    }
}