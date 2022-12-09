using GamingStore.Areas.Admin.Models;
using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Reviews.Models;

namespace GamingStore.Areas.Admin.Services.Admin
{
    public interface IAdminService
    {
        Task<IEnumerable<ReviewServiceModel>> GetReviewsInfo(string username, string game, ReviewSorting sorting, int currentPage, int reviewsPerPage);

        Task<IEnumerable<UserViewModel>> GetUsersInfo(string email, string phoneNumber, UserSorting sorting,
            int currentPage, int usersPerPage);

        Task<int> UsersCount(string email, string phoneNumber, UserSorting sorting);

        Task<int> ReviewsCount(string gameTitle, string username);

        Task<bool> ApproveGame(int id);

        Task<bool> DeleteUser(string userId);
    }
}