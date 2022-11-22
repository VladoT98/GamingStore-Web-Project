using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamingStore.Data;
using GamingStore.Infrastructure.Enums;
using GamingStore.Services.Games;
using GamingStore.Services.Reviews.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public IEnumerable<ReviewServiceModel> AdminReviews(string username, string game, ReviewSorting sorting, int currentPage, int reviewsPerPage)
        {
            var reviewsQuery = data.GameReviews.AsQueryable();

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

            var result = reviewsQuery
                .Skip((currentPage - 1) * reviewsPerPage)
                .Take(reviewsPerPage)
                .ProjectTo<ReviewServiceModel>(mapper.ConfigurationProvider)
                .ToList();

            return result;
        }

        public async Task<bool> ApproveGame(int id)
        {
            var game = await data.Games.FindAsync(id);

            if (game == null) return false;

            game.IsApproved = true;

            await data.SaveChangesAsync();

            return true;
        }
    }
}
