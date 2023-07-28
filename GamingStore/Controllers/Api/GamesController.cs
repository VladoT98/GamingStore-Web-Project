    using AutoMapper;
using GamingStore.Data;
using GamingStore.Services.Games.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;

        public GamesController(GamingStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GameServiceModel>> GetAll()
        {
            var games = await this.data.Games.ToListAsync();
            return mapper.Map<List<GameServiceModel>>(games);
        }

        [HttpGet("{title}")]
        public async Task<GameServiceModel> GetByTitle(string title)
        {
            var games = await this.data.Games
                .FirstOrDefaultAsync(x => x.Title == title);

            return mapper.Map<GameServiceModel>(games);
        }
    }
}
