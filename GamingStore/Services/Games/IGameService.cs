using GamingStore.Data.Models;
using GamingStore.Models;
using GamingStore.Models.Games;
using GamingStore.Services.Games.Models;

namespace GamingStore.Services.Games
{
    public interface IGameService
    {
        Task<GameSearchViewModel> GetFullGameDetails(string searchByTitle, string publisher, GameSorting sorting,
            int currentPage, int gamesPerPage, bool isAdmin, bool isMyGames, string userId);

        Task Delete(Game game);

        Task<int> Add(GameFormModel gameModel, string userId);

        Task<bool> Edit(GameFormModel gameFormModel, int id, bool isAdmin);

        Task<GameDetailsServiceModel> Details(int gameId);

        Game FindById(int gameId);

        Task<IEnumerable<GameGenreServiceModel>> GameGenres();

        Task<IEnumerable<GamePlatformServiceModel>> GamePlatforms();

        Task<bool> IsGenresExist(GameFormModel gameFormModel);

        Task<bool> IsPublisherExist(GameFormModel gameFormModel);

        Task<bool> IsPlatformExist(GameFormModel gameFormModel);

        Task<bool> IsGameBySeller(int id, int sellerId);
    }
}
