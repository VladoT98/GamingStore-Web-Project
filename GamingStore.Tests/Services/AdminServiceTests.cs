using GamingStore.Areas.Admin.Models;
using GamingStore.Areas.Admin.Services.Admin;
using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Games;
using GamingStore.Services.Reviews.Models;
using GamingStore.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class AdminServiceTests
    {
        private GamingStoreDbContext data;
        private IAdminService adminService;

        public AdminServiceTests()
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
                SellerId = 1,
                Reviews = new List<GameReview>
                {
                    new GameReview
                    {
                        From = "From",
                        Content = "Content",
                        UserId = "User",
                        ImageUrl = "ImageUrl",
                    },
                    new GameReview
                    {
                        From = "From1",
                        Content = "Content1",
                        UserId = "User1",
                        ImageUrl = "ImageUrl1",
                    }
                }
            });

            this.data.Users.Add(new IdentityUser
            {
                Id = "User",
                Email = "Email",
                PhoneNumber = "Phone"
            });

            this.data.SaveChanges();

            this.adminService = new AdminService(data, MapperMock.Instance, Mock.Of<IGameService>());
        }

        [Fact]
        public async Task GetReviewsInfoShouldReturnFullReviewInfo()
        {
            var result = await this.adminService.GetReviewsInfo("From", null, ReviewSorting.Oldest, 1, 5);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ReviewServiceModel>>(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("From", result.First().From);
        }

        [Fact]
        public async Task GetUserInfoShouldReturnUserInfo()
        {
            this.data.Users.Add(new IdentityUser
            {
                Email = "NewEmail",
                PhoneNumber = "NewPhone"
            });

            await this.data.SaveChangesAsync();

            var result = await this.adminService.GetUsersInfo("Email", null, UserSorting.EmailAscending, 1, 5);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<UserViewModel>>(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Email", result.First().Email);
        }

        [Fact]
        public async Task UsersCountShouldReturnTheCorrectUsersCount()
        {
            this.data.Users.Add(new IdentityUser
            {
                Email = "NewEmail",
                PhoneNumber = null
            });

            var result = await this.adminService.UsersCount("Email", null, UserSorting.HasPhoneNumber);

            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ReviewsCountShouldReturnTheCorrectReviewsCount()
        {
            var result = await this.adminService.ReviewsCount(null, "From1");

            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ApproveGameShouldMakeTheGameApproved()
        {
            this.data.Games.Add(new Game
            {
                Title = "Game1",
                Description = "Description1",
                ImageUrl = "ImageUrl1",
                TrailerUrl = "TrailerUrl1",
                IsApproved = false,
            });

            await this.data.SaveChangesAsync();

            await this.adminService.ApproveGame(2);

            var result = await this.data.Games.FindAsync(2);

            Assert.True(result.IsApproved);
        }

        [Fact]
        public async Task DeleteUserShouldRemoveTheUser()
        {
            var isUserDeleted = await this.adminService.DeleteUser("User");

            var result = this.data.Users.Any();

            Assert.IsType<bool>(isUserDeleted);
            Assert.False(result);
        }
    }
}