using GameHaven.Models.Models;
using GameHaven.Utility;
using Microsoft.EntityFrameworkCore;

namespace GameHaven.DataAccess
{
    // Database context for GameHaven Shop
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        // Orders for checkout
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Game config
            modelBuilder.Entity<Game>()
                .Property(g => g.Price)
                .HasColumnType("decimal(18,2)");

            // User config
            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
                b.Property(u => u.LastName).HasMaxLength(100).IsRequired();
                b.Property(u => u.Email).HasMaxLength(256).IsRequired();
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.PasswordHash).HasMaxLength(128).IsRequired(); 
                b.Property(u => u.HomeAddress).HasMaxLength(1000);
                b.Property(u => u.Role).HasConversion<int>().IsRequired();
            });

            // Order & OrderItem config
            modelBuilder.Entity<Order>(b =>
            {
                b.Property(o => o.EmailAddress).HasMaxLength(256).IsRequired();
                b.Property(o => o.CreatedUtc).HasDefaultValueSql("GETUTCDATE()");
                b.HasMany(o => o.Items)
                 .WithOne(i => i.Order)
                 .HasForeignKey(i => i.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(b =>
            {
                b.Property(i => i.ProductName).HasMaxLength(200).IsRequired();
                b.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
                b.Property(i => i.Quantity).IsRequired();
            });

            // Seed admin 
            var admin = new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@gamehaven.com",
                HomeAddress = "GameHaven HQ",
                Role = UserRole.Admin,
                PasswordHash = SimplePassword.Hash("Admin1")
            };
            modelBuilder.Entity<User>().HasData(admin);

            // Seed initial Home Page Best Seller games 
            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Title = "NBA 2K26", Price = 62m, ImageUrl = "/images/homepage/bestsellers/nba2k26.png", Category = "Home Page Best Seller", Slug = "nba-2k26", Description = "Take your basketball career to the next level with NBA 2K26.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "September 2025", Genre = "Sports / Simulation" },
                new Game { Id = 2, Title = "EA FC 26", Price = 69m, ImageUrl = "/images/homepage/bestsellers/eafc26.png", Category = "Home Page Best Seller", Slug = "ea-fc-26", Description = "Experience the ultimate football simulation in EA FC 26.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "September 2025", Genre = "Sports / Simulation" },
                new Game { Id = 3, Title = "WWE 2K25", Price = 25m, ImageUrl = "/images/homepage/bestsellers/wwe2k25.png", Category = "Home Page Best Seller", Slug = "wwe-2k25", Description = "Step into the ring and become a wrestling champion in WWE 2K25.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "March 2025", Genre = "Sports / Action" },
                new Game { Id = 4, Title = "GTA 5", Price = 40m, ImageUrl = "/images/homepage/bestsellers/gta5.png", Category = "Home Page Best Seller", Slug = "gta-5", Description = "Explore Los Santos in this epic open-world crime adventure.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "March 2022", Genre = "Action / Adventure" },
                new Game { Id = 5, Title = "COD Black Ops 6", Price = 39m, ImageUrl = "/images/homepage/bestsellers/codbo6.png", Category = "Home Page Best Seller", Slug = "cod-black-ops-6", Description = "Engage in intense warfare in the latest Call of Duty Black Ops game.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "October 2024", Genre = "Shooter / Action" },
                new Game { Id = 6, Title = "Borderlands 4", Price = 58m, ImageUrl = "/images/homepage/bestsellers/borderlands4.png", Category = "Home Page Best Seller", Slug = "borderlands-4", Description = "Loot, shoot and explore the chaotic world of Borderlands 4.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "September 2025", Genre = "Shooter / RPG" },
                new Game { Id = 7, Title = "Little Nightmares III", Price = 35m, ImageUrl = "/images/homepage/bestsellers/little-nightmares-3.png", Category = "Home Page Best Seller", Slug = "little-nightmares-3", Description = "Venture into dark, twisted worlds in this chilling platformer.", Platform = "PS5, PS4, Switch, PC", ReleaseDate = "October 2025", Genre = "Horror / Platformer" },
                new Game { Id = 8, Title = "Assassin’s Creed Shadows", Price = 40m, ImageUrl = "/images/homepage/bestsellers/assassins-creed-shadows.png", Category = "Home Page Best Seller", Slug = "assassins-creed-shadows", Description = "Embark on a stealthy adventure full of intrigue.", Platform = "PS5, PS4, Xbox, PC", ReleaseDate = "March 2025", Genre = "Action / Stealth" },
                new Game { Id = 9, Title = "Pokémon Legends Z-A", Price = 32m, ImageUrl = "/images/homepage/bestsellers/pokemon-legends-za.png", Category = "Home Page Best Seller", Slug = "pokemon-legends-za", Description = "Catch, train and battle in the latest Pokémon adventure.", Platform = "Switch", ReleaseDate = "October 2025", Genre = "Adventure / RPG" },
                new Game { Id = 10, Title = "Sonic Racing: CrossWorlds", Price = 43m, ImageUrl = "/images/homepage/bestsellers/sonic-racing-crossworlds.png", Category = "Home Page Best Seller", Slug = "sonic-racing-crossworlds", Description = "Race at lightning speed across amazing worlds with Sonic and friends.", Platform = "Switch", ReleaseDate = "September 2025", Genre = "Racing / Arcade" }
            );

            // Seed Nintendo Products
            modelBuilder.Entity<Game>().HasData(
                // Nintendo Accessories
                new Game { Id = 11, Title = "Nintendo Switch Dock Set", Price = 120m, ImageUrl = "/images/nintendo/accessories/switch-dock-set.png", Category = "Nintendo Accessories", Slug = "switch-dock-set", Description = "High-quality dock for Nintendo Switch with extra HDMI support.", Platform = "Nintendo Switch", ReleaseDate = "June 2025", Genre = "Nintendo Accessory" },
                new Game { Id = 12, Title = "Nintendo Switch Carrying Case + Screen Protector", Price = 30m, ImageUrl = "/images/nintendo/accessories/switch-case-screen-protector.png", Category = "Nintendo Accessories", Slug = "switch-case-screen-protector", Description = "Protect your console on the go with this durable case and screen protector.", Platform = "Nintendo Switch", ReleaseDate = "June 2025", Genre = "Nintendo Accessory" },
                new Game { Id = 13, Title = "Nintendo Switch Joy-Con Charging Grip", Price = 13m, ImageUrl = "/images/nintendo/accessories/joycon-charging-grip.png", Category = "Nintendo Accessories", Slug = "joycon-charging-grip", Description = "Charge your Joy-Cons while you play for uninterrupted gaming.", Platform = "Nintendo Switch", ReleaseDate = "October 2024", Genre = "Nintendo Accessory" },
                new Game { Id = 14, Title = "Nintendo Switch Leg Strap", Price = 15m, ImageUrl = "/images/nintendo/accessories/switch-leg-strap.png", Category = "Nintendo Accessories", Slug = "switch-leg-strap", Description = "Comfortable leg strap accessory for Ring Fit Adventure.", Platform = "Nintendo Switch", ReleaseDate = "April 2922", Genre = "Nintendo Accessory" },
                new Game { Id = 15, Title = "Nintendo Switch AC Adapter", Price = 15m, ImageUrl = "/images/nintendo/accessories/switch-ac-adapter.png", Category = "Nintendo Accessories", Slug = "switch-ac-adapter", Description = "Official AC adapter to keep your Switch charged.", Platform = "Nintendo Switch", ReleaseDate = "March 2017", Genre = "Nintendo Accessory" },

                // Nintendo Consoles
                new Game { Id = 16, Title = "Nintendo Switch 2", Price = 380m, ImageUrl = "/images/nintendo/consoles/switch2.png", Category = "Nintendo Console", Slug = "switch-2", Description = "Next-gen Nintendo Switch with improved graphics and battery life.", Platform = "Nintendo Switch", ReleaseDate = "June 2025", Genre = "Nintendo Console" },
                new Game { Id = 17, Title = "Switch Lite Hyrule Edition", Price = 250m, ImageUrl = "/images/nintendo/consoles/switch-lite-hyrule.png", Category = "Nintendo Console", Slug = "switch-lite-hyrule", Description = "Special edition Lite console with Zelda-themed design.", Platform = "Nintendo Switch", ReleaseDate = "September 2024", Genre = "Nintendo Console" },
                new Game { Id = 18, Title = "Switch OLED", Price = 274m, ImageUrl = "/images/nintendo/consoles/switch-oled.png", Category = "Nintendo Console", Slug = "switch-oled", Description = "Enhanced OLED display console for Nintendo Switch.", Platform = "Nintendo Switch", ReleaseDate = "October 2021", Genre = "Nintendo Console" },
                new Game { Id = 19, Title = "Switch Lite (Yellow)", Price = 170m, ImageUrl = "/images/nintendo/consoles/switch-lite-yellow.png", Category = "Nintendo Console", Slug = "switch-lite-yellow", Description = "Compact and portable Nintendo Switch Lite in Yellow.", Platform = "Nintendo Switch", ReleaseDate = "September 2019", Genre = "Nintendo Console" },
                new Game { Id = 20, Title = "Nintendo Switch Lite", Price = 184m, ImageUrl = "/images/nintendo/consoles/switch-lite2.png", Category = "Nintendo Console", Slug = "switch-lite2", Description = "Standard Nintendo Switch Lite for portable gaming.", Platform = "Nintendo Switch", ReleaseDate = "September 2019", Genre = "Nintendo Console" },

                // Nintendo Games
                new Game { Id = 21, Title = "Pokémon Scarlet", Price = 38m, ImageUrl = "/images/nintendo/games/pokemon-scarlet.png", Category = "Nintendo Game", Slug = "pokemon-scarlet", Description = "Embark on a brand new Pokémon adventure with Scarlet edition.", Platform = "Nintendo Switch", ReleaseDate = "November 2022", Genre = "Nintendo Game" },
                new Game { Id = 22, Title = "Pokémon Violet", Price = 38m, ImageUrl = "/images/nintendo/games/pokemon-violet.png", Category = "Nintendo Game", Slug = "pokemon-violet", Description = "Explore the regions of Violet and catch new Pokémon.", Platform = "Nintendo Switch", ReleaseDate = "November 2022", Genre = "Nintendo Game" },
                new Game { Id = 23, Title = "Mario & Luigi: Brothership", Price = 29m, ImageUrl = "/images/nintendo/games/mario-luigi-brothership.png", Category = "Nintendo Game", Slug = "mario-luigi-brothership", Description = "Join Mario & Luigi in a brand-new cooperative adventure.", Platform = "Nintendo Switch", ReleaseDate = "November 2024", Genre = "Nintendo Game" },
                new Game { Id = 24, Title = "Super Mario 3D World + Bowser’s Fury", Price = 38m, ImageUrl = "/images/nintendo/games/super-mario-3d-world-bowsers-fury.png", Category = "Nintendo Game", Slug = "super-mario-3d-world-bowsers-fury", Description = "Two exciting adventures in one package.", Platform = "Nintendo Switch", ReleaseDate = "February 2021", Genre = "Nintendo Game" },
                new Game { Id = 25, Title = "Drag x Drive", Price = 17m, ImageUrl = "/images/nintendo/games/drag-drive.png", Category = "Nintendo Game", Slug = "drag-drive", Description = "High-speed drag racing on Nintendo Switch.", Platform = "Nintendo Switch", ReleaseDate = "August 2025", Genre = "Nintendo Game" },

                // Nintendo New In
                new Game { Id = 26, Title = "The Legend of Zelda: Tears of the Kingdom", Price = 60m, ImageUrl = "/images/nintendo/newin/the-legend-of-zelda-tears-of-the-kingdom.png", Category = "Nintendo New In", Slug = "zelda-tears-of-the-kingdom", Description = "Epic new Zelda adventure across Hyrule.", Platform = "Nintendo Switch", ReleaseDate = "May 2023", Genre = "Nintendo New In" },
                new Game { Id = 27, Title = "Super Mario Party Jamboree", Price = 38m, ImageUrl = "/images/nintendo/newin/super-mario-party-jamboree.png", Category = "Nintendo New In", Slug = "super-mario-party-jamboree", Description = "Party with friends in Mario's latest mini-game extravaganza.", Platform = "Nintendo Switch", ReleaseDate = "October 2024", Genre = "Nintendo New In" },
                new Game { Id = 28, Title = "Donkey Kong Bananza", Price = 57m, ImageUrl = "/images/nintendo/newin/donkey-kong-bananza.png", Category = "Nintendo New In", Slug = "donkey-kong-bananza", Description = "Swing through barrels and beat the competition.", Platform = "Nintendo Switch", ReleaseDate = "July 2025", Genre = "Nintendo New In" },
                new Game { Id = 29, Title = "Pokémon Legends: Z-A", Price = 42m, ImageUrl = "/images/nintendo/newin/pokémon-legends-Z-A.png", Category = "Nintendo New In", Slug = "pokemon-legends-za", Description = "Explore new regions and legendary Pokémon.", Platform = "Nintendo Switch", ReleaseDate = "October 2025", Genre = "Nintendo New In" },
                new Game { Id = 30, Title = "Mario Kart World", Price = 60m, ImageUrl = "/images/nintendo/newin/mario-kart-world.png", Category = "Nintendo New In", Slug = "mario-kart-world", Description = "Race around the world in this exciting Mario Kart edition.", Platform = "Nintendo Switch", ReleaseDate = "June 2025", Genre = "Nintendo New In" },
                new Game { Id = 31, Title = "Metroid Prime 4: Beyond", Price = 50m, ImageUrl = "/images/nintendo/newin/metroid-prime-4-beyond.png", Category = "Nintendo New In", Slug = "metroid-prime-4-beyond", Description = "New Metroid adventure with enhanced gameplay.", Platform = "Nintendo Switch", ReleaseDate = "June 2025", Genre = "Nintendo New In" },
                new Game { Id = 32, Title = "Sonic Racing: CrossWorlds", Price = 43m, ImageUrl = "/images/nintendo/newin/sonic-racing-crossWorlds.png", Category = "Nintendo New In", Slug = "sonic-racing-crossworlds", Description = "Fast-paced racing with Sonic and friends.", Platform = "Nintendo Switch", ReleaseDate = "September 2025", Genre = "Nintendo New In" },
                new Game { Id = 33, Title = "Little Nightmares III Nintendo Switch", Price = 33m, ImageUrl = "/images/nintendo/newin/little-nightmares-3-nintendo-switch.png", Category = "Nintendo New In", Slug = "little-nightmares-3-ns", Description = "Spooky and thrilling adventures await.", Platform = "Nintendo Switch", ReleaseDate = "October 2025", Genre = "Nintendo New In" },
                new Game { Id = 34, Title = "EA Sports FC 26 Nintendo Switch", Price = 45m, ImageUrl = "/images/nintendo/newin/ea-sports-fc-26-nintendo-switch.png", Category = "Nintendo New In", Slug = "ea-fc-26-ns", Description = "Take your football game on the go.", Platform = "Nintendo Switch", ReleaseDate = "September 2025", Genre = "Nintendo New In" },
                new Game { Id = 35, Title = "LEGO Party", Price = 39m, ImageUrl = "/images/nintendo/newin/lego-party.png", Category = "Nintendo New In", Slug = "lego-party", Description = "Creative building and party fun in one game.", Platform = "Nintendo Switch", ReleaseDate = "September 2025", Genre = "Nintendo New In" }
            );

            // Seed Xbox Products
            modelBuilder.Entity<Game>().HasData(
                // Xbox Accessories
                new Game { Id = 36, Title = "Xbox Rechargeable Battery + USB-C Cable", Price = 20m, ImageUrl = "/images/xbox/accessories/xbox-battery-usbc.png", Category = "Xbox Accessories", Slug = "xbox-battery-usbc", Description = "Stay powered with the official rechargeable battery pack and USB-C cable.", Platform = "Xbox Series X|S", ReleaseDate = "November 2020", Genre = "Xbox Accessory" },
                new Game { Id = 37, Title = "Xbox Wireless Controller (Standard)", Price = 50m, ImageUrl = "/images/xbox/accessories/xbox-controller-standard.png", Category = "Xbox Accessories", Slug = "xbox-controller-standard", Description = "Experience the modern Xbox controller design with enhanced comfort and precision.", Platform = "Xbox Series X|S", ReleaseDate = "November 2020", Genre = "Xbox Accessory" },
                new Game { Id = 38, Title = "Xbox Elite Wireless Controller Series 2", Price = 150m, ImageUrl = "/images/xbox/accessories/xbox-elite-controller-series2.png", Category = "Xbox Accessories", Slug = "xbox-elite-controller-series2", Description = "High-performance controller with interchangeable thumbsticks and paddles.", Platform = "Xbox Series X|S", ReleaseDate = "November 2019", Genre = "Xbox Accessory" },
                new Game { Id = 39, Title = "Xbox Wireless Headset", Price = 110m, ImageUrl = "/images/xbox/accessories/xbox-wireless-headset.png", Category = "Xbox Accessories", Slug = "xbox-wireless-headset", Description = "Immerse yourself with crystal-clear sound and wireless freedom.", Platform = "Xbox Series X|S", ReleaseDate = "March 2021", Genre = "Xbox Accessory" },
                new Game { Id = 40, Title = "Xbox Stereo Headset", Price = 45m, ImageUrl = "/images/xbox/accessories/xbox-stereo-headset.png", Category = "Xbox Accessories", Slug = "xbox-stereo-headset", Description = "Comfortable wired headset delivering rich, clear audio.", Platform = "Xbox Series X|S", ReleaseDate = "September 2021", Genre = "Xbox Accessory" },

                // Xbox Consoles
                new Game { Id = 41, Title = "Xbox Series S (512 GB)", Price = 300m, ImageUrl = "/images/xbox/consoles/xbox-series-s-512.png", Category = "Xbox Console", Slug = "xbox-series-s-512", Description = "Compact next-gen console delivering powerful performance in a smaller size.", Platform = "Xbox Series S", ReleaseDate = "November 2020", Genre = "Xbox Console" },
                new Game { Id = 42, Title = "Xbox Series X (1 TB)", Price = 500m, ImageUrl = "/images/xbox/consoles/xbox-series-x-1tb.png", Category = "Xbox Console", Slug = "xbox-series-x-1tb", Description = "The fastest, most powerful Xbox console ever built.", Platform = "Xbox Series X", ReleaseDate = "November 2020", Genre = "Xbox Console" },
                new Game { Id = 43, Title = "Xbox Series X - Digital Edition (1 TB)", Price = 450m, ImageUrl = "/images/xbox/consoles/xbox-series-x-digital-1tb.png", Category = "Xbox Console", Slug = "xbox-series-x-digital-1tb", Description = "All-digital edition of the Xbox Series X with 1TB storage.", Platform = "Xbox Series X", ReleaseDate = "October 2024", Genre = "Xbox Console" },
                new Game { Id = 44, Title = "Xbox Series S (1 TB)", Price = 330m, ImageUrl = "/images/xbox/consoles/xbox-series-s-1tb.png", Category = "Xbox Console", Slug = "xbox-series-s-1tb", Description = "Digital-only console with larger 1TB SSD storage.", Platform = "Xbox Series S", ReleaseDate = "September 2023", Genre = "Xbox Console" },
                new Game { Id = 45, Title = "Xbox One", Price = 150m, ImageUrl = "/images/xbox/consoles/xbox-one.png", Category = "Xbox Console", Slug = "xbox-one", Description = "Classic Xbox One console for gaming, entertainment, and media.", Platform = "Xbox One", ReleaseDate = "November 2013", Genre = "Xbox Console" },

                // Xbox Games
                new Game { Id = 46, Title = "Borderlands 4", Price = 58m, ImageUrl = "/images/xbox/games/borderlands4.png", Category = "Xbox Game", Slug = "borderlands-4", Description = "Explore chaotic worlds and collect tons of loot in the latest Borderlands adventure.", Platform = "Xbox Series X|S", ReleaseDate = "September 2025", Genre = "Xbox Game" },
                new Game { Id = 47, Title = "Ninja Gaiden 4", Price = 50m, ImageUrl = "/images/xbox/games/ninja-gaiden-4.png", Category = "Xbox Game", Slug = "ninja-gaiden-4", Description = "Fast-paced combat and deadly precision return in this action-packed sequel.", Platform = "Xbox Series X|S", ReleaseDate = "October 2025", Genre = "Xbox Game" },
                new Game { Id = 48, Title = "Civilization VII", Price = 40m, ImageUrl = "/images/xbox/games/civilization7-2.png", Category = "Xbox Game", Slug = "civilization-7", Description = "Build an empire that will stand the test of time in this new strategy masterpiece.", Platform = "Xbox Series X|S", ReleaseDate = "February 2025", Genre = "Xbox Game" },
                new Game { Id = 49, Title = "Assassin’s Creed Shadows", Price = 40m, ImageUrl = "/images/xbox/games/assassins-creed-shadows-2.png", Category = "Xbox Game", Slug = "assassins-creed-shadows", Description = "Journey through Feudal Japan in Ubisoft’s next-generation adventure.", Platform = "Xbox Series X|S", ReleaseDate = "March 2025", Genre = "Xbox Game" },
                new Game { Id = 50, Title = "Blood West", Price = 34.99m, ImageUrl = "/images/xbox/games/blood-west-2.png", Category = "Xbox Game", Slug = "blood-west", Description = "Survive the cursed Wild West in this dark FPS horror experience.", Platform = "Xbox Series X|S", ReleaseDate = "February 2022", Genre = "Xbox Game" },

                // Xbox New In (Home Page)
                new Game { Id = 51, Title = "Blood West", Price = 34.99m, ImageUrl = "/images/xbox/newin/blood-west.png", Category = "Xbox New In", Slug = "blood-west-newin", Description = "Survive the cursed Wild West in this eerie first-person horror.", Platform = "Xbox Series X|S", ReleaseDate = "February 2022", Genre = "Xbox New In" },
                new Game { Id = 52, Title = "Ninja Gaiden 4", Price = 59.99m, ImageUrl = "/images/xbox/newin/ninja-gaiden-4.png", Category = "Xbox New In", Slug = "ninja-gaiden-4-newin", Description = "Sharpen your blade in the next generation of fast-paced ninja action.", Platform = "Xbox Series X|S", ReleaseDate = "October 2025", Genre = "Xbox New In" },
                new Game { Id = 53, Title = "Call of Duty: Black Ops 7", Price = 69.99m, ImageUrl = "/images/xbox/newin/cod-black-ops-7.png", Category = "Xbox New In", Slug = "cod-black-ops-7", Description = "Return to the shadows in the most intense Black Ops yet.", Platform = "Xbox Series X|S", ReleaseDate = "November 2025", Genre = "Xbox New In" },
                new Game { Id = 54, Title = "Gears of War Reloaded", Price = 64.99m, ImageUrl = "/images/xbox/newin/gears-of-war-reloaded.png", Category = "Xbox New In", Slug = "gears-of-war-reloaded", Description = "Revisit the origins of the Gears saga with next-gen visuals.", Platform = "Xbox Series X|S", ReleaseDate = "August 2025", Genre = "Xbox New In" },
                new Game { Id = 55, Title = "Tony Hawk Pro Skater 3+4", Price = 44.99m, ImageUrl = "/images/xbox/newin/tony-hawk-pro-skater-3-4.png", Category = "Xbox New In", Slug = "tony-hawk-pro-skater-3-4", Description = "Skate through remastered classics with new features and levels.", Platform = "Xbox Series X|S", ReleaseDate = "July 2025", Genre = "Xbox New In" },
                new Game { Id = 56, Title = "High on Life 2", Price = 39.99m, ImageUrl = "/images/xbox/newin/high-on-life.png", Category = "Xbox New In", Slug = "high-on-life-2", Description = "The bizarre comedy FPS returns with even more talking weapons.", Platform = "Xbox Series X|S", ReleaseDate = "November 2025", Genre = "Xbox New In" },
                new Game { Id = 57, Title = "Persona 4 Remake", Price = 54.99m, ImageUrl = "/images/xbox/newin/persona-4-remake.png", Category = "Xbox New In", Slug = "persona-4-remake", Description = "Rediscover a beloved classic reimagined for modern consoles.", Platform = "Xbox Series X|S", ReleaseDate = "June 2025", Genre = "Xbox New In" },
                new Game { Id = 58, Title = "The Outer Worlds 2", Price = 59.99m, ImageUrl = "/images/xbox/newin/outer-worlds-2.png", Category = "Xbox New In", Slug = "outer-worlds-2", Description = "Embark on a new galactic journey in this space-faring RPG sequel.", Platform = "Xbox Series X|S", ReleaseDate = "October 2025", Genre = "Xbox New In" },
                new Game { Id = 59, Title = "Final Fantasy XVI", Price = 49.99m, ImageUrl = "/images/xbox/newin/final-fantasy-16.png", Category = "Xbox New In", Slug = "final-fantasy-16", Description = "Experience an epic fantasy story full of intrigue and stunning visuals.", Platform = "Xbox Series X|S", ReleaseDate = "June 2023", Genre = "Xbox New In" },
                new Game { Id = 60, Title = "Keeper", Price = 49.99m, ImageUrl = "/images/xbox/newin/keeper.png", Category = "Xbox New In", Slug = "keeper", Description = "Protect your realm in this visually stunning action RPG.", Platform = "Xbox Series X|S", ReleaseDate = "November 2025", Genre = "Xbox New In" }
            );

            // Seed PlayStation Products
            modelBuilder.Entity<Game>().HasData(
                // PlayStation Accessories
                new Game { Id = 61, Title = "DualSense Charging Station", Price = 25m, ImageUrl = "/images/playstation/accessories/dualsense-charging-station.png", Category = "PlayStation Accessories", Slug = "dualsense-charging-station", Description = "Charge your DualSense controllers quickly and safely.", Platform = "PlayStation 5", ReleaseDate = "November 2020", Genre = "PlayStation Accessory" },
                new Game { Id = 62, Title = "PlayStation 5 HD Camera", Price = 48m, ImageUrl = "/images/playstation/accessories/ps5-hd-camera.png", Category = "PlayStation Accessories", Slug = "ps5-hd-camera", Description = "Stream in high definition with the PS5 HD Camera.", Platform = "PlayStation 5", ReleaseDate = "November 2020", Genre = "PlayStation Accessory" },
                new Game { Id = 63, Title = "Pulse 3D Wireless Headset", Price = 130m, ImageUrl = "/images/playstation/accessories/pulse3d-headset.png", Category = "PlayStation Accessories", Slug = "pulse3d-headset", Description = "Immersive 3D audio experience for PS5.", Platform = "PlayStation 5", ReleaseDate = "October 2020", Genre = "PlayStation Accessory" },
                new Game { Id = 64, Title = "DualSense Wireless Controller (PS5)", Price = 60m, ImageUrl = "/images/playstation/accessories/dualsense-controller.png", Category = "PlayStation Accessories", Slug = "dualsense-controller", Description = "Next-gen controller with adaptive triggers.", Platform = "PlayStation 5", ReleaseDate = "November 2020", Genre = "PlayStation Accessory" },
                new Game { Id = 65, Title = "PlayStation 5 Media Remote", Price = 33m, ImageUrl = "/images/playstation/accessories/ps5-media-remote.png", Category = "PlayStation Accessories", Slug = "ps5-media-remote", Description = "Control media playback easily on your PS5.", Platform = "PlayStation 5", ReleaseDate = "November 2020", Genre = "PlayStation Accessory" },

                // PlayStation Consoles
                new Game { Id = 66, Title = "PlayStation 5 Standard", Price = 479m, ImageUrl = "/images/playstation/consoles/ps5-standard.png", Category = "PlayStation Consoles", Slug = "ps5-standard", Description = "Next-gen console with 4K gaming and ultra-fast SSD.", Platform = "PlayStation 5", ReleaseDate = "November 2020", Genre = "PlayStation Console" },
                new Game { Id = 67, Title = "PlayStation 5 Digital Edition", Price = 399m, ImageUrl = "/images/playstation/consoles/ps5-digital.png", Category = "PlayStation Consoles", Slug = "ps5-digital", Description = "All-digital PS5 with full next-gen features.", Platform = "PlayStation 5", ReleaseDate = "November 2020", Genre = "PlayStation Console" },
                new Game { Id = 68, Title = "PlayStation 4 Pro", Price = 279m, ImageUrl = "/images/playstation/consoles/ps4-pro.png", Category = "PlayStation Consoles", Slug = "ps4-pro", Description = "Enhanced PS4 experience with improved graphics.", Platform = "PlayStation 4", ReleaseDate = "November 2016", Genre = "PlayStation Console" },
                new Game { Id = 69, Title = "PlayStation 4 Slim (500GB)", Price = 150m, ImageUrl = "/images/playstation/consoles/ps4-slim.png", Category = "PlayStation Consoles", Slug = "ps4-slim", Description = "Slimline PS4 with compact design and reliable performance.", Platform = "PlayStation 4", ReleaseDate = "September 2016", Genre = "PlayStation Console" },
                new Game { Id = 70, Title = "PlayStation 4 WW2 Bundle", Price = 245m, ImageUrl = "/images/playstation/consoles/ps4-ww2-bundle.png", Category = "PlayStation Consoles", Slug = "ps4-ww2-bundle", Description = "PS4 with Call of Duty: WW2 bundle included.", Platform = "PlayStation 4", ReleaseDate = "November 2017", Genre = "PlayStation Console" },

                // PlayStation Games
                new Game { Id = 71, Title = "Battlefield 6", Price = 50m, ImageUrl = "/images/playstation/games/battlefield-6.png", Category = "PlayStation Games", Slug = "battlefield-6", Description = "Experience next-gen large-scale warfare in stunning detail with up to 128-player battles.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation Game" },
                new Game { Id = 72, Title = "Doom: The Dark Ages", Price = 55m, ImageUrl = "/images/playstation/games/doom-dark-ages.png", Category = "PlayStation Games", Slug = "doom-dark-ages", Description = "Rip and tear through the medieval hellscapes of this intense Doom prequel.", Platform = "PS5/PS4", ReleaseDate = "May 2025", Genre = "PlayStation Game" },
                new Game { Id = 73, Title = "NHL 25", Price = 10m, ImageUrl = "/images/playstation/games/nhl25.png", Category = "PlayStation Games", Slug = "nhl-25", Description = "Step into the world of professional hockey with enhanced physics and new career modes.", Platform = "PS5/PS4", ReleaseDate = "October 2024", Genre = "PlayStation Game" },
                new Game { Id = 74, Title = "Little Nightmares III", Price = 35m, ImageUrl = "/images/playstation/games/little-nightmares-3.png", Category = "PlayStation Games", Slug = "little-nightmares-3", Description = "Venture into dark, twisted worlds in this chilling and emotional horror platformer.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation Game" },
                new Game { Id = 75, Title = "Monster Hunter Wilds", Price = 25m, ImageUrl = "/images/playstation/games/monster-hunter-wilds.png", Category = "PlayStation Games", Slug = "monster-hunter-wilds", Description = "Explore vast landscapes and hunt incredible beasts in this next-gen Monster Hunter adventure.", Platform = "PS5/PS4", ReleaseDate = "February 2025", Genre = "PlayStation Game" },

                // PlayStation New In
                new Game { Id = 76, Title = "Ghost of Yōtei", Price = 69.99m, ImageUrl = "/images/playstation/newin/ghost-of-yotei.png", Category = "PlayStation New In", Slug = "ghost-of-yotei", Description = "Uncover the mysteries of the snowy island in this action-adventure game.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 77, Title = "ARC Raiders", Price = 49.99m, ImageUrl = "/images/playstation/newin/arc-raiders.png", Category = "PlayStation New In", Slug = "arc-raiders", Description = "Join elite raiders in tactical cooperative missions.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 78, Title = "Jurassic World Evolution 3", Price = 49.99m, ImageUrl = "/images/playstation/newin/jurassic-world-evolution-3.png", Category = "PlayStation New In", Slug = "jurassic-world-evolution-3", Description = "Manage your park and dinosaurs in stunning detail.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 79, Title = "Death Stranding 2: On the Beach", Price = 54.99m, ImageUrl = "/images/playstation/newin/death-stranding-2.png", Category = "PlayStation New In", Slug = "death-stranding-2", Description = "Continue the epic journey in Hideo Kojima's sequel.", Platform = "PS5/PS4", ReleaseDate = "June 2025", Genre = "PlayStation New In" },
                new Game { Id = 80, Title = "Call of Duty: Black Ops 7", Price = 59.99m, ImageUrl = "/images/playstation/newin/cod-black-ops-7.png", Category = "PlayStation New In", Slug = "cod-black-ops-7", Description = "Return to iconic Black Ops multiplayer battles.", Platform = "PS5/PS4", ReleaseDate = "November 2025", Genre = "PlayStation New In" },
                new Game { Id = 81, Title = "Vampire: The Masquerade – Bloodlines 2", Price = 49.99m, ImageUrl = "/images/playstation/newin/vampire-bloodlines-2.png", Category = "PlayStation New In", Slug = "vampire-bloodlines-2", Description = "Dive into the dark vampire underworld of Seattle.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 82, Title = "Double Dragon Revive Limited Edition", Price = 35.95m, ImageUrl = "/images/playstation/newin/double-dragon-revive.png", Category = "PlayStation New In", Slug = "double-dragon-revive", Description = "Classic beat ‘em up action remastered.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 83, Title = "Digimon Story: Time Stranger", Price = 54.99m, ImageUrl = "/images/playstation/newin/digimon-time-stranger.png", Category = "PlayStation New In", Slug = "digimon-time-stranger", Description = "Time-traveling Digimon adventure.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 84, Title = "Little Nightmares III", Price = 32.99m, ImageUrl = "/images/playstation/newin/little-nightmares-3.png", Category = "PlayStation New In", Slug = "little-nightmares-3-newin", Description = "Chilling platformer adventure.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" },
                new Game { Id = 85, Title = "Battlefield 6", Price = 69.99m, ImageUrl = "/images/playstation/newin/battlefield-6.png", Category = "PlayStation New In", Slug = "battlefield-6-newin", Description = "Next-gen massive warfare action.", Platform = "PS5/PS4", ReleaseDate = "October 2025", Genre = "PlayStation New In" }
            );

            // Seed Computer Products
            modelBuilder.Entity<Game>().HasData(
                // Computer Accessories
                new Game { Id = 86, Title = "HyperX Pulsefire Haste 2 Gaming Mouse", Price = 22.99m, ImageUrl = "/images/computer/accessories/hyperx-pulsefire-haste2.png", Category = "Computer Accessories", Slug = "hyperx-pulsefire-haste2", Description = "Lightweight gaming mouse with precision tracking and ergonomic comfort.", Platform = "PC", ReleaseDate = "April 2023", Genre = "Computer Accessory" },
                new Game { Id = 87, Title = "USB-C to HDMI Adapter, Type-C to HDMI 4K Adapter", Price = 6.13m, ImageUrl = "/images/computer/accessories/usb-c-hdmi-adapter.png", Category = "Computer Accessories", Slug = "usb-c-hdmi-adapter", Description = "Connect USB-C laptops or tablets to HDMI displays in crisp 4K quality.", Platform = "PC", ReleaseDate = "August 2014", Genre = "Computer Accessory" },
                new Game { Id = 88, Title = "HyperX Cloud Alpha – Gaming Headset", Price = 59.99m, ImageUrl = "/images/computer/accessories/hyperx-cloud-alpha.png", Category = "Computer Accessories", Slug = "hyperx-cloud-alpha", Description = "Premium sound and comfort for immersive PC gaming.", Platform = "PC", ReleaseDate = "August 2017", Genre = "Computer Accessory" },
                new Game { Id = 89, Title = "YETI USB Streaming Microphone - Blackout", Price = 119.00m, ImageUrl = "/images/computer/accessories/yeti-usb-microphone.png", Category = "Computer Accessories", Slug = "yeti-usb-microphone", Description = "Professional-grade USB microphone for streaming and podcasting.", Platform = "PC", ReleaseDate = "August 2020", Genre = "Computer Accessory" },
                new Game { Id = 90, Title = "Logitech C310 HD Webcam, 720p/30fps", Price = 22.50m, ImageUrl = "/images/computer/accessories/logitech-c310.png", Category = "Computer Accessories", Slug = "logitech-c310", Description = "Reliable HD webcam ideal for streaming or video calls.", Platform = "PC", ReleaseDate = "July 2010", Genre = "Computer Accessory" },

                // Computer Consoles / Handhelds / Builds
                new Game { Id = 91, Title = "Steam Deck 256GB", Price = 354.00m, ImageUrl = "/images/computer/consoles/steam-deck-256gb.png", Category = "Computer Console", Slug = "steam-deck-256gb", Description = "Portable PC gaming device with full Steam library support.", Platform = "PC", ReleaseDate = "February 2022", Genre = "Computer Console" },
                new Game { Id = 92, Title = "ASUS ROG Ally 512GB", Price = 499.00m, ImageUrl = "/images/computer/consoles/asus-rog-ally-512gb.png", Category = "Computer Console", Slug = "asus-rog-ally-512gb", Description = "High-performance handheld gaming PC powered by Windows 11.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer Console" },
                new Game { Id = 93, Title = "Lenovo Legion Go 512GB", Price = 675.00m, ImageUrl = "/images/computer/consoles/lenovo-legion-go-512gb.png", Category = "Computer Console", Slug = "lenovo-legion-go-512gb", Description = "Powerful portable console with detachable controllers and 8.8” QHD display.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer Console" },
                new Game { Id = 94, Title = "MSI Claw A1M 512GB", Price = 549.99m, ImageUrl = "/images/computer/consoles/msi-claw-a1m-512gb.png", Category = "Computer Console", Slug = "msi-claw-a1m-512gb", Description = "Intel-powered gaming handheld built for performance and portability.", Platform = "PC", ReleaseDate = "March 2024", Genre = "Computer Console" },
                new Game { Id = 95, Title = "Zotac Zbox Magnus ER51060", Price = 785.57m, ImageUrl = "/images/computer/consoles/zotac-zbox-magnus-er51060.png", Category = "Computer Console", Slug = "zotac-zbox-magnus-er51060", Description = "Compact yet powerful mini-PC ideal for gaming and creative work.", Platform = "PC", ReleaseDate = "February 2018", Genre = "Computer Console" },

                // Computer Games
                new Game { Id = 96, Title = "Sea Fantasy", Price = 15m, ImageUrl = "/images/computer/games/sea-fantasy-2.png", Category = "Computer Game", Slug = "sea-fantasy", Description = "Sail through mysterious waters and uncover ancient secrets.", Platform = "PC", ReleaseDate = "January 2025", Genre = "Computer Game" },
                new Game { Id = 97, Title = "Ys Memoire: The Oath in Felghana", Price = 25m, ImageUrl = "/images/computer/games/ys-memoire-2.png", Category = "Computer Game", Slug = "ys-memoire", Description = "Classic RPG adventure remastered for modern systems.", Platform = "PC", ReleaseDate = "January 2025", Genre = "Computer Game" },
                new Game { Id = 98, Title = "Freedom Wars Remastered", Price = 16m, ImageUrl = "/images/computer/games/freedom-wars-remastered-2.png", Category = "Computer Game", Slug = "freedom-wars-remastered", Description = "Battle for freedom in this post-apocalyptic action RPG.", Platform = "PC", ReleaseDate = "January 2025", Genre = "Computer Game" },
                new Game { Id = 99, Title = "Magic INN", Price = 15m, ImageUrl = "/images/computer/games/magic-inn-2.png", Category = "Computer Game", Slug = "magic-inn", Description = "Run a magical tavern in a world full of potions, spells, and adventure.", Platform = "PC", ReleaseDate = "August 2025", Genre = "Computer Game" },
                new Game { Id = 100, Title = "Hyper Light Breaker", Price = 30m, ImageUrl = "/images/computer/games/hyper-light-breaker-2.png", Category = "Computer Game", Slug = "hyper-light-breaker", Description = "Explore the Overgrowth in this open-world roguelike action game.", Platform = "PC", ReleaseDate = "January 2025", Genre = "Computer Game" },

                // Computer New In (Homepage)
                new Game { Id = 101, Title = "Borderlands 4", Price = 58m, ImageUrl = "/images/computer/newin/borderlands-4.png", Category = "Computer New In", Slug = "borderlands-4", Description = "Return to Pandora for an explosive new looter-shooter experience.", Platform = "PC", ReleaseDate = "September 2025", Genre = "Computer New In" },
                new Game { Id = 102, Title = "Keeper", Price = 49.99m, ImageUrl = "/images/computer/newin/keeper.png", Category = "Computer New In", Slug = "keeper", Description = "Build, defend, and manage your dungeons in this dark fantasy adventure.", Platform = "PC", ReleaseDate = "November 2025", Genre = "Computer New In" },
                new Game { Id = 103, Title = "Ninja Gaiden 4", Price = 59.99m, ImageUrl = "/images/computer/newin/ninja-gaiden-4.png", Category = "Computer New In", Slug = "ninja-gaiden-4", Description = "Fast-paced hack-and-slash action returns with new mechanics.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" },
                new Game { Id = 104, Title = "Vampire: The Masquerade - Bloodlines 2", Price = 54.99m, ImageUrl = "/images/computer/newin/vampire-the-masquerade-bloodlines-2.png", Category = "Computer New In", Slug = "vampire-the-masquerade-bloodlines-2", Description = "Immerse yourself in the dark vampire world of Seattle.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" },
                new Game { Id = 105, Title = "ARC Raiders", Price = 49.99m, ImageUrl = "/images/computer/newin/arc-raiders.png", Category = "Computer New In", Slug = "arc-raiders", Description = "Team up and fight back against robotic threats in this co-op shooter.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" },
                new Game { Id = 106, Title = "Digimon Story: Time Stranger", Price = 54.99m, ImageUrl = "/images/computer/newin/digimon-time-stranger.png", Category = "Computer New In", Slug = "digimon-time-stranger", Description = "A new chapter in the Digimon Story RPG series.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" },
                new Game { Id = 107, Title = "The Outer Worlds 2", Price = 59.99m, ImageUrl = "/images/computer/newin/outer-worlds-2.png", Category = "Computer New In", Slug = "outer-worlds-2", Description = "Embark on a satirical sci-fi adventure across the stars.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" },
                new Game { Id = 108, Title = "King of Meat", Price = 38.99m, ImageUrl = "/images/computer/newin/king-of-meat.png", Category = "Computer New In", Slug = "king-of-meat", Description = "Defend your butcher empire in this over-the-top combat game.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" },
                new Game { Id = 109, Title = "Professional Baseball Spirits (Korean Version)", Price = 90m, ImageUrl = "/images/computer/newin/professional-baseball-spirits.png", Category = "Computer New In", Slug = "professional-baseball-spirits", Description = "The ultimate baseball simulation for fans of the sport.", Platform = "PC", ReleaseDate = "October 2024", Genre = "Computer New In" },
                new Game { Id = 110, Title = "NASCAR 25 (PC)", Price = 40m, ImageUrl = "/images/computer/newin/nascar-25-pc.png", Category = "Computer New In", Slug = "nascar-25", Description = "Race to victory in the newest installment of the NASCAR series.", Platform = "PC", ReleaseDate = "October 2025", Genre = "Computer New In" }
            );
        }
    }
}
