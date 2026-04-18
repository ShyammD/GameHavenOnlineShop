// Using statements for diagnostics, models, data access, MVC, EF Core, and helpers
using System.Diagnostics;
using GameHaven_Online_Shop.Models;
using GameHaven.DataAccess;
using GameHaven.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameHaven_Online_Shop.Helpers;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for Home page, product display, and basket management
    public class HomeController : Controller
    {
        // Logger for diagnostic messages
        private readonly ILogger<HomeController> _logger;

        // Database context
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Home page with optional search and sort
        public async Task<IActionResult> Index(string? q, string? search, string? sort)
        {
            var query = !string.IsNullOrWhiteSpace(q) ? q : search;

            // Redirect to global search if query is present
            if (!string.IsNullOrWhiteSpace(query))
                return RedirectToAction("Index", "Search", new { q = query, sort });

            // Display Home Page Best Sellers
            IQueryable<Game> bestSellers = _context.Games
                .Where(g => g.Category == "Home Page Best Seller");

            bestSellers = (sort ?? string.Empty).ToLower() switch
            {
                "price-asc" => bestSellers.OrderBy(g => g.Price),
                "price-desc" => bestSellers.OrderByDescending(g => g.Price),
                "name-asc" => bestSellers.OrderBy(g => g.Title),
                "name-desc" => bestSellers.OrderByDescending(g => g.Title),
                _ => bestSellers.OrderBy(g => g.Title)
            };

            ViewData["CurrentSearch"] = "";
            ViewData["CurrentSort"] = sort;

            return View(await bestSellers.AsNoTracking().ToListAsync());
        }

        // GET: Display product details by slug
        [Route("Home/Details/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return NotFound();

            var game = await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Slug.ToLower() == slug.ToLower());

            if (game == null)
                return NotFound();

            // Fetch related products in same category
            var relatedGames = await _context.Games
                .AsNoTracking()
                .Where(g => g.Category == game.Category && g.Id != game.Id)
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedGames = relatedGames;
            return View(game);
        }

        // GET: Privacy page
        public IActionResult Privacy() => View();

        // GET: Error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: Add an item to basket
        [HttpPost]
        public IActionResult AddToBasket(string id, string title, decimal price, string image)
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();

            // Increment quantity if item exists
            var existingItem = basket.FirstOrDefault(x => x.Id == id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                // Add new item to basket
                basket.Add(new BasketItem
                {
                    Id = id,
                    Title = title,
                    Price = price,
                    Image = image
                });
            }

            HttpContext.Session.SetObjectAsJson("Basket", basket);
            return RedirectToAction("Basket");
        }

        // GET: View basket
        public IActionResult Basket()
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            return View(basket);
        }

        // POST: Update item quantity in basket
        [HttpPost]
        public IActionResult UpdateQuantity(string id, int delta)
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            var item = basket.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Quantity += delta;
                if (item.Quantity <= 0)
                {
                    basket.Remove(item);
                }
            }

            HttpContext.Session.SetObjectAsJson("Basket", basket);
            return RedirectToAction("Basket");
        }

        // POST: Remove item from basket
        [HttpPost]
        public IActionResult RemoveFromBasket(string id)
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            basket.RemoveAll(x => x.Id == id);
            HttpContext.Session.SetObjectAsJson("Basket", basket);
            return RedirectToAction("Basket");
        }
    }
}