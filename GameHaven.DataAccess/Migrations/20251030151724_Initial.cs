using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReleaseDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Category", "Description", "Genre", "ImageUrl", "Platform", "Price", "ReleaseDate", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, "Home Page Best Seller", "Take your basketball career to the next level with NBA 2K26.", "Sports / Simulation", "/images/homepage/bestsellers/nba2k26.png", "PS5, PS4, Xbox, PC", 62m, "September 2025", "nba-2k26", "NBA 2K26" },
                    { 2, "Home Page Best Seller", "Experience the ultimate football simulation in EA FC 26.", "Sports / Simulation", "/images/homepage/bestsellers/eafc26.png", "PS5, PS4, Xbox, PC", 69m, "September 2025", "ea-fc-26", "EA FC 26" },
                    { 3, "Home Page Best Seller", "Step into the ring and become a wrestling champion in WWE 2K25.", "Sports / Action", "/images/homepage/bestsellers/wwe2k25.png", "PS5, PS4, Xbox, PC", 25m, "March 2025", "wwe-2k25", "WWE 2K25" },
                    { 4, "Home Page Best Seller", "Explore Los Santos in this epic open-world crime adventure.", "Action / Adventure", "/images/homepage/bestsellers/gta5.png", "PS5, PS4, Xbox, PC", 40m, "March 2022", "gta-5", "GTA 5" },
                    { 5, "Home Page Best Seller", "Engage in intense warfare in the latest Call of Duty Black Ops game.", "Shooter / Action", "/images/homepage/bestsellers/codbo6.png", "PS5, PS4, Xbox, PC", 39m, "October 2024", "cod-black-ops-6", "COD Black Ops 6" },
                    { 6, "Home Page Best Seller", "Loot, shoot and explore the chaotic world of Borderlands 4.", "Shooter / RPG", "/images/homepage/bestsellers/borderlands4.png", "PS5, PS4, Xbox, PC", 58m, "September 2025", "borderlands-4", "Borderlands 4" },
                    { 7, "Home Page Best Seller", "Venture into dark, twisted worlds in this chilling platformer.", "Horror / Platformer", "/images/homepage/bestsellers/little-nightmares-3.png", "PS5, PS4, Switch, PC", 35m, "October 2025", "little-nightmares-3", "Little Nightmares III" },
                    { 8, "Home Page Best Seller", "Embark on a stealthy adventure full of intrigue.", "Action / Stealth", "/images/homepage/bestsellers/assassins-creed-shadows.png", "PS5, PS4, Xbox, PC", 40m, "March 2025", "assassins-creed-shadows", "Assassin’s Creed Shadows" },
                    { 9, "Home Page Best Seller", "Catch, train and battle in the latest Pokémon adventure.", "Adventure / RPG", "/images/homepage/bestsellers/pokemon-legends-za.png", "Switch", 32m, "October 2025", "pokemon-legends-za", "Pokémon Legends Z-A" },
                    { 10, "Home Page Best Seller", "Race at lightning speed across amazing worlds with Sonic and friends.", "Racing / Arcade", "/images/homepage/bestsellers/sonic-racing-crossworlds.png", "Switch", 43m, "September 2025", "sonic-racing-crossworlds", "Sonic Racing: CrossWorlds" },
                    { 11, "Nintendo Accessories", "High-quality dock for Nintendo Switch with extra HDMI support.", "Nintendo Accessory", "/images/nintendo/accessories/switch-dock-set.png", "Nintendo Switch", 120m, "June 2025", "switch-dock-set", "Nintendo Switch Dock Set" },
                    { 12, "Nintendo Accessories", "Protect your console on the go with this durable case and screen protector.", "Nintendo Accessory", "/images/nintendo/accessories/switch-case-screen-protector.png", "Nintendo Switch", 30m, "June 2025", "switch-case-screen-protector", "Nintendo Switch Carrying Case + Screen Protector" },
                    { 13, "Nintendo Accessories", "Charge your Joy-Cons while you play for uninterrupted gaming.", "Nintendo Accessory", "/images/nintendo/accessories/joycon-charging-grip.png", "Nintendo Switch", 13m, "October 2024", "joycon-charging-grip", "Nintendo Switch Joy-Con Charging Grip" },
                    { 14, "Nintendo Accessories", "Comfortable leg strap accessory for Ring Fit Adventure.", "Nintendo Accessory", "/images/nintendo/accessories/switch-leg-strap.png", "Nintendo Switch", 15m, "April 2922", "switch-leg-strap", "Nintendo Switch Leg Strap" },
                    { 15, "Nintendo Accessories", "Official AC adapter to keep your Switch charged.", "Nintendo Accessory", "/images/nintendo/accessories/switch-ac-adapter.png", "Nintendo Switch", 15m, "March 2017", "switch-ac-adapter", "Nintendo Switch AC Adapter" },
                    { 16, "Nintendo Console", "Next-gen Nintendo Switch with improved graphics and battery life.", "Nintendo Console", "/images/nintendo/consoles/switch2.png", "Nintendo Switch", 380m, "June 2025", "switch-2", "Nintendo Switch 2" },
                    { 17, "Nintendo Console", "Special edition Lite console with Zelda-themed design.", "Nintendo Console", "/images/nintendo/consoles/switch-lite-hyrule.png", "Nintendo Switch", 250m, "September 2024", "switch-lite-hyrule", "Switch Lite Hyrule Edition" },
                    { 18, "Nintendo Console", "Enhanced OLED display console for Nintendo Switch.", "Nintendo Console", "/images/nintendo/consoles/switch-oled.png", "Nintendo Switch", 274m, "October 2021", "switch-oled", "Switch OLED" },
                    { 19, "Nintendo Console", "Compact and portable Nintendo Switch Lite in Yellow.", "Nintendo Console", "/images/nintendo/consoles/switch-lite-yellow.png", "Nintendo Switch", 170m, "September 2019", "switch-lite-yellow", "Switch Lite (Yellow)" },
                    { 20, "Nintendo Console", "Standard Nintendo Switch Lite for portable gaming.", "Nintendo Console", "/images/nintendo/consoles/switch-lite2.png", "Nintendo Switch", 184m, "September 2019", "switch-lite2", "Nintendo Switch Lite" },
                    { 21, "Nintendo Game", "Embark on a brand new Pokémon adventure with Scarlet edition.", "Nintendo Game", "/images/nintendo/games/pokemon-scarlet.png", "Nintendo Switch", 38m, "November 2022", "pokemon-scarlet", "Pokémon Scarlet" },
                    { 22, "Nintendo Game", "Explore the regions of Violet and catch new Pokémon.", "Nintendo Game", "/images/nintendo/games/pokemon-violet.png", "Nintendo Switch", 38m, "November 2022", "pokemon-violet", "Pokémon Violet" },
                    { 23, "Nintendo Game", "Join Mario & Luigi in a brand-new cooperative adventure.", "Nintendo Game", "/images/nintendo/games/mario-luigi-brothership.png", "Nintendo Switch", 29m, "November 2024", "mario-luigi-brothership", "Mario & Luigi: Brothership" },
                    { 24, "Nintendo Game", "Two exciting adventures in one package.", "Nintendo Game", "/images/nintendo/games/super-mario-3d-world-bowsers-fury.png", "Nintendo Switch", 38m, "February 2021", "super-mario-3d-world-bowsers-fury", "Super Mario 3D World + Bowser’s Fury" },
                    { 25, "Nintendo Game", "High-speed drag racing on Nintendo Switch.", "Nintendo Game", "/images/nintendo/games/drag-drive.png", "Nintendo Switch", 17m, "August 2025", "drag-drive", "Drag x Drive" },
                    { 26, "Nintendo New In", "Epic new Zelda adventure across Hyrule.", "Nintendo New In", "/images/nintendo/newin/the-legend-of-zelda-tears-of-the-kingdom.png", "Nintendo Switch", 60m, "May 2023", "zelda-tears-of-the-kingdom", "The Legend of Zelda: Tears of the Kingdom" },
                    { 27, "Nintendo New In", "Party with friends in Mario's latest mini-game extravaganza.", "Nintendo New In", "/images/nintendo/newin/super-mario-party-jamboree.png", "Nintendo Switch", 38m, "October 2024", "super-mario-party-jamboree", "Super Mario Party Jamboree" },
                    { 28, "Nintendo New In", "Swing through barrels and beat the competition.", "Nintendo New In", "/images/nintendo/newin/donkey-kong-bananza.png", "Nintendo Switch", 57m, "July 2025", "donkey-kong-bananza", "Donkey Kong Bananza" },
                    { 29, "Nintendo New In", "Explore new regions and legendary Pokémon.", "Nintendo New In", "/images/nintendo/newin/pokémon-legends-Z-A.png", "Nintendo Switch", 42m, "October 2025", "pokemon-legends-za", "Pokémon Legends: Z-A" },
                    { 30, "Nintendo New In", "Race around the world in this exciting Mario Kart edition.", "Nintendo New In", "/images/nintendo/newin/mario-kart-world.png", "Nintendo Switch", 60m, "June 2025", "mario-kart-world", "Mario Kart World" },
                    { 31, "Nintendo New In", "New Metroid adventure with enhanced gameplay.", "Nintendo New In", "/images/nintendo/newin/metroid-prime-4-beyond.png", "Nintendo Switch", 50m, "June 2025", "metroid-prime-4-beyond", "Metroid Prime 4: Beyond" },
                    { 32, "Nintendo New In", "Fast-paced racing with Sonic and friends.", "Nintendo New In", "/images/nintendo/newin/sonic-racing-crossWorlds.png", "Nintendo Switch", 43m, "September 2025", "sonic-racing-crossworlds", "Sonic Racing: CrossWorlds" },
                    { 33, "Nintendo New In", "Spooky and thrilling adventures await.", "Nintendo New In", "/images/nintendo/newin/little-nightmares-3-nintendo-switch.png", "Nintendo Switch", 33m, "October 2025", "little-nightmares-3-ns", "Little Nightmares III Nintendo Switch" },
                    { 34, "Nintendo New In", "Take your football game on the go.", "Nintendo New In", "/images/nintendo/newin/ea-sports-fc-26-nintendo-switch.png", "Nintendo Switch", 45m, "September 2025", "ea-fc-26-ns", "EA Sports FC 26 Nintendo Switch" },
                    { 35, "Nintendo New In", "Creative building and party fun in one game.", "Nintendo New In", "/images/nintendo/newin/lego-party.png", "Nintendo Switch", 39m, "September 2025", "lego-party", "LEGO Party" },
                    { 36, "Xbox Accessories", "Stay powered with the official rechargeable battery pack and USB-C cable.", "Xbox Accessory", "/images/xbox/accessories/xbox-battery-usbc.png", "Xbox Series X|S", 20m, "November 2020", "xbox-battery-usbc", "Xbox Rechargeable Battery + USB-C Cable" },
                    { 37, "Xbox Accessories", "Experience the modern Xbox controller design with enhanced comfort and precision.", "Xbox Accessory", "/images/xbox/accessories/xbox-controller-standard.png", "Xbox Series X|S", 50m, "November 2020", "xbox-controller-standard", "Xbox Wireless Controller (Standard)" },
                    { 38, "Xbox Accessories", "High-performance controller with interchangeable thumbsticks and paddles.", "Xbox Accessory", "/images/xbox/accessories/xbox-elite-controller-series2.png", "Xbox Series X|S", 150m, "November 2019", "xbox-elite-controller-series2", "Xbox Elite Wireless Controller Series 2" },
                    { 39, "Xbox Accessories", "Immerse yourself with crystal-clear sound and wireless freedom.", "Xbox Accessory", "/images/xbox/accessories/xbox-wireless-headset.png", "Xbox Series X|S", 110m, "March 2021", "xbox-wireless-headset", "Xbox Wireless Headset" },
                    { 40, "Xbox Accessories", "Comfortable wired headset delivering rich, clear audio.", "Xbox Accessory", "/images/xbox/accessories/xbox-stereo-headset.png", "Xbox Series X|S", 45m, "September 2021", "xbox-stereo-headset", "Xbox Stereo Headset" },
                    { 41, "Xbox Console", "Compact next-gen console delivering powerful performance in a smaller size.", "Xbox Console", "/images/xbox/consoles/xbox-series-s-512.png", "Xbox Series S", 300m, "November 2020", "xbox-series-s-512", "Xbox Series S (512 GB)" },
                    { 42, "Xbox Console", "The fastest, most powerful Xbox console ever built.", "Xbox Console", "/images/xbox/consoles/xbox-series-x-1tb.png", "Xbox Series X", 500m, "November 2020", "xbox-series-x-1tb", "Xbox Series X (1 TB)" },
                    { 43, "Xbox Console", "All-digital edition of the Xbox Series X with 1TB storage.", "Xbox Console", "/images/xbox/consoles/xbox-series-x-digital-1tb.png", "Xbox Series X", 450m, "October 2024", "xbox-series-x-digital-1tb", "Xbox Series X - Digital Edition (1 TB)" },
                    { 44, "Xbox Console", "Digital-only console with larger 1TB SSD storage.", "Xbox Console", "/images/xbox/consoles/xbox-series-s-1tb.png", "Xbox Series S", 330m, "September 2023", "xbox-series-s-1tb", "Xbox Series S (1 TB)" },
                    { 45, "Xbox Console", "Classic Xbox One console for gaming, entertainment, and media.", "Xbox Console", "/images/xbox/consoles/xbox-one.png", "Xbox One", 150m, "November 2013", "xbox-one", "Xbox One" },
                    { 46, "Xbox Game", "Explore chaotic worlds and collect tons of loot in the latest Borderlands adventure.", "Xbox Game", "/images/xbox/games/borderlands4.png", "Xbox Series X|S", 58m, "September 2025", "borderlands-4", "Borderlands 4" },
                    { 47, "Xbox Game", "Fast-paced combat and deadly precision return in this action-packed sequel.", "Xbox Game", "/images/xbox/games/ninja-gaiden-4.png", "Xbox Series X|S", 50m, "October 2025", "ninja-gaiden-4", "Ninja Gaiden 4" },
                    { 48, "Xbox Game", "Build an empire that will stand the test of time in this new strategy masterpiece.", "Xbox Game", "/images/xbox/games/civilization7-2.png", "Xbox Series X|S", 40m, "February 2025", "civilization-7", "Civilization VII" },
                    { 49, "Xbox Game", "Journey through Feudal Japan in Ubisoft’s next-generation adventure.", "Xbox Game", "/images/xbox/games/assassins-creed-shadows-2.png", "Xbox Series X|S", 40m, "March 2025", "assassins-creed-shadows", "Assassin’s Creed Shadows" },
                    { 50, "Xbox Game", "Survive the cursed Wild West in this dark FPS horror experience.", "Xbox Game", "/images/xbox/games/blood-west-2.png", "Xbox Series X|S", 34.99m, "February 2022", "blood-west", "Blood West" },
                    { 51, "Xbox New In", "Survive the cursed Wild West in this eerie first-person horror.", "Xbox New In", "/images/xbox/newin/blood-west.png", "Xbox Series X|S", 34.99m, "February 2022", "blood-west-newin", "Blood West" },
                    { 52, "Xbox New In", "Sharpen your blade in the next generation of fast-paced ninja action.", "Xbox New In", "/images/xbox/newin/ninja-gaiden-4.png", "Xbox Series X|S", 59.99m, "October 2025", "ninja-gaiden-4-newin", "Ninja Gaiden 4" },
                    { 53, "Xbox New In", "Return to the shadows in the most intense Black Ops yet.", "Xbox New In", "/images/xbox/newin/cod-black-ops-7.png", "Xbox Series X|S", 69.99m, "November 2025", "cod-black-ops-7", "Call of Duty: Black Ops 7" },
                    { 54, "Xbox New In", "Revisit the origins of the Gears saga with next-gen visuals.", "Xbox New In", "/images/xbox/newin/gears-of-war-reloaded.png", "Xbox Series X|S", 64.99m, "August 2025", "gears-of-war-reloaded", "Gears of War Reloaded" },
                    { 55, "Xbox New In", "Skate through remastered classics with new features and levels.", "Xbox New In", "/images/xbox/newin/tony-hawk-pro-skater-3-4.png", "Xbox Series X|S", 44.99m, "July 2025", "tony-hawk-pro-skater-3-4", "Tony Hawk Pro Skater 3+4" },
                    { 56, "Xbox New In", "The bizarre comedy FPS returns with even more talking weapons.", "Xbox New In", "/images/xbox/newin/high-on-life.png", "Xbox Series X|S", 39.99m, "November 2025", "high-on-life-2", "High on Life 2" },
                    { 57, "Xbox New In", "Rediscover a beloved classic reimagined for modern consoles.", "Xbox New In", "/images/xbox/newin/persona-4-remake.png", "Xbox Series X|S", 54.99m, "June 2025", "persona-4-remake", "Persona 4 Remake" },
                    { 58, "Xbox New In", "Embark on a new galactic journey in this space-faring RPG sequel.", "Xbox New In", "/images/xbox/newin/outer-worlds-2.png", "Xbox Series X|S", 59.99m, "October 2025", "outer-worlds-2", "The Outer Worlds 2" },
                    { 59, "Xbox New In", "Experience an epic fantasy story full of intrigue and stunning visuals.", "Xbox New In", "/images/xbox/newin/final-fantasy-16.png", "Xbox Series X|S", 49.99m, "June 2023", "final-fantasy-16", "Final Fantasy XVI" },
                    { 60, "Xbox New In", "Protect your realm in this visually stunning action RPG.", "Xbox New In", "/images/xbox/newin/keeper.png", "Xbox Series X|S", 49.99m, "November 2025", "keeper", "Keeper" },
                    { 61, "PlayStation Accessories", "Charge your DualSense controllers quickly and safely.", "PlayStation Accessory", "/images/playstation/accessories/dualsense-charging-station.png", "PlayStation 5", 25m, "November 2020", "dualsense-charging-station", "DualSense Charging Station" },
                    { 62, "PlayStation Accessories", "Stream in high definition with the PS5 HD Camera.", "PlayStation Accessory", "/images/playstation/accessories/ps5-hd-camera.png", "PlayStation 5", 48m, "November 2020", "ps5-hd-camera", "PlayStation 5 HD Camera" },
                    { 63, "PlayStation Accessories", "Immersive 3D audio experience for PS5.", "PlayStation Accessory", "/images/playstation/accessories/pulse3d-headset.png", "PlayStation 5", 130m, "October 2020", "pulse3d-headset", "Pulse 3D Wireless Headset" },
                    { 64, "PlayStation Accessories", "Next-gen controller with adaptive triggers.", "PlayStation Accessory", "/images/playstation/accessories/dualsense-controller.png", "PlayStation 5", 60m, "November 2020", "dualsense-controller", "DualSense Wireless Controller (PS5)" },
                    { 65, "PlayStation Accessories", "Control media playback easily on your PS5.", "PlayStation Accessory", "/images/playstation/accessories/ps5-media-remote.png", "PlayStation 5", 33m, "November 2020", "ps5-media-remote", "PlayStation 5 Media Remote" },
                    { 66, "PlayStation Consoles", "Next-gen console with 4K gaming and ultra-fast SSD.", "PlayStation Console", "/images/playstation/consoles/ps5-standard.png", "PlayStation 5", 479m, "November 2020", "ps5-standard", "PlayStation 5 Standard" },
                    { 67, "PlayStation Consoles", "All-digital PS5 with full next-gen features.", "PlayStation Console", "/images/playstation/consoles/ps5-digital.png", "PlayStation 5", 399m, "November 2020", "ps5-digital", "PlayStation 5 Digital Edition" },
                    { 68, "PlayStation Consoles", "Enhanced PS4 experience with improved graphics.", "PlayStation Console", "/images/playstation/consoles/ps4-pro.png", "PlayStation 4", 279m, "November 2016", "ps4-pro", "PlayStation 4 Pro" },
                    { 69, "PlayStation Consoles", "Slimline PS4 with compact design and reliable performance.", "PlayStation Console", "/images/playstation/consoles/ps4-slim.png", "PlayStation 4", 150m, "September 2016", "ps4-slim", "PlayStation 4 Slim (500GB)" },
                    { 70, "PlayStation Consoles", "PS4 with Call of Duty: WW2 bundle included.", "PlayStation Console", "/images/playstation/consoles/ps4-ww2-bundle.png", "PlayStation 4", 245m, "November 2017", "ps4-ww2-bundle", "PlayStation 4 WW2 Bundle" },
                    { 71, "PlayStation Games", "Experience next-gen large-scale warfare in stunning detail with up to 128-player battles.", "PlayStation Game", "/images/playstation/games/battlefield-6.png", "PS5/PS4", 50m, "October 2025", "battlefield-6", "Battlefield 6" },
                    { 72, "PlayStation Games", "Rip and tear through the medieval hellscapes of this intense Doom prequel.", "PlayStation Game", "/images/playstation/games/doom-dark-ages.png", "PS5/PS4", 55m, "May 2025", "doom-dark-ages", "Doom: The Dark Ages" },
                    { 73, "PlayStation Games", "Step into the world of professional hockey with enhanced physics and new career modes.", "PlayStation Game", "/images/playstation/games/nhl25.png", "PS5/PS4", 10m, "October 2024", "nhl-25", "NHL 25" },
                    { 74, "PlayStation Games", "Venture into dark, twisted worlds in this chilling and emotional horror platformer.", "PlayStation Game", "/images/playstation/games/little-nightmares-3.png", "PS5/PS4", 35m, "October 2025", "little-nightmares-3", "Little Nightmares III" },
                    { 75, "PlayStation Games", "Explore vast landscapes and hunt incredible beasts in this next-gen Monster Hunter adventure.", "PlayStation Game", "/images/playstation/games/monster-hunter-wilds.png", "PS5/PS4", 25m, "February 2025", "monster-hunter-wilds", "Monster Hunter Wilds" },
                    { 76, "PlayStation New In", "Uncover the mysteries of the snowy island in this action-adventure game.", "PlayStation New In", "/images/playstation/newin/ghost-of-yotei.png", "PS5/PS4", 69.99m, "October 2025", "ghost-of-yotei", "Ghost of Yōtei" },
                    { 77, "PlayStation New In", "Join elite raiders in tactical cooperative missions.", "PlayStation New In", "/images/playstation/newin/arc-raiders.png", "PS5/PS4", 49.99m, "October 2025", "arc-raiders", "ARC Raiders" },
                    { 78, "PlayStation New In", "Manage your park and dinosaurs in stunning detail.", "PlayStation New In", "/images/playstation/newin/jurassic-world-evolution-3.png", "PS5/PS4", 49.99m, "October 2025", "jurassic-world-evolution-3", "Jurassic World Evolution 3" },
                    { 79, "PlayStation New In", "Continue the epic journey in Hideo Kojima's sequel.", "PlayStation New In", "/images/playstation/newin/death-stranding-2.png", "PS5/PS4", 54.99m, "June 2025", "death-stranding-2", "Death Stranding 2: On the Beach" },
                    { 80, "PlayStation New In", "Return to iconic Black Ops multiplayer battles.", "PlayStation New In", "/images/playstation/newin/cod-black-ops-7.png", "PS5/PS4", 59.99m, "November 2025", "cod-black-ops-7", "Call of Duty: Black Ops 7" },
                    { 81, "PlayStation New In", "Dive into the dark vampire underworld of Seattle.", "PlayStation New In", "/images/playstation/newin/vampire-bloodlines-2.png", "PS5/PS4", 49.99m, "October 2025", "vampire-bloodlines-2", "Vampire: The Masquerade – Bloodlines 2" },
                    { 82, "PlayStation New In", "Classic beat ‘em up action remastered.", "PlayStation New In", "/images/playstation/newin/double-dragon-revive.png", "PS5/PS4", 35.95m, "October 2025", "double-dragon-revive", "Double Dragon Revive Limited Edition" },
                    { 83, "PlayStation New In", "Time-traveling Digimon adventure.", "PlayStation New In", "/images/playstation/newin/digimon-time-stranger.png", "PS5/PS4", 54.99m, "October 2025", "digimon-time-stranger", "Digimon Story: Time Stranger" },
                    { 84, "PlayStation New In", "Chilling platformer adventure.", "PlayStation New In", "/images/playstation/newin/little-nightmares-3.png", "PS5/PS4", 32.99m, "October 2025", "little-nightmares-3-newin", "Little Nightmares III" },
                    { 85, "PlayStation New In", "Next-gen massive warfare action.", "PlayStation New In", "/images/playstation/newin/battlefield-6.png", "PS5/PS4", 69.99m, "October 2025", "battlefield-6-newin", "Battlefield 6" },
                    { 86, "Computer Accessories", "Lightweight gaming mouse with precision tracking and ergonomic comfort.", "Computer Accessory", "/images/computer/accessories/hyperx-pulsefire-haste2.png", "PC", 22.99m, "April 2023", "hyperx-pulsefire-haste2", "HyperX Pulsefire Haste 2 Gaming Mouse" },
                    { 87, "Computer Accessories", "Connect USB-C laptops or tablets to HDMI displays in crisp 4K quality.", "Computer Accessory", "/images/computer/accessories/usb-c-hdmi-adapter.png", "PC", 6.13m, "August 2014", "usb-c-hdmi-adapter", "USB-C to HDMI Adapter, Type-C to HDMI 4K Adapter" },
                    { 88, "Computer Accessories", "Premium sound and comfort for immersive PC gaming.", "Computer Accessory", "/images/computer/accessories/hyperx-cloud-alpha.png", "PC", 59.99m, "August 2017", "hyperx-cloud-alpha", "HyperX Cloud Alpha – Gaming Headset" },
                    { 89, "Computer Accessories", "Professional-grade USB microphone for streaming and podcasting.", "Computer Accessory", "/images/computer/accessories/yeti-usb-microphone.png", "PC", 119.00m, "August 2020", "yeti-usb-microphone", "YETI USB Streaming Microphone - Blackout" },
                    { 90, "Computer Accessories", "Reliable HD webcam ideal for streaming or video calls.", "Computer Accessory", "/images/computer/accessories/logitech-c310.png", "PC", 22.50m, "July 2010", "logitech-c310", "Logitech C310 HD Webcam, 720p/30fps" },
                    { 91, "Computer Console", "Portable PC gaming device with full Steam library support.", "Computer Console", "/images/computer/consoles/steam-deck-256gb.png", "PC", 354.00m, "February 2022", "steam-deck-256gb", "Steam Deck 256GB" },
                    { 92, "Computer Console", "High-performance handheld gaming PC powered by Windows 11.", "Computer Console", "/images/computer/consoles/asus-rog-ally-512gb.png", "PC", 499.00m, "October 2025", "asus-rog-ally-512gb", "ASUS ROG Ally 512GB" },
                    { 93, "Computer Console", "Powerful portable console with detachable controllers and 8.8” QHD display.", "Computer Console", "/images/computer/consoles/lenovo-legion-go-512gb.png", "PC", 675.00m, "October 2025", "lenovo-legion-go-512gb", "Lenovo Legion Go 512GB" },
                    { 94, "Computer Console", "Intel-powered gaming handheld built for performance and portability.", "Computer Console", "/images/computer/consoles/msi-claw-a1m-512gb.png", "PC", 549.99m, "March 2024", "msi-claw-a1m-512gb", "MSI Claw A1M 512GB" },
                    { 95, "Computer Console", "Compact yet powerful mini-PC ideal for gaming and creative work.", "Computer Console", "/images/computer/consoles/zotac-zbox-magnus-er51060.png", "PC", 785.57m, "February 2018", "zotac-zbox-magnus-er51060", "Zotac Zbox Magnus ER51060" },
                    { 96, "Computer Game", "Sail through mysterious waters and uncover ancient secrets.", "Computer Game", "/images/computer/games/sea-fantasy-2.png", "PC", 15m, "January 2025", "sea-fantasy", "Sea Fantasy" },
                    { 97, "Computer Game", "Classic RPG adventure remastered for modern systems.", "Computer Game", "/images/computer/games/ys-memoire-2.png", "PC", 25m, "January 2025", "ys-memoire", "Ys Memoire: The Oath in Felghana" },
                    { 98, "Computer Game", "Battle for freedom in this post-apocalyptic action RPG.", "Computer Game", "/images/computer/games/freedom-wars-remastered-2.png", "PC", 16m, "January 2025", "freedom-wars-remastered", "Freedom Wars Remastered" },
                    { 99, "Computer Game", "Run a magical tavern in a world full of potions, spells, and adventure.", "Computer Game", "/images/computer/games/magic-inn-2.png", "PC", 15m, "August 2025", "magic-inn", "Magic INN" },
                    { 100, "Computer Game", "Explore the Overgrowth in this open-world roguelike action game.", "Computer Game", "/images/computer/games/hyper-light-breaker-2.png", "PC", 30m, "January 2025", "hyper-light-breaker", "Hyper Light Breaker" },
                    { 101, "Computer New In", "Return to Pandora for an explosive new looter-shooter experience.", "Computer New In", "/images/computer/newin/borderlands-4.png", "PC", 58m, "September 2025", "borderlands-4", "Borderlands 4" },
                    { 102, "Computer New In", "Build, defend, and manage your dungeons in this dark fantasy adventure.", "Computer New In", "/images/computer/newin/keeper.png", "PC", 49.99m, "November 2025", "keeper", "Keeper" },
                    { 103, "Computer New In", "Fast-paced hack-and-slash action returns with new mechanics.", "Computer New In", "/images/computer/newin/ninja-gaiden-4.png", "PC", 59.99m, "October 2025", "ninja-gaiden-4", "Ninja Gaiden 4" },
                    { 104, "Computer New In", "Immerse yourself in the dark vampire world of Seattle.", "Computer New In", "/images/computer/newin/vampire-the-masquerade-bloodlines-2.png", "PC", 54.99m, "October 2025", "vampire-the-masquerade-bloodlines-2", "Vampire: The Masquerade - Bloodlines 2" },
                    { 105, "Computer New In", "Team up and fight back against robotic threats in this co-op shooter.", "Computer New In", "/images/computer/newin/arc-raiders.png", "PC", 49.99m, "October 2025", "arc-raiders", "ARC Raiders" },
                    { 106, "Computer New In", "A new chapter in the Digimon Story RPG series.", "Computer New In", "/images/computer/newin/digimon-time-stranger.png", "PC", 54.99m, "October 2025", "digimon-time-stranger", "Digimon Story: Time Stranger" },
                    { 107, "Computer New In", "Embark on a satirical sci-fi adventure across the stars.", "Computer New In", "/images/computer/newin/outer-worlds-2.png", "PC", 59.99m, "October 2025", "outer-worlds-2", "The Outer Worlds 2" },
                    { 108, "Computer New In", "Defend your butcher empire in this over-the-top combat game.", "Computer New In", "/images/computer/newin/king-of-meat.png", "PC", 38.99m, "October 2025", "king-of-meat", "King of Meat" },
                    { 109, "Computer New In", "The ultimate baseball simulation for fans of the sport.", "Computer New In", "/images/computer/newin/professional-baseball-spirits.png", "PC", 90m, "October 2024", "professional-baseball-spirits", "Professional Baseball Spirits (Korean Version)" },
                    { 110, "Computer New In", "Race to victory in the newest installment of the NASCAR series.", "Computer New In", "/images/computer/newin/nascar-25-pc.png", "PC", 40m, "October 2025", "nascar-25", "NASCAR 25 (PC)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
