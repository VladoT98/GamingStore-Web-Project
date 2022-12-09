using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Models.Reviews;
using GamingStore.Services.Reviews;
using GamingStore.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class ReviewServiceTests
    {
        private GamingStoreDbContext data;
        private IReviewService reviewService;

        public ReviewServiceTests()
        {
            this.data = DatabaseMock.Instance;

            this.data.Users.Add(new IdentityUser { Id = "User", Email = "Email@gmail.com" });

            this.data.GameReviews.Add(new GameReview
            {
                From = "From",
                ImageUrl = "ImageUrl",
                Content = "Content",
                UserId = "User",
                GameId = 1
            });

            this.data.SaveChanges();

            this.reviewService = new ReviewService(this.data, MapperMock.Instance);
        }

        [Fact]
        public async Task AddShouldAddReviewProperly()
        {
            await this.reviewService.Add(new ReviewFormModel { Content = "" }, "User");

            Assert.Equal(2, this.data.GameReviews.Count());
        }

        [Fact]
        public async Task DeleteShouldRemoveReviewProperly()
        {
            await this.reviewService.Delete(this.data.GameReviews.First());

            Assert.Empty(this.data.GameReviews);
        }

        [Fact]
        public async Task EditShouldEditReviewProperly()
        {
            var test = this.data.GameReviews.ToList();
            var result = await this.reviewService.Edit(new ReviewFormModel { Content = "NewContent" }, 1);

            Assert.IsType<bool>(result);
            Assert.Equal("NewContent", this.data.GameReviews.First().Content);
        }

        [Fact]
        public async Task IsGameValidShouldReturnTrueIfTheGameIsValid()
        {
            await this.data.Games.AddAsync(new Game
            {
                Title = "",
                Description = "",
                ImageUrl = "",
                TrailerUrl = ""
            });

            await this.reviewService.Add(new ReviewFormModel { Content = "", GameId = 1 }, "User");

            var result = await this.reviewService.IsGameIdValid(1);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task FindByIdShouldReturnTheCorrectReview()
        {
            var result = await this.reviewService.FindById(1);

            Assert.NotNull(result);
            Assert.IsType<GameReview>(result);
            Assert.Equal(this.data.GameReviews.First(), result);
        }

        [Fact]
        public async Task IsAllowedToReviewShouldReturnTrueIfUserNotReviewsTheGame()
        {
            this.data.Users.Add(new IdentityUser { Id = "NewUser", Email = "NewUserEmail@gmail.com" });
            await this.data.SaveChangesAsync();

            var result = await this.reviewService.IsAllowedToReview("NewUser", 1);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task IsAllowedToReviewShouldReturnFalseIfUserReviewedTheGame()
        {
            var result = await this.reviewService.IsAllowedToReview("User", 1);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}