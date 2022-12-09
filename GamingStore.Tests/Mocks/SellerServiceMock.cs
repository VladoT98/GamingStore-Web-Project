using GamingStore.Services.Sellers;
using Moq;

namespace GamingStore.Tests.Mocks
{
    public static class SellerServiceMock
    {
        public static ISellerService Instance
        {
            get
            {
                var sellerServiceMock = new Mock<ISellerService>();

                sellerServiceMock.Setup(x => x.GetSellerId(It.IsAny<string>()))
                    .ReturnsAsync(1);

                return sellerServiceMock.Object;
            }
        }
    }
}
