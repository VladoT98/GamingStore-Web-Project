using GamingStore.Data;
using GamingStore.Data.Models;
using AutoMapper;
using GamingStore.Models.Sellers;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Services.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;

        public SellerService(GamingStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task Become(BecomeSellerFormModel sellerModel, string userId)
        {
            var seller = this.mapper.Map<Seller>(sellerModel);
            seller.UserId = userId;

            this.data.Sellers.Add(seller);
            await this.data.SaveChangesAsync();
        }

        public async Task<bool> IsUserSeller(string userId)
            => await this.data.Sellers
                .AnyAsync(x => x.UserId == userId);

        public async Task<int> GetSellerId(string userId)
            => await this.data.Sellers
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
    }
}
