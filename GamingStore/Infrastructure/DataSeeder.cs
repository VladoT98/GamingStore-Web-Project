using GamingStore.Data;
using GamingStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Infrastructure
{
    public class DataSeeder : IDataSeeder
    {
        public async Task SeedPlatforms(GamingStoreDbContext data)
        {
            if (await data.Platforms.AnyAsync()) return;

            await data.Platforms.AddRangeAsync(new[]
            {
                new Platform() {Name = "PC"},
                new Platform() {Name = "Nintendo"},
                new Platform() {Name = "Play Station"},
                new Platform() {Name = "Xbox"},
                new Platform() {Name = "Mobile"},
            });

            await data.SaveChangesAsync();
        }

        public async Task SeedPublishers(GamingStoreDbContext data)
        {
            if (await data.Publishers.AnyAsync()) return;

            await data.Publishers.AddRangeAsync(new[]
             {
                new Publisher()
                {
                    Name = "Activision",
                    Description = "Activision is one of the most iconic game publishers of all time.",
                    Employees = 4320,
                    Games = data.Games.Where(x => x.Publisher.Name == "Activision")
                },
                new Publisher()
                {
                    Name = "Electronic Arts",
                    Description = "Good game publisher.",
                    Employees = 3150,
                    Games = data.Games.Where(x => x.Publisher.Name == "Electronic Arts")
                },
                new Publisher()
                {
                    Name = "Ubisoft",
                    Description = "Very well know game publisher in the gaming space.",
                    Employees = 1890,
                    Games = data.Games.Where(x => x.Publisher.Name == "Ubisoft")
                },
                new Publisher()
                {
                    Name = "Hi-Rez",
                    Description = "Has one of the greatest games ever made: SMITE!",
                    Employees = 2565,
                    Games = data.Games.Where(x => x.Publisher.Name == "Hi-Rez")
                },
                new Publisher()
                {
                    Name = "Sony Interactive",
                    Description = "Sony Interactive Entertainment (SIE), formerly known as Sony Computer Entertainment (SCE), is a multinational video game and digital entertainment company",
                    Employees = 8787,
                    Games = data.Games.Where(x => x.Publisher.Name == "Sony Interactive")
                },
                new Publisher()
                {
                    Name = "Xbox Studios",
                    Description = "The studios under Bethesda Softworks, focus on delivering great games for everyone, wherever they play – on console, PC, or mobile devices.",
                    Employees = 3564,
                    Games = data.Games.Where(x => x.Publisher.Name == "Xbox Studios")
                },
                new Publisher()
                {
                    Name = "2K Games",
                    Description = "2K is an American video game publisher based in Novato, California. 2K was founded under Take-Two Interactive in January 2005 through the 2K Games and 2K Sports labels, following Take-Two Interactive's acquisition of Visual Concepts that same month.",
                    Employees = 2980,
                    Games = data.Games.Where(x => x.Publisher.Name == "2K Games")
                },
                new Publisher()
                {
                    Name = "Plarium",
                    Description = "Plarium is an Israeli international mobile, social and web-based game developer and publisher, known for massively multiplayer online games",
                    Employees = 980,
                    Games = data.Games.Where(x => x.Publisher.Name == "Plarium")
                },
                new Publisher()
                {
                    Name = "Blizzard",
                    Description = "Blizzard Entertainment, Inc. is an American video game developer and publisher based in Irvine, California.",
                    Employees = 4560,
                    Games = data.Games.Where(x => x.Publisher.Name == "Blizzard")
                },
            });

            await data.SaveChangesAsync();
        }

        public async Task SeedGenres(GamingStoreDbContext data)
        {
            if (await data.Genres.AnyAsync()) return;

            await data.Genres.AddRangeAsync(new[]
            {
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Looter-Shooter" },
                new Genre() { Name = "MMORPG" },
                new Genre() { Name = "MOBA" },
                new Genre() { Name = "FPS" },
                new Genre() { Name = "Sports" },
                new Genre() { Name = "Adventure" },
            });

            await data.SaveChangesAsync();
        }

        public async Task SeedGames(GamingStoreDbContext data)
        {
            if (await data.Games.AnyAsync()) return;

            await data.Games.AddRangeAsync(new[]
            {
                new Game()
                {
                    Title = "Call of Duty: Cold War",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 1,
                    SellerId = 3,
                    Description = "Call of Duty: Black Ops Cold War is set during the Cold War in the early 1980s. The campaign follows Green Beret turned CIA SAD/SOG officer Russell Adler (Bruce Thomas) and his mission to stop an international espionage threat named Perseus (William Salyers) in 1981.",
                    Price = 59.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://www.callofduty.com/content/dam/atvi/callofduty/cod-touchui/zeus/common/social-share/zeus-social-share.jpg",
                    ReleaseYear = 2019,
                },
                new Game()
                {
                    Title = "Far Cry 6",
                    GenreId = 2,
                    PlatformId = 1,
                    PublisherId = 3,
                    SellerId = 2,
                    Description = "Far Cry® 6 thrusts players into the adrenaline-filled world of a modern-day guerrilla revolution. As dictator of Yara, Antón Castillo is intent on restoring his nation back to its former glory by any means, with his son, Diego, dutifully at his side. Become a guerrilla fighter and burn their regime to the ground.",
                    Price = 19.99m,
                    IsCrossPlay = false,
                    ImageUrl = "https://www.xboxtavern.com/wp-content/uploads/2021/10/far-cry-6-main.jpg",
                    ReleaseYear = 2015
                },
                new Game()
                {
                    Title = "Fifa 2022",
                    GenreId = 6,
                    PlatformId = 1,
                    PublisherId = 4,
                    SellerId = 3,
                    Description = "FIFA 22 introduces HyperMotion Technology, which uses motion capture data collected from 22 real-life players playing a complete, high-intensity football match in motion capture suits. The data collected from player movements, tackles, aerial duels and on-ball actions is used to power FIFA 22 gameplay.",
                    Price = 9.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://digistatement.com/wp-content/uploads/2021/12/FIFA-22.jpg",
                    ReleaseYear = 2022
                },
                new Game()
                {
                Title = "Call of Duty: Warzone",
                GenreId = 5,
                PlatformId = 1,
                PublisherId = 5,
                SellerId = 2,
                Description = "Warzone allows online multiplayer combat among 150 players, although some limited-time game modes support 200 players. The game features both cross-platform play and cross-platform progression between the three aforementioned titles. At launch, the game features two main modes: Battle Royale and Plunder.",
                Price = 49.99m,
                IsCrossPlay = true,
                ImageUrl = "https://wallpaperaccess.com/full/2314739.jpg",
                ReleaseYear = 2019
                },
                new Game()
                {
                    Title = "Smite",
                    GenreId = 4,
                    PlatformId = 1,
                    PublisherId = 2,
                    SellerId = 3,
                    Description = "SMITE is a free-to-play online game developed by Titan Forge Games and published Hi-Rez Studios. It features a large pool of playable characters from ancient mythology in session-based team combat.",
                    Price = 10.95m,
                    IsCrossPlay = true,
                    ImageUrl = "https://wallpaperaccess.com/full/1505556.jpg",
                    ReleaseYear = 2014
                },
                new Game()
                {
                    Title = "Titanfall 2",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 4,
                    SellerId = 3,
                    Description = "Similar to its predecessor, Titanfall 2 is a first-person shooter where players can control both a pilot and their Titans—mecha-style robots that stand roughly seven to ten meters tall. The pilot has a large variety of equipment that enhance their abilities during combat.",
                    Price = 9.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images3.alphacoders.com/751/thumb-1920-751195.jpg",
                    ReleaseYear = 2015
                },
                new Game()
                {
                    Title = "God of War",
                    GenreId = 7,
                    PlatformId = 3,
                    PublisherId = 1,
                    SellerId = 2,
                    Description = "Kratos Goes Full Dad in New God of War PS4 trailer\r\nUnlike previous installments, this game focuses on Norse mythology and follows an older and more seasoned Kratos and his new son Atreus in the years since God of War III. The game released on April 20, 2018, and is currently exclusive to the PlayStation 4.",
                    Price = 79.95m,
                    IsCrossPlay = false,
                    ImageUrl = "https://wallpaper.dog/large/20474279.png",
                    ReleaseYear = 2018
                },
                new Game()
                {
                    Title = "RAID: Shadow Legends",
                    GenreId = 7,
                    PlatformId = 5,
                    PublisherId = 8,
                    SellerId = 2,
                    Description = "Raid: Shadow Legends is a 3D mobile turn-based RPG set in a fantasy world populated by 16 different factions, each with distinct traits and 15 character classes. Collect heroes to defeat the Dark Lord and restore peace and harmony to the realm of Teleria. Pros: +Variety of collectible heroes.",
                    Price = 9.99m,
                    IsCrossPlay = false,
                    ImageUrl = "https://images6.alphacoders.com/113/thumb-1920-1137219.jpg",
                    ReleaseYear = 2018
                },
                new Game()
                {
                    Title = "Overwatch",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 9,
                    SellerId = 2,
                    Description = "Released in 2016 by Blizzard Entertainment, Overwatch is a first-person multiplayer shooter, set in a future where a conflict between robots and humanity necessitated the creation of a task force, conveniently called Overwatch.",
                    Price = 19.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images.hdqwalls.com/wallpapers/overwatch-game-all-heroes.jpg",
                    ReleaseYear = 2016
                },
                new Game()
                {
                    Title = "Quantum Break",
                    GenreId = 7,
                    PlatformId = 4,
                    PublisherId = 6,
                    SellerId = 2,
                    Description = "Quantum Break is an action-adventure video game played from a third-person perspective. Players play as Jack Joyce, who has time manipulation powers in a world where time stutters, making everything freeze except Joyce.",
                    Price = 36.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://wallpaperaccess.com/full/1863993.jpg",
                    ReleaseYear = 2016
                },
                new Game()
                {
                    Title = "Borderlands 3",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 7,
                    SellerId = 2,
                    Description = "The plot is centered on four new Vault Hunters recruited by the Crimson Raiders of Pandora to stop twins Troy and Tyreen Calypso and their insane cult followers from harnessing the power of the alien Vaults spread across the galaxy.",
                    Price = 36.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images3.alphacoders.com/100/1004475.jpg",
                    ReleaseYear = 2019
                },
            });

            await data.SaveChangesAsync();
        }

        public async Task SeedSellers(GamingStoreDbContext data)
        {
            if (await data.Sellers.AnyAsync()) return;

            await data.Sellers.AddRangeAsync(new[]
            {
                new Seller()
                {
                    Name = "Vladislav Tonchev",
                    PhoneNumber = "+359 98 777 1234",
                    UserId = "b732c0f4-6a18-49a2-91d5-e1e1cd12fa6d"
                },
                new Seller()
                {
                    Name = "Nikolay Kostov",
                    PhoneNumber = "+359 22 111 5432",
                    UserId = "e7a7b4fc-243e-468f-8408-f9c60586ccdd"
                },
            });

            await data.SaveChangesAsync();
        }

        public async Task SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.RoleExistsAsync("Administrator")) return;

            var role = new IdentityRole { Name = "Administrator" };

            await roleManager.CreateAsync(role);

            var user = new IdentityUser()
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                PhoneNumber = "+359 89 561 9321",
            };

            await userManager.CreateAsync(user, "admin123");

            await userManager.AddToRoleAsync(user, "Administrator");
        }

        public async Task SeedReviews(GamingStoreDbContext data)
        {
            if (await data.GameReviews.AnyAsync()) return;

            await data.GameReviews.AddRangeAsync(new[]
            {
                new GameReview()
                {
                    From = "Guy Gilbert",
                    ImageUrl = "https://www.clipartmax.com/png/middle/171-1717870_stockvader-predicted-cron-for-may-user-profile-icon-png.png",
                    GameId = 2,
                    UserId = "13caee2f-ba88-4eb9-a2ac-2444f9bbbef7",
                    Content = "I’m breaking down each section multiplayer, zombies, and campaign (No spoilers) Campaign: ‑honestly it’s great, the feeling of going on Cold War black ops missions where the game truly immersed you"
                },
                new GameReview()
                {
                    From = "Ivan Ivanov",
                    ImageUrl = "https://www.dlf.pt/dfpng/middlepng/569-5693658_dummy-user-image-png-transparent-png.png",
                    GameId = 4,
                    UserId = "cd583ca1-f1ed-4f4b-ac57-7ef8bd05092d",
                    Content = "I honestly think this is the best fifa for about 3/4 years. The gameplay is slightly slower than last year and player movements aren't as erratic giving the game a little bit more of a realistic feel. Goalkeepers are actually making saves that they should be and making a string of saves in a row."
                }
            });

            await data.SaveChangesAsync();
        }
    }
}