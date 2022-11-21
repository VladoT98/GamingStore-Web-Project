namespace GamingStore.Models.Api
{
    public class StatisticsApiModel
    {
        public int GamesCount { get; init; }

        public int GenresCount { get; init; }

        public int PublishersCount { get; init; }

        public int PlatformsCount { get; init; }

        public int BlogsCount { get; init; }

        public int ReviewsCount { get; init; }
    }
}