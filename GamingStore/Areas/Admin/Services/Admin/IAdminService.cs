using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Reviews.Models;

namespace GamingStore.Areas.Admin.Services.Admin
{
    public interface IAdminService
    {
        IEnumerable<ReviewServiceModel> AdminReviews(string username, string game, ReviewSorting sorting, int currentPage, int reviewsPerPage);

        Task<bool> ApproveGame(int id);
    }
}
