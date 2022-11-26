using AutoMapper;
using GamingStore.Areas.Admin.Models;
using GamingStore.Data.Models;
using GamingStore.Models.Games;
using GamingStore.Models.Publishers;
using GamingStore.Models.Reviews;
using GamingStore.Models.Sellers;
using GamingStore.Services.Games.Models;
using GamingStore.Services.Reviews.Models;

namespace GamingStore.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameBaseModel>();
            CreateMap<Game, GameServiceModel>();
            CreateMap<Game, GameDetailsServiceModel>()
                .ForMember(x => x.IsCrossPlay, cfg =>
                    cfg.MapFrom(x => x.IsCrossPlay ? "Yes" : "No"))
                .ForMember(x => x.UserId, cfg =>
                    cfg.MapFrom(x => x.Seller.UserId));

            CreateMap<GameDetailsServiceModel, GameFormModel>();

            CreateMap<GameFormModel, Game>()
                .ForMember(x => x.IsCrossPlay, cfg =>
                    cfg.MapFrom(x => x.IsCrossPlay == "Yes"));

            CreateMap<BecomeSellerFormModel, Seller>();

            CreateMap<ReviewFormModel, GameReview>().ReverseMap();
            CreateMap<GameReview, ReviewServiceModel>()
                .ForMember(x => x.Game, cfg =>
                    cfg.MapFrom(x => x.Game.Title));

            CreateMap<Platform, GamePlatformServiceModel>();
            CreateMap<Genre, GameGenreServiceModel>();
            CreateMap<RegisterPublisherFormModel, Publisher>();
            CreateMap<BlogFormModel, Blog>().ReverseMap();
            CreateMap<Blog, BlogViewModel>();
        }
    }
}