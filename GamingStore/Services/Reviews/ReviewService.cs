using AutoMapper;
using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Models.Reviews;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper mapper;
        private readonly GamingStoreDbContext data;

        public ReviewService(IMapper mapper, GamingStoreDbContext data)
        {
            this.mapper = mapper;
            this.data = data;
        }

        public async Task Add(ReviewFormModel reviewModel, string userId)
        {
            var review = this.mapper.Map<GameReview>(reviewModel);
            review.UserId = userId;

            if (string.IsNullOrEmpty(review.ImageUrl))
            {
                review.ImageUrl =
                    "https://us.123rf.com/450wm/tuktukdesign/tuktukdesign1608/tuktukdesign160800043/61010830-user-icon-man-profile-businessman-avatar-person-glyph-vector-illustration.jpg?ver=6";
            }

            this.data.GameReviews.Add(review);
            await this.data.SaveChangesAsync();
        }

        public async Task Delete(GameReview review)
        {
            this.data.GameReviews.Remove(review);
            await this.data.SaveChangesAsync();
        }

        public async Task<bool> Edit(ReviewFormModel reviewFormModel, int id)
        {
            var reviewToEdit = await this.data.GameReviews.FindAsync(id);

            if (reviewToEdit == null) return false;

            reviewToEdit.From = reviewFormModel.From;
            reviewToEdit.Content = reviewFormModel.Content;
            reviewToEdit.ImageUrl = reviewFormModel.ImageUrl;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsGameIdValid(int gameId)
            => await this.data.Games.AnyAsync(x => x.Id == gameId);

        public async Task<GameReview> FindById(int reviewId)
            => await this.data.GameReviews.FindAsync(reviewId);

        public async Task<int> ReviewsCount(string gameTitle, string username)
        {
            if (string.IsNullOrEmpty(gameTitle) && string.IsNullOrEmpty(username))
            {
                return await this.data.GameReviews.CountAsync();
            }

            var reviews = this.data.Games.AsQueryable();

            if (gameTitle != null)
                reviews = reviews
                    .Where(x => x.Title.ToLower().Contains(gameTitle.ToLower()));

            if (username != null)
                reviews = reviews
                    .Where(x => x.Publisher.Name.ToLower().Contains(username.ToLower()));

            return await reviews.CountAsync();
        }
    }
}