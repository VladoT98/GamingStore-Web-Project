using AutoMapper;
using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Models.Publishers;

namespace GamingStore.Services.Publishers
{
    public class PublisherService : IPublisherService
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;

        public PublisherService(GamingStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task Register(RegisterPublisherFormModel model)
        {
            var publisher = this.mapper.Map<Publisher>(model);
            await this.data.Publishers.AddAsync(publisher);
            await this.data.SaveChangesAsync();
        }
    }
}