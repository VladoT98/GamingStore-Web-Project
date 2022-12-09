using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Infrastructure.Enums;
using GamingStore.Models.Games;
using GamingStore.Services.Games;
using GamingStore.Services.Games.Models;
using GamingStore.Tests.Mocks;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class GameServiceTests
    {
        private GamingStoreDbContext data;
        private IGameService gameService;

        public GameServiceTests()
        {
            this.data = DatabaseMock.Instance;

            this.data.Games.Add(new Game
            {
                Title = "Game",
                Description = "Description",
                ImageUrl = "ImageUrl",
                TrailerUrl = "TrailerUrl",
                IsApproved = true,
                GenreId = 1,
                PublisherId = 1,
                PlatformId = 1,
                SellerId = 1
            });

            this.data.Sellers.Add(new Seller
            {
                Name = "Seller",
                PhoneNumber = "Phone",
                UserId = "User"
            });

            this.data.Publishers.Add(new Publisher { Name = "Publisher", Description = "Description" });

            this.data.Genres.Add(new Genre { Name = "Genre" });

            this.data.Platforms.Add(new Platform { Name = "Platform" });

            this.data.SaveChanges();

            this.gameService = new GameService(data, SellerServiceMock.Instance, MapperMock.Instance, Mock.Of<IMemoryCache>());
        }

        [Fact]
        public async Task DeleteShouldRemoveAGame()
        {
            await this.gameService.Delete(this.data.Games.First());

            Assert.Empty(this.data.Games);
        }

        [Fact]
        public async Task AddShouldAddAGame()
        {
            var result = await this.gameService.Add(new GameFormModel()
            {
                Title = "Game1",
                Description = "Description1",
                ImageUrl = "ImageUrl1",
                TrailerUrl = "TrailerUrl1",
                PublisherName = "Publisher"
            }, "User");

            Assert.Equal(2, this.data.Games.Count());
        }

        [Fact]
        public async Task EditShouldEditGameProperly()
        {
            var result = await this.gameService.Edit(new GameFormModel
            {
                Title = "NewGame",
                PublisherName = "Publisher"
            }, 1, false);

            Assert.IsType<bool>(result);
            Assert.Equal("NewGame", this.data.Games.First().Title);
        }

        [Fact]
        public async Task DetailsShouldReturnTheCorrectGameDetails()
        {
            var result = await this.gameService.Details(1);

            Assert.IsType<GameDetailsServiceModel>(result);
            Assert.Equal(result.Title, this.data.Games.First().Title);
        }

        [Fact]
        public async Task GetFullGameDetailsShouldReturnTheCorrectFullGameDetails()
        {
            this.data.Games.Add(new Game
            {
                Title = "Game1",
                Description = "Description1",
                ImageUrl = "ImageUrl1",
                TrailerUrl = "TrailerUrl1",
                SellerId = 1,
                IsApproved = true,
                Price = 9.99m
            });

            await this.data.SaveChangesAsync();

            var result = await this.gameService
                .GetFullGameDetails("Game", null, GameSorting.FreeGames, 1, 5, false, false, "User");

            Assert.NotNull(result);
            Assert.IsType<GameSearchViewModel>(result);
            Assert.Single(result.Games);
            Assert.Equal("Game", result.Games.First().Title);
        }

        [Fact]
        public async Task IsGenresExistShouldReturnTrueIfGenreExists()
        {
            var gameFormModel = MapperMock.Instance.Map<GameFormModel>(this.data.Games.First());

            var result = await this.gameService.IsGenresExist(gameFormModel);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task IsGenresExistShouldReturnFalseIfGenreNotExists()
        {
            var result = await this.gameService.IsGenresExist(new GameFormModel()
            {
                Title = "",
                Description = "",
                ImageUrl = "",
                TrailerUrl = "",
                GenreId = 2
            });

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public async Task IsPublisherExistShouldReturnTrueIfPublisherExists()
        {
            var gameFormModel = MapperMock.Instance.Map<GameFormModel>(this.data.Games.First());

            var result = await this.gameService.IsPublisherExist(gameFormModel);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task IsPublisherExistShouldReturnFalseIfPublisherNotExists()
        {
            var result = await this.gameService.IsPublisherExist(new GameFormModel()
            {
                Title = "",
                Description = "",
                ImageUrl = "",
                TrailerUrl = "",
                PublisherName = "NewPublisher"
            });

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public async Task IsPlatformExistShouldReturnTrueIfPlatformExists()
        {
            var gameFormModel = MapperMock.Instance.Map<GameFormModel>(this.data.Games.First());

            var result = await this.gameService.IsPlatformExist(gameFormModel);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task IsPlatformExistShouldReturnFalseIfPlatformNotExists()
        {
            var result = await this.gameService.IsPlatformExist(new GameFormModel()
            {
                Title = "",
                Description = "",
                ImageUrl = "",
                TrailerUrl = "",
                PlatformId = 2
            });

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void FindByIdShouldReturnTheCorrectGame()
        {
            var result = this.gameService.FindById(1);

            Assert.IsType<Game>(result);
            Assert.NotNull(result);
            Assert.Equal("Game", result.Title);
        }

        [Fact]
        public async Task IsGameBySellerShouldReturnTrue()
        {
            var result = await this.gameService.IsGameBySeller(1, 1);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }
    }
}