using GamingStore.Areas.Admin.Models;
using GamingStore.Models.Games;
using GamingStore.Services.Home;
using Moq;

namespace GamingStore.Tests.Mocks
{
    public static class HomeServiceMock
    {
        public static IHomeService Instance
        {
            get
            {
                var homeServiceMock = new Mock<IHomeService>();

                homeServiceMock
                    .Setup(x => x.GetBlogPosts())
                    .ReturnsAsync(It.IsAny<IEnumerable<BlogViewModel>>());
                homeServiceMock
                    .Setup(x => x.GetNewArrivals())
                    .ReturnsAsync(It.IsAny<IEnumerable<GameBaseModel>>());

                return homeServiceMock.Object;
            }
        }
    }
}