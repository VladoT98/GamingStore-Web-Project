using AutoMapper;
using GamingStore.Data.Models;
using GamingStore.Infrastructure;
using GamingStore.Models.Games;

namespace GamingStore.Tests.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfig = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                    config.CreateMap<Game, GameFormModel>();
                });

                return new Mapper(mapperConfig);
            }
        }
    }
}