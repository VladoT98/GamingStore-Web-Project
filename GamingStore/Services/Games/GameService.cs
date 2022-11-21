using GamingStore.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamingStore.Data.Models;
using GamingStore.Models;
using GamingStore.Models.Games;
using GamingStore.Services.Games.Models;
using GamingStore.Services.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GamingStore.Services.Games
{
    public class GameService : IGameService
    {
        private readonly GamingStoreDbContext data;
        private readonly ISellerService sellerService;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public GameService(GamingStoreDbContext data, IMapper mapper, IMemoryCache cache, ISellerService sellerService)
        {
            this.data = data;
            this.mapper = mapper;
            this.cache = cache;
            this.sellerService = sellerService;
        }

        public async Task Delete(Game game)
        {
            this.data.Games.Remove(game);
            await this.data.SaveChangesAsync();
        }

        public async Task<int> Add(GameFormModel gameModel, string userId)
        {
            var game = this.mapper.Map<Game>(gameModel);
            game.SellerId = await this.sellerService.GetSellerId(userId);
            game.PublisherId = await this.GetPublisherId(gameModel.PublisherName);

            await this.data.Games.AddAsync(game);
            await this.data.SaveChangesAsync();

            return game.Id;
        }

        public async Task<bool> Edit(GameFormModel gameModel, int id, bool isAdmin)
        {
            var gameToEdit = await this.data.Games.FindAsync(id);

            if (gameToEdit == null) return false;

            gameToEdit.Title = gameModel.Title;
            gameToEdit.Price = gameModel.Price;
            gameToEdit.Description = gameModel.Description;
            gameToEdit.ImageUrl = gameModel.ImageUrl;
            gameToEdit.ReleaseYear = gameModel.ReleaseYear;
            gameToEdit.IsCrossPlay = gameModel.IsCrossPlay == "Yes";
            gameToEdit.PublisherId = await this.GetPublisherId(gameModel.PublisherName);
            gameToEdit.GenreId = gameModel.GenreId;
            gameToEdit.PlatformId = gameModel.PlatformId;
            gameToEdit.IsApproved = isAdmin;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<GameDetailsServiceModel> Details(int id)
            => await this.data.Games
                .Where(x => x.Id == id)
                .ProjectTo<GameDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<GameServiceModel>> GetFilteredGames(string title, string publisher, GameSorting sorting, int currentPage, int gamesPerPage, bool isAdmin, bool isMyGames, string userId)
        {
            var gamesQuery = await GetGamesQuery(isMyGames, userId, sorting);

            if (!string.IsNullOrWhiteSpace(title))
                gamesQuery = gamesQuery
                    .Where(x => x.Title.ToLower()
                        .Contains(title.ToLower()));

            if (!string.IsNullOrWhiteSpace(publisher))
                gamesQuery = gamesQuery
                    .OrderBy(x => x)
                    .Where(x => x.Publisher.Name.ToLower() == publisher.ToLower());

            gamesQuery = sorting switch
            {
                GameSorting.PriceAscending => gamesQuery.OrderBy(x => x.Price),
                GameSorting.PriceDescending => gamesQuery.OrderByDescending(x => x.Price),
                GameSorting.ReleaseYearAscending => gamesQuery.OrderBy(x => x.ReleaseYear),
                GameSorting.ReleaseYearDescending => gamesQuery.OrderByDescending(x => x.ReleaseYear),
                GameSorting.TitleAlphabetically => gamesQuery.OrderBy(x => x.Title),
                GameSorting.ReviewsCount => gamesQuery.OrderByDescending(x => x.Reviews.Count()),
                GameSorting.FreeGames => gamesQuery.OrderByDescending(x => x.Id),
                GameSorting.RecentlyAdded or _ => gamesQuery.OrderByDescending(x => x.Id),
            };

            var result = await gamesQuery
                .Skip((currentPage - 1) * gamesPerPage)
                .Take(gamesPerPage)
                .ProjectTo<GameServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return isAdmin || isMyGames
                ? result
                : result.Where(x => x.IsApproved);
        }

        public async Task<GameSearchViewModel> GetFullGameDetails(string searchByTitle, string publisher, GameSorting sorting, int currentPage, int gamesPerPage, bool isAdmin, bool isMyGames, string userId)
        {
            var games = await this
                .GetFilteredGames(searchByTitle, publisher, sorting, currentPage, gamesPerPage, isAdmin, isMyGames, userId);

            var publishers = this.data.Publishers
                .Select(x => x.Name);

            var gamesCount = await this.GamesCount(searchByTitle, publisher, isMyGames, userId, sorting);

            return new GameSearchViewModel()
            {
                SearchByTitle = searchByTitle,
                Publisher = publisher,
                Sorting = sorting,
                Games = games,
                Publishers = publishers,
                IsAdmin = isAdmin,
                TotalItems = gamesCount,
                CurrentPage = currentPage,
                ItemsPerPage = gamesPerPage
            };
        }

        public async Task<IEnumerable<GameGenreServiceModel>> GameGenres()
            => await GetInfo<GameGenreServiceModel>("GenresCacheKey");

        public async Task<IEnumerable<GamePlatformServiceModel>> GamePlatforms()
            => await GetInfo<GamePlatformServiceModel>("PlatformsCacheKey");

        public async Task<bool> IsGenresExist(GameFormModel gameFormModel)
            => await this.data.Genres.AnyAsync(x => x.Id == gameFormModel.GenreId);

        public async Task<bool> IsPublisherExist(GameFormModel gameFormModel)
            => await this.data.Publishers.AnyAsync(x => x.Name == gameFormModel.PublisherName);

        public async Task<bool> IsPlatformExist(GameFormModel gameFormModel)
            => await this.data.Platforms.AnyAsync(x => x.Id == gameFormModel.PlatformId);

        public async Task<int> GamesCount(string title, string publisher, bool isMyGames, string userId, GameSorting sorting)
        {
            var gamesQuery = await GetGamesQuery(isMyGames, userId, sorting);

            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(publisher))
            {
                return await gamesQuery.CountAsync();
            }

            if (title != null)
                gamesQuery = gamesQuery
                    .Where(x => x.Title.ToLower().Contains(title.ToLower()));

            if (publisher != null)
                gamesQuery = gamesQuery
                    .Where(x => x.Publisher.Name.ToLower().Contains(publisher.ToLower()));

            return await gamesQuery.CountAsync();
        }

        public Game FindById(int gameId)
            => this.data.Games
                .FirstOrDefault(x => x.Id == gameId);

        public async Task<bool> IsGameBySeller(int id, int sellerId)
            => await this.data.Games
                .AnyAsync(x => x.Id == id && x.SellerId == sellerId);

        private async Task<int> GetPublisherId(string publisherName)
        {
            var publisher = await this.data.Publishers
                .FirstOrDefaultAsync(x => x.Name == publisherName);

            return publisher.Id;
        }

        private async Task<IEnumerable<T>> GetInfo<T>(string cacheKey)
        {
            var entities = this.cache.Get<List<T>>(cacheKey);

            if (entities == null)
            {
                if (typeof(T) == typeof(GameGenreServiceModel))
                {
                    entities = await this.data.Genres
                        .ProjectTo<T>(this.mapper.ConfigurationProvider)
                        .ToListAsync();
                }
                else if (typeof(T) == typeof(GamePublisherServiceModel))
                {
                    entities = await this.data.Publishers
                        .ProjectTo<T>(this.mapper.ConfigurationProvider)
                        .ToListAsync();
                }
                else if (typeof(T) == typeof(GamePlatformServiceModel))
                {
                    entities = await this.data.Platforms
                        .ProjectTo<T>(this.mapper.ConfigurationProvider)
                        .ToListAsync();
                }

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                this.cache.Set(cacheKey, entities, cacheOptions);
            }

            return entities;
        }

        private async Task<IQueryable<Game>> GetGamesQuery(bool isMyGames, string userId, GameSorting sorting)
        {
            var sellerId = await this.sellerService.GetSellerId(userId);

            var gamesQuery = isMyGames
                ? this.data.Games.Where(x => x.SellerId == sellerId).AsQueryable()
                : this.data.Games.AsQueryable();

            if (sorting == GameSorting.FreeGames)
                gamesQuery = gamesQuery.Where(x => x.Price == 0 || x.Price == null);
            else if (sorting == GameSorting.PriceAscending || sorting == GameSorting.PriceDescending)
                gamesQuery = gamesQuery.Where(x => x.Price > 0);

            return gamesQuery;
        }
    }
}