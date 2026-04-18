// Using statements for data access, helpers, models, MVC, and EF Core
using GameHaven.DataAccess;
using GameHaven_Online_Shop.Helpers;
using GameHaven_Online_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for Nintendo products and basket management
    public class NintendoController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public NintendoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method for sorting products
        private static IQueryable<GameHaven.Models.Models.Game> ApplySort(
            IQueryable<GameHaven.Models.Models.Game> query, string? sort)
        {
            return (sort ?? string.Empty).ToLower() switch
            {
                "price-asc" => query.OrderBy(g => g.Price),
                "price-desc" => query.OrderByDescending(g => g.Price),
                "name-asc" => query.OrderBy(g => g.Title),
                "name-desc" => query.OrderByDescending(g => g.Title),
                _ => query.OrderBy(g => g.Id)
            };
        }

        // Nintendo Main Page
        public async Task<IActionResult> Index(string? q, string? sort)
        {
            // Redirect to Search page if query exists
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Get Nintendo New In products
            var items = _context.Games
                .Where(g => g.Category == "Nintendo New In");

            // Apply sorting
            items = ApplySort(items, sort);

            ViewData["CurrentSort"] = sort;
            ViewData["CurrentSearch"] = q;

            return View(await items.ToListAsync());
        }

        // Nintendo Consoles page
        public async Task<IActionResult> Consoles(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var items = _context.Games
                .Where(g => g.Category == "Nintendo Console");

            items = ApplySort(items, sort);

            ViewData["CurrentSort"] = sort;
            ViewData["CurrentSearch"] = q;

            return View(await items.ToListAsync());
        }

        // Nintendo Accessories page
        public async Task<IActionResult> Accessories(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var items = _context.Games
                .Where(g => g.Category == "Nintendo Accessories");

            items = ApplySort(items, sort);

            ViewData["CurrentSort"] = sort;
            ViewData["CurrentSearch"] = q;

            return View(await items.ToListAsync());
        }

        // Nintendo Games page
        public async Task<IActionResult> Games(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var items = _context.Games
                .Where(g => g.Category == "Nintendo Game");

            items = ApplySort(items, sort);

            ViewData["CurrentSort"] = sort;
            ViewData["CurrentSearch"] = q;

            return View(await items.ToListAsync());
        }

        // Product details page
        [Route("Nintendo/Details/{slug}/{category}")]
        public async Task<IActionResult> Details(string slug, string category)
        {
            if (string.IsNullOrWhiteSpace(slug) || string.IsNullOrWhiteSpace(category))
                return RedirectToAction(nameof(Index));

            // Get product details
            var product = await _context.Games
                .FirstOrDefaultAsync(g =>
                    g.Slug.ToLower() == slug.ToLower() &&
                    (g.Category ?? "").ToLower() == category.ToLower());

            if (product == null)
                return RedirectToAction(nameof(Index));

            // Get related products
            var related = await _context.Games
                .Where(g => g.Id != product.Id &&
                            (g.Category ?? "").ToLower() == (product.Category ?? "").ToLower())
                .ToListAsync();

            ViewData["RelatedProducts"] = related;
            return View(product);
        }

        // POST: Add item to basket
        [HttpPost]
        public IActionResult AddToBasket(string id, string title, decimal price, string image)
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            var existing = basket.FirstOrDefault(x => x.Id == id);

            if (existing != null)
                existing.Quantity++;
            else
                basket.Add(new BasketItem { Id = id, Title = title, Price = price, Image = image });

            HttpContext.Session.SetObjectAsJson("Basket", basket);
            return RedirectToAction(nameof(Basket));
        }

        // GET: View basket
        public IActionResult Basket()
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            return View(basket);
        }
    }
}