using GamingStore.Data.Models;
using GamingStore.Models.Reviews;

namespace GamingStore.Services.Reviews
{
    public interface IReviewService
    {
        Task Add(ReviewFormModel reviewModel, string userId);

        Task Delete(GameReview review);

        Task<bool> Edit(ReviewFormModel reviewFormModel, int id);

        Task<bool> IsGameIdValid(int gameId);

        Task<GameReview> FindById(int reviewId);

        Task<int> ReviewsCount(string gameTitle, string username);

        Task<bool> IsAllowedToReview(string userId, int gameId);
    }
}