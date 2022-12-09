using GamingStore.Data;
using GamingStore.Models.Sellers;
using GamingStore.Services.Sellers;
using GamingStore.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class SellerServiceTests
    {
        private GamingStoreDbContext data;
        private ISellerService sellerService;

        public SellerServiceTests()
        {
            this.data = DatabaseMock.Instance;
            this.data.Users.Add(new IdentityUser { Id = "User" });
            this.data.SaveChanges();

            this.sellerService = new SellerService(data, MapperMock.Instance);
        }

        [Fact]
        public async Task BecomeShouldMakeTheUserASeller()
        {
            await this.sellerService.Become(new BecomeSellerFormModel { Name = "", PhoneNumber = "" }, "User");

            var result = await this.sellerService.IsUserSeller("User");

            Assert.True(result);
        }

        [Fact]
        public async Task GetSellerIdShouldReturnCorrectSellerId()
        {
            await this.sellerService.Become(new BecomeSellerFormModel { Name = "", PhoneNumber = "" }, "User");

            var result = await this.sellerService.GetSellerId("User");

            Assert.Equal(1, result);
        }
    }
}