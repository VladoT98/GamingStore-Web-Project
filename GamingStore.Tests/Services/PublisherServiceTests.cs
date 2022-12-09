using GamingStore.Models.Publishers;
using GamingStore.Services.Publishers;
using GamingStore.Tests.Mocks;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class PublisherServiceTests
    {
        [Fact]
        public async Task RegisterShouldAddThePublisherProperly()
        {
            var data = DatabaseMock.Instance;

            var publisherService = new PublisherService(data, MapperMock.Instance);

            await publisherService.Register(new RegisterPublisherFormModel() { Name = "", Description = ""});

            Assert.Single(data.Publishers);
        }
    }
}
