using AutoMapper;
using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Models.Reviews;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;

        public ReviewService(GamingStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task Add(ReviewFormModel reviewModel, string userId)
        {
            var review = this.mapper.Map<GameReview>(reviewModel);

            var user = await this.data.Users.FindAsync(userId);
            var username = user.Email.Substring(0, user.Email.IndexOf('@'));

            review.From = username;
            review.UserId = userId;

            if (string.IsNullOrEmpty(review.ImageUrl))
            {
                review.ImageUrl = "https://bit.ly/3U7uG1t";
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

            reviewToEdit.Content = reviewFormModel.Content;
            reviewToEdit.ImageUrl = reviewFormModel.ImageUrl ?? "https://bit.ly/3U7uG1t";

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsGameIdValid(int gameId)
            => await this.data.Games.AnyAsync(x => x.Id == gameId);

        public async Task<GameReview> FindById(int reviewId)
            => await this.data.GameReviews.FindAsync(reviewId);

        public async Task<bool> IsAllowedToReview(string userId, int gameId)
         => !await this.data.GameReviews.AnyAsync(x => x.UserId == userId && x.GameId == gameId);
    }
}