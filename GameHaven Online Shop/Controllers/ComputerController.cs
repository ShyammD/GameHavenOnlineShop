// Using statements for data access, helpers, models, MVC, and EF Core
using GameHaven.DataAccess;
using GameHaven_Online_Shop.Helpers;
using GameHaven_Online_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for Computer category products and basket management
    public class ComputerController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for database context
        public ComputerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Apply sorting to a query based on sort parameter
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

        // GET: /Computer
        public async Task<IActionResult> Index(string? q, string? sort)
        {
            // Redirect to SearchController if query exists
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Fetch "Computer New In" products
            var products = _context.Games.Where(g => g.Category == "Computer New In");

            // Apply sorting
            products = ApplySort(products, sort);

            // Preserve search and sort state for view
            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // GET: /Computer/Consoles
        public async Task<IActionResult> Consoles(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Fetch Computer Consoles
            var products = _context.Games.Where(g => g.Category == "Computer Console");

            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // GET: /Computer/Accessories
        public async Task<IActionResult> Accessories(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Fetch Computer Accessories
            var products = _context.Games.Where(g => g.Category == "Computer Accessories");

            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // GET: /Computer/Games
        public async Task<IActionResult> Games(string? q, string? sort)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Fetch Computer Games
            var products = _context.Games.Where(g => g.Category == "Computer Game");

            products = ApplySort(products, sort);

            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            return View(await products.ToListAsync());
        }

        // GET: /Computer/Details/{slug}/{category}
        [Route("Computer/Details/{slug}/{category}")]
        public async Task<IActionResult> Details(string slug, string category)
        {
            if (string.IsNullOrWhiteSpace(slug) || string.IsNullOrWhiteSpace(category))
                return RedirectToAction(nameof(Index));

            // Fetch product details by slug and category
            var product = await _context.Games
                .FirstOrDefaultAsync(g =>
                    g.Slug.ToLower() == slug.ToLower() &&
                    (g.Category ?? "").ToLower() == category.ToLower());

            if (product == null)
                return RedirectToAction(nameof(Index));

            // Fetch related products in same category
            var related = await _context.Games
                .Where(g => g.Id != product.Id &&
                            (g.Category ?? "").ToLower() == (product.Category ?? "").ToLower())
                .ToListAsync();

            ViewData["RelatedProducts"] = related;
            return View(product);
        }

        // POST: /Computer/AddToBasket
        [HttpPost]
        public IActionResult AddToBasket(string id, string title, decimal price, string image)
        {
            // Get basket from session or initialize new list
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();

            // Increment quantity if item already exists
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

            // Save basket back to session
            HttpContext.Session.SetObjectAsJson("Basket", basket);

            return RedirectToAction(nameof(Basket));
        }

        // GET: /Computer/Basket
        public IActionResult Basket()
        {
            // Retrieve basket from session
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            return View(basket);
        }
    }
}