using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Models.Games;
using GamingStore.Services.Games;
using GamingStore.Services.ShoppingCart;
using GamingStore.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class CartServiceTests
    {
        private GamingStoreDbContext data;
        private IGameService gameService;
        private ICartService cartService;

        public CartServiceTests()
        {
            this.data = DatabaseMock.Instance;
            this.data.Games.Add(new Game { Title = "", Description = "", ImageUrl = "", TrailerUrl = "", IsApproved = true });
            this.data.SaveChanges();

            this.gameService = new GameService(this.data, null, null, null);
            this.cartService = new CartService(gameService);
        }

        [Fact]
        public void AddToCartShouldAddProperly()
        {
            var result = this.cartService.AddToCart(1, Mock.Of<ISession>());

            Assert.NotNull(result);
            Assert.IsType<List<GameBaseModel>>(result);
            Assert.Single(result);
        }

        [Fact]
        public void IsGameInCartShouldReturnPositiveIntegerIfTheGameIsInTheCart()
        {
            var cart = this.cartService.AddToCart(1, Mock.Of<ISession>());

            var result = this.cartService.IsGameInCart(cart, 1);

            Assert.IsType<int>(result);
            Assert.True(result >= 0);
        }

        [Fact]
        public void IsGameInCartShouldReturnNegativeIntegerIfTheGameIsNotInTheCart()
        {
            var cart = this.cartService.AddToCart(1, Mock.Of<ISession>());

            var result = this.cartService.IsGameInCart(cart, 2);

            Assert.IsType<int>(result);
            Assert.True(result < 0);
        }
    }
}