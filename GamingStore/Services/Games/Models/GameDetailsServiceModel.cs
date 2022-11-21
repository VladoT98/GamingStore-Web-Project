using GamingStore.Services.Reviews.Models;

namespace GamingStore.Services.Games.Models
{
    public class GameDetailsServiceModel : GameServiceModel
    {
        public string IsCrossPlay { get; init; }

        public string PlatformName { get; init; }

        public int ReleaseYear { get; init; }

        public string GenreName { get; init; }

        public string SellerName { get; set; }

        public string UserId { get; set; }

        public int GenreId { get; set; }

        public int PlatformId { get; set; }

        public int PublisherId { get; set; }

        public List<ReviewServiceModel> Reviews { get; set; }
    }
}
