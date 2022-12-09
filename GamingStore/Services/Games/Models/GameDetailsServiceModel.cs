using GamingStore.Services.Reviews.Models;

namespace GamingStore.Services.Games.Models
{
    public class GameDetailsServiceModel : GameServiceModel
    {
        public int Id { get; init; }

        public int ReleaseYear { get; init; }

        public string IsCrossPlay { get; init; }

        public string PlatformName { get; init; }

        public string GenreName { get; init; }

        public string SellerName { get; init; }

        public string TrailerUrl { get; set; }

        public string UserId { get; init; }

        public int GenreId { get; init; }

        public int PlatformId { get; init; }

        public int PublisherId { get; init; }

        public List<ReviewServiceModel> Reviews { get; init; }
    }
}