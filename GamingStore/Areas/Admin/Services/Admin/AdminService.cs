using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamingStore.Areas.Admin.Models;
using GamingStore.Data;
using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Games;
using GamingStore.Services.Reviews.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Areas.Admin.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;

        public AdminService(GamingStoreDbContext data, IMapper mapper, IGameService gameService)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReviewServiceModel>> GetReviewsInfo(string username, string game, ReviewSorting sorting, int currentPage, int reviewsPerPage)
        {
            var reviewsQuery = this.data.GameReviews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(username))
                reviewsQuery = reviewsQuery
                    .Where(x => x.From.ToLower().Contains(username.ToLower()));

            if (!string.IsNullOrWhiteSpace(game))
                reviewsQuery = reviewsQuery
                    .Where(x => x.Game.Title.ToLower().Contains(game.ToLower()));

            reviewsQuery = sorting switch
            {
                ReviewSorting.RecentlyAdded => reviewsQuery.OrderByDescending(x => x.Id),
                ReviewSorting.Oldest => reviewsQuery.OrderBy(x => x.Id),
                _ => reviewsQuery.OrderByDescending(x => x.Id)
            };

            var reviews = await reviewsQuery
                .Skip((currentPage - 1) * reviewsPerPage)
                .Take(reviewsPerPage)
                .ProjectTo<ReviewServiceModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersInfo(string email, string phoneNumber, UserSorting sorting, int currentPage, int reviewsPerPage)
        {
            var usersQuery = this.data.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
                usersQuery = usersQuery
                    .Where(x => x.Email.ToLower().Contains(email.ToLower()));

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                usersQuery = usersQuery
                    .Where(x => x.PhoneNumber.ToLower().Contains(phoneNumber.ToLower()));

            usersQuery = sorting switch
            {
                UserSorting.RecentlyRegistered => usersQuery.OrderByDescending(x => x.Id),
                UserSorting.FirstRegistered => usersQuery.OrderBy(x => x.Id),
                UserSorting.EmailAscending => usersQuery.OrderBy(x => x.Email),
                UserSorting.EmailDescending => usersQuery.OrderByDescending(x => x.Email),
                UserSorting.HasPhoneNumber => usersQuery.Where(x => x.PhoneNumber != null),
                _ => usersQuery.OrderByDescending(x => x.Id)
            };

            var users = await usersQuery
                .Skip((currentPage - 1) * reviewsPerPage)
                .Take(reviewsPerPage)
                .ProjectTo<UserViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<int> UsersCount(string email, string phoneNumber, UserSorting sorting)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phoneNumber) && sorting != UserSorting.HasPhoneNumber)
            {
                return await this.data.Users.CountAsync();
            }

            var users = this.data.Users.AsQueryable();

            if (email != null)
                users = users
                    .Where(x => x.Email.ToLower().Contains(email.ToLower()));

            if (phoneNumber != null)
                users = users
                    .Where(x => x.PhoneNumber.ToLower().Contains(phoneNumber.ToLower()));

            if (sorting == UserSorting.HasPhoneNumber)
                users = users
                   .Where(x => x.PhoneNumber != null);

            return await users.CountAsync();
        }

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

        public async Task<bool> ApproveGame(int id)
        {
            var game = await data.Games.FindAsync(id);

            if (game == null) return false;

            game.IsApproved = true;

            await data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await this.data.Users.FindAsync(userId);
            var seller = await this.data.Sellers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null || seller == null) return false;

            this.data.Sellers.Remove(seller);
            this.data.Users.Remove(user);
            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
