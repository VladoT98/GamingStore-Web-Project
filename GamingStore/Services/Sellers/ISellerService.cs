using GamingStore.Models.Sellers;

namespace GamingStore.Services.Sellers
{
    public interface ISellerService
    {
        Task Become(BecomeSellerFormModel sellerModel, string userId);

        Task<bool> IsUserSeller(string userId);

        Task<int> GetSellerId(string userId);
    }
}
