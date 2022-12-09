using GamingStore.Controllers;
using GamingStore.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GamingStore.Tests.Controllers
{
    public class HomeControllerTests
    {
        private HomeController homeController;

        public HomeControllerTests()
            => this.homeController = new HomeController(HomeServiceMock.Instance);

        [Fact]
        public async Task IndexShouldReturnView()
        {
            var result = await this.homeController.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PrivacyShouldReturnView()
        {
            var result = this.homeController.Privacy();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ContactUsShouldReturnView()
        {
            var result = this.homeController.ContactUs();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AboutUsShouldReturnView()
        {
            var result = this.homeController.AboutUs();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ContactedResponseShouldReturnView()
        {
            var result = this.homeController.ContactedResponse();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            var result = this.homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
