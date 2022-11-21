using GamingStore.Models.Publishers;

namespace GamingStore.Services.Publishers
{
    public interface IPublisherService
    {
        Task Register(RegisterPublisherFormModel model);
    }
}
