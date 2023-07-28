using GamingStore.Data;
using GamingStore.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace GamingStore.Infrastructure
{
    public class DataSeeder : IDataSeeder
    {
        public void SeedPlatforms(GamingStoreDbContext data)
        {
            if (data.Platforms.Any()) return;

            data.Platforms.AddRange(new[]
            {
                new Platform() {Name = "PC"},
                new Platform() {Name = "Nintendo"},
                new Platform() {Name = "Play Station"},
                new Platform() {Name = "Xbox"},
                new Platform() {Name = "Mobile"},
            });

            data.SaveChanges();
        }

        public void SeedPublishers(GamingStoreDbContext data)
        {
            if (data.Publishers.Any()) return;

            data.Publishers.AddRange(new[]
            {
                new Publisher()
                {
                    Name = "Activision",
                    Description = "Activision Publishing, Inc. is an American video game publisher based in Santa Monica, California.",
                    Employees = 4320,
                    Games = data.Games.Where(x => x.Publisher.Name == "Activision").ToList()
                },
                new Publisher()
                {
                    Name = "2K Games",
                    Description = "2K is an American video game publisher owned by Take-Two Interactive and based in Novato, California.",
                    Employees = 3150,
                    Games = data.Games.Where(x => x.Publisher.Name == "2K Games").ToList()
                },
                new Publisher()
                {
                    Name = "Electronic Arts",
                    Description = "Good game publisher.",
                    Employees = 3150,
                    Games = data.Games.Where(x => x.Publisher.Name == "Electronic Arts").ToList()
                },
                new Publisher()
                {
                    Name = "Ubisoft",
                    Description = "Very well know game publisher in the gaming space.",
                    Employees = 1890,
                    Games = data.Games.Where(x => x.Publisher.Name == "Ubisoft").ToList()
                },
                new Publisher()
                {
                    Name = "Hi-Rez",
                    Description = "Has one of the greatest games ever made: SMITE!",
                    Employees = 2565,
                    Games = data.Games.Where(x => x.Publisher.Name == "Hi-Rez").ToList()
                },
                new Publisher()
                {
                    Name = "Sony Interactive",
                    Description = "Sony Interactive Entertainment (SIE), formerly known as Sony Computer Entertainment (SCE), is a multinational video game and digital entertainment company",
                    Employees = 8787,
                    Games = data.Games.Where(x => x.Publisher.Name == "Sony Interactive").ToList()
                },
                new Publisher()
                {
                    Name = "Xbox Studios",
                    Description = "The studios under Bethesda Softworks, focus on delivering great games for everyone, wherever they play – on console, PC, or mobile devices.",
                    Employees = 3564,
                    Games = data.Games.Where(x => x.Publisher.Name == "Xbox Studios").ToList()
                },
                new Publisher()
                {
                    Name = "2K Games",
                    Description = "2K is an American video game publisher based in Novato, California. 2K was founded under Take-Two Interactive in January 2005 through the 2K Games and 2K Sports labels, following Take-Two Interactive's acquisition of Visual Concepts that same month.",
                    Employees = 2980,
                    Games = data.Games.Where(x => x.Publisher.Name == "2K Games").ToList()
                },
                new Publisher()
                {
                    Name = "Plarium",
                    Description = "Plarium is an Israeli international mobile, social and web-based game developer and publisher, known for massively multiplayer online games",
                    Employees = 980,
                    Games = data.Games.Where(x => x.Publisher.Name == "Plarium").ToList()
                },
                new Publisher()
                {
                    Name = "Blizzard",
                    Description = "Blizzard Entertainment, Inc. is an American video game developer and publisher based in Irvine, California.",
                    Employees = 4560,
                    Games = data.Games.Where(x => x.Publisher.Name == "Blizzard").ToList()
                },
            });

            data.SaveChanges();
        }

        public void SeedGenres(GamingStoreDbContext data)
        {
            if (data.Genres.Any()) return;

            data.Genres.AddRange(new[]
            {
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Looter-Shooter" },
                new Genre() { Name = "MMORPG" },
                new Genre() { Name = "MOBA" },
                new Genre() { Name = "FPS" },
                new Genre() { Name = "Sports" },
                new Genre() { Name = "Adventure" },
            });

            data.SaveChanges();
        }

        public void SeedUser(GamingStoreDbContext data)
        {
            if (data.Users.Any()) return;

            var user = new IdentityUser()
            {
                UserName = "Vladislav Tonchev",
                Email = "vladot@gmail.com"
            };

            data.Users.Add(user);

            data.SaveChanges();
        }

        public void SeedSellers(GamingStoreDbContext data)
        {
            if (data.Sellers.Any()) return;

            var seller = new Seller()
            {
                Name = "Vladislav Tonchev",
                PhoneNumber = "+359 98 777 1234",
                UserId = data.Users.First().Id
            };

            data.Sellers.Add(seller);

            data.SaveChanges();
        }

        public void SeedGames(GamingStoreDbContext data)
        {
            if (data.Games.Any()) return;

            data.Games.AddRangeAsync(new[]
                {
                new Game()
                {
                    Title = "Far Cry 6",
                    GenreId = 2,
                    PlatformId = 1,
                    PublisherId = 3,
                    SellerId = 1,
                    Description = "Far Cry® 6 thrusts players into the adrenaline-filled world of a modern-day guerrilla revolution. As dictator of Yara, Antón Castillo is intent on restoring his nation back to its former glory by any means, with his son, Diego, dutifully at his side. Become a guerrilla fighter and burn their regime to the ground.",
                    Price = 19.99m,
                    IsCrossPlay = false,
                    ImageUrl = "https://www.xboxtavern.com/wp-content/uploads/2021/10/far-cry-6-main.jpg",
                    ReleaseYear = 2015,
                    TrailerUrl = "https://www.youtube.com/embed/-IJuKT1mHO8",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "Fifa 2023",
                    GenreId = 6,
                    PlatformId = 1,
                    PublisherId = 4,
                    SellerId = 1,
                    Description = "FIFA 23 features the men's World Cup game mode and the women's World Cup game mode, replicating the 2022 FIFA World Cup and the 2023 FIFA Women's World Cup. The 2022 FIFA World Cup mode was released on 9 November for all platforms except for the Nintendo Switch Legacy Edition.",
                    Price = 9.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images2.alphacoders.com/127/1276415.jpg",
                    ReleaseYear = 2023,
                    TrailerUrl = "https://www.youtube.com/embed/o3V-GvvzjE4",
                    IsApproved = true
                },
                new Game()
                {
                Title = "Call of Duty: Warzone 2.0",
                GenreId = 5,
                PlatformId = 1,
                PublisherId = 5,
                SellerId = 1,
                Description = "Similar to its predecessor, in Warzone 2.0's primary game mode, Battle Royale, players compete in a continuously shrinking map to be the last player(s) remaining. Players parachute onto a large game map, where they encounter and eliminate other players.",
                Price = 49.99m,
                IsCrossPlay = true,
                ImageUrl = "https://editors.dexerto.com/wp-content/uploads/2022/11/04/Warzone-2.jpg",
                ReleaseYear = 2022,
                TrailerUrl = "https://www.youtube.com/embed/oJca6zoI50E",
                IsApproved = true
                },
                new Game()
                {
                    Title = "Smite",
                    GenreId = 4,
                    PlatformId = 1,
                    PublisherId = 2,
                    SellerId = 1,
                    Description = "SMITE is a free-to-play online game developed by Titan Forge Games and published Hi-Rez Studios. It features a large pool of playable characters from ancient mythology in session-based team combat.",
                    Price = 10.95m,
                    IsCrossPlay = true,
                    ImageUrl = "https://wallpaperaccess.com/full/1505556.jpg",
                    ReleaseYear = 2014,
                    TrailerUrl = "https://www.youtube.com/embed/khsjzariacQ",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "Titanfall 2",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 4,
                    SellerId = 1,
                    Description = "Similar to its predecessor, Titanfall 2 is a first-person shooter where players can control both a pilot and their Titans—mecha-style robots that stand roughly seven to ten meters tall. The pilot has a large variety of equipment that enhance their abilities during combat.",
                    Price = 9.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images3.alphacoders.com/751/thumb-1920-751195.jpg",
                    ReleaseYear = 2015,
                    TrailerUrl = "https://www.youtube.com/embed/ktw2k3m7Qko",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "God Of War Ragnarök",
                    GenreId = 7,
                    PlatformId = 3,
                    PublisherId = 1,
                    SellerId = 1,
                    Description = "Kratos and Atreus must journey to each of the Nine Realms in search of answers as Asgardian forces prepare for a prophesied battle that will end the world. Along the way they will explore stunning, mythical landscapes, and face fearsome enemies in the form of Norse gods and monsters.",
                    Price = 79.95m,
                    IsCrossPlay = false,
                    ImageUrl = "https://www.denofgeek.com/wp-content/uploads/2022/11/God-of-War-Ragnarok-Sequel.jpg?fit=1920%2C1052",
                    ReleaseYear = 2022,
                    TrailerUrl = "https://www.youtube.com/embed/EE-4GvjKcfs",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "RAID: Shadow Legends",
                    GenreId = 7,
                    PlatformId = 5,
                    PublisherId = 8,
                    SellerId = 1,
                    Description = "Raid: Shadow Legends is a 3D mobile turn-based RPG set in a fantasy world populated by 16 different factions, each with distinct traits and 15 character classes. Collect heroes to defeat the Dark Lord and restore peace and harmony to the realm of Teleria. Pros: +Variety of collectible heroes.",
                    Price = 9.99m,
                    IsCrossPlay = false,
                    ImageUrl = "https://images6.alphacoders.com/113/thumb-1920-1137219.jpg",
                    ReleaseYear = 2018,
                    TrailerUrl = "https://www.youtube.com/embed/gmwbHdJLadE",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "Overwatch",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 9,
                    SellerId = 1,
                    Description = "Released in 2016 by Blizzard Entertainment, Overwatch is a first-person multiplayer shooter, set in a future where a conflict between robots and humanity necessitated the creation of a task force, conveniently called Overwatch.",
                    Price = 19.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images.hdqwalls.com/wallpapers/overwatch-game-all-heroes.jpg",
                    ReleaseYear = 2016,
                    TrailerUrl = "https://www.youtube.com/embed/FqnKB22pOC0",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "Quantum Break",
                    GenreId = 7,
                    PlatformId = 4,
                    PublisherId = 6,
                    SellerId = 1,
                    Description = "Quantum Break is an action-adventure video game played from a third-person perspective. Players play as Jack Joyce, who has time manipulation powers in a world where time stutters, making everything freeze except Joyce.",
                    Price = 36.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://wallpaperaccess.com/full/1863993.jpg",
                    ReleaseYear = 2016,
                    TrailerUrl = "https://www.youtube.com/embed/ruY1eT9bXiw",
                    IsApproved = true
                },
                new Game()
                {
                    Title = "Borderlands 3",
                    GenreId = 5,
                    PlatformId = 1,
                    PublisherId = 1,
                    SellerId = 1,
                    Description =
                        "The plot is centered on four new Vault Hunters recruited by the Crimson Raiders of Pandora to stop twins Troy and Tyreen Calypso and their insane cult followers from harnessing the power of the alien Vaults spread across the galaxy.",
                    Price = 36.99m,
                    IsCrossPlay = true,
                    ImageUrl = "https://images3.alphacoders.com/100/1004475.jpg",
                    ReleaseYear = 2019,
                    TrailerUrl = "https://www.youtube.com/embed/gjLQ2Uj4OPw",
                    IsApproved = true
                },
                }
            );

            data.SaveChanges();
        }

        public void SeedReviews(GamingStoreDbContext data)
        {
            if (data.GameReviews.Any()) return;

            var userId = data.Users.First().Id;

            data.GameReviews.AddRange(new[]
            {
                new GameReview()
                {
                    From = "Guy Gilbert",
                    ImageUrl =
                        "https://www.clipartmax.com/png/middle/171-1717870_stockvader-predicted-cron-for-may-user-profile-icon-png.png",
                    GameId = 1,
                    UserId = userId,
                    Content = "Great game"
                },
                new GameReview()
                {
                    From = "Robert White",
                    ImageUrl =
                        "https://www.clipartmax.com/png/middle/171-1717870_stockvader-predicted-cron-for-may-user-profile-icon-png.png",
                    GameId = 2,
                    UserId = userId,
                    Content = "The gameplay is so cool."
                },
                new GameReview()
                {
                    From = "Jessica Tucker",
                    ImageUrl =
                        "https://www.clipartmax.com/png/middle/171-1717870_stockvader-predicted-cron-for-may-user-profile-icon-png.png",
                    GameId = 3,
                    UserId = userId,
                    Content = "Incredible game!"
                },
                new GameReview()
                {
                    From = "Peter Ivanov",
                    ImageUrl =
                        "https://www.clipartmax.com/png/middle/171-1717870_stockvader-predicted-cron-for-may-user-profile-icon-png.png",
                    GameId = 4,
                    UserId = userId,
                    Content = "Amazing game!"
                },
            });

            data.SaveChanges();
        }

        public void SeedBlogPosts(GamingStoreDbContext data)
        {
            if (data.Blogs.Any()) return;

            data.Blogs.AddRange(new[]
            {
                new Blog()
                {
                    Title = "1080p vs. 1440p Gaming: The Three Factors to Consider",
                    Content = "1. Pixel Density (PPI)\nPixel density is the number of pixels per inch of the display. It describes how many pixels you can see on every inch of the display. The higher this number, the sharper the image you will see. For instance, if we calculate the PPI of a 24-inch FHD (1920x1080) monitor, it comes out to 92.56 PPI. If we keep the monitor size the same but increase the resolution to 2K (2560x1440), the result is 123.41 PPI.\n2. Gaming Performance\nThe 1080p vs. 1440p gaming debate can never have a satisfactory answer if there is no talk about the impact of these resolutions on the performance. You probably don’t need us to tell you that the higher the resolution, the higher the performance cost. So, 1440p will result in a significant performance impact, and the games on your 1440p display won’t run as smoothly as on a 1080p panel.\n3. Display Cost\nThe next factor you need to consider is the cost of the display. Unsurprisingly, 1440p displays are more expensive than 1080p displays, if we keep all the factors like refresh rate, panel technology, and response time the same.\r\n\r\nIf we add higher refresh rates like 144Hz to the mix, the cost increases. The same goes for panel technology. OLED panels are the priciest, followed by IPS and VA panels, while TN panels are the most budget-friendly.\r\n\r\nHowever, if money is not a concern, look towards monitors with high refresh rate, low latency, OLED or IPS panels. But if money is tight, a decent 60Hz 1440p TN will be a lot better than a 1080p 60Hz TN panel.\r\n\r\nThat said, for competitive gaming, it makes sense to get the fastest panel you can afford regardless of the resolution. For instance, a 1080p 240Hz display will give you a competitive edge over folks with 1440p 60Hz monitors.",
                    ImageUrl = "https://i.ytimg.com/vi/i1DjDMt7QZA/maxresdefault.jpg"
                },
                new Blog()
                {
                    Title = "IMPROVING YOUR AIM IN CALL OF DUTY: WARZONE",
                    Content = "Are you ready to be the one who gets that perfect headshot in Warzone? If you're having trouble getting those perfect shots, maybe it's time to take a look at what kind of thumbsticks you play with. \r\n\r\nThe number one thing to improve your aim is having the right thumbsticks. Everyone has different hand sizes, thumb lengths, and preferences. That’s why SCUF thumbsticks are interchangeable: to match your unique preference of comfort and playstyle. In this guide, we'll be going over how to improve your aim with the right sticks. \r\n\r\n \r\n\r\nIdentify your Playstyle\r\n\r\nAre you someone who likes to plan out a strategy and take their time to take out the enemy? Or do you prefer to take a more direct aggressive approach? Do you like shotguns or sniper rifles more? Whatever your preferred style, it’s a key part in what thumbsticks will suit you best. \r\n\r\n \r\n\r\nUnderstanding Thumbstick Shape and Length\r\n\r\nThere are two parts to the thumbstick: its shape and its length. \r\n\r\nConcave thumbsticks are designed for more movement control. \r\n\r\n\r\nDomed thumbsticks are designed for more accuracy. \r\n\r\nShort thumbsticks are designed for quicker movement speed\r\n\r\nTall thumbsticks are designed to have more angle to play with. \r\n\r\n \r\n\r\nChoosing Thumbsticks\r\n\r\nFor aggressive players, like SMG players, we recommend a short concave thumbstick on the left and a short domed thumbstick on the right. You’ll be quick to move and ready to quickly aim.\r\n\r\nFor defensive players, like sniper rifle players, we recommend a short concave thumbstick on the left and a tall domed thumbstick on the right. You can still move quickly, but also be the most accurate when aiming. \r\n\r\nTry out different thumbstick types and combinations to find the ones that fit your playstyle, or change it up when you’re in the mood for something different. Whether it is that critical moment in a 1v1 in the Gulag to get back in the game, or just trying to dominate on the field, the right thumbsticks will make sure your aim is supreme. \r\n\r\n\r\n\r\n \r\n\r\nCombining with In-Game Settings\r\n\r\n\r\nJust as an additional boost to help with your aim, we have a couple of adjustments to make that will improve your aim in combination with the sticks.\r\n\r\n\r\n\r\nWe recommend changing your ADS Sensitivity Multiplier for more control when aiming quickly during a fight. \r\n\r\n\r\nStart by going into Options > Controller > ADS Sensitivity Multiplier (Low Zoom) > Set to 0.88\r\nNext do the same for ADS Sensitivity Multiplier (High Zoom)\r\nWe also recommend turning off Controller Vibration if you have vibration modules in your controller.\r\n\r\nOptions > Controller > Controller Vibration > Disabled\r\nCheck out our other Call of Duty: Warzone Guides\r\n\r\nGetting Started in Call of Duty: Warzone\r\n\r\nExpert Controller Settings in Call of Duty: Warzone",
                    ImageUrl = "https://i.ytimg.com/vi/h-584lxeJ6Q/maxresdefault.jpg"
                }
            });

            data.SaveChanges();
        }

        public void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator")) return;

                    var role = new IdentityRole { Name = "Administrator" };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@gmail.com";
                    const string adminPassword = "admin123";

                    var user = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        PhoneNumber = "+359 89 561 9321"
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, "Administrator");
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}