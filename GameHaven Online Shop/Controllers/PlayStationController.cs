// Using statements for data access, helpers, models, MVC, and EF Core
using GameHaven.DataAccess;
using GameHaven_Online_Shop.Helpers;
using GameHaven_Online_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for PlayStation products and basket management
    public class PlayStationController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _context;

        // Constructor
        public PlayStationController(ApplicationDbContext context)
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

        // PlayStation main page
        public async Task<IActionResult> Index(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var products = _context.Games.Where(g => g.Category == "PlayStation New In");
            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // PlayStation Consoles page
        public async Task<IActionResult> Consoles(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var products = _context.Games.Where(g => g.Category == "PlayStation Consoles");
            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // PlayStation Accessories page
        public async Task<IActionResult> Accessories(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var products = _context.Games.Where(g => g.Category == "PlayStation Accessories");
            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // PlayStation Games page
        public async Task<IActionResult> Games(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            var products = _context.Games.Where(g => g.Category == "PlayStation Games");
            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // Product details page
        [Route("PlayStation/Details/{slug}/{category}")]
        public async Task<IActionResult> Details(string slug, string category)
        {
            if (string.IsNullOrWhiteSpace(slug) || string.IsNullOrWhiteSpace(category))
                return RedirectToAction(nameof(Index));

            var product = await _context.Games
                .FirstOrDefaultAsync(g =>
                    g.Slug.ToLower() == slug.ToLower() &&
                    (g.Category ?? "").ToLower() == category.ToLower());

            if (product == null)
                return RedirectToAction(nameof(Index));

            var related = await _context.Games
                .Where(g => g.Id != product.Id &&
                            (g.Category ?? "").ToLower() == (product.Category ?? "").ToLower())
                .ToListAsync();

            ViewData["RelatedProducts"] = related;
            return View(product);
        }

        // POST: Add product to basket
        [HttpPost]
        public IActionResult AddToBasket(string id, string title, decimal price, string image)
        {
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            var existingItem = basket.FirstOrDefault(x => x.Id == id);

            if (existingItem != null)
                existingItem.Quantity++;
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