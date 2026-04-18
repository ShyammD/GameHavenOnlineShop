using GameHaven.DataAccess; 
using GameHaven_Online_Shop.Helpers; 
using GameHaven_Online_Shop.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace GameHaven_Online_Shop.Controllers
{
    public class XboxController : Controller
    {
        private readonly ApplicationDbContext _context; // Database context instance

        public XboxController(ApplicationDbContext context)
        {
            _context = context; // Initialise database context
        }

        // Helper method for sorting products based on the selected option
        private static IQueryable<GameHaven.Models.Models.Game> ApplySort(
            IQueryable<GameHaven.Models.Models.Game> query, string? sort)
        {
            return (sort ?? string.Empty).ToLower() switch
            {
                "price-asc" => query.OrderBy(g => g.Price), // Sort by price ascending
                "price-desc" => query.OrderByDescending(g => g.Price), // Sort by price descending
                "name-asc" => query.OrderBy(g => g.Title), // Sort by name A-Z
                "name-desc" => query.OrderByDescending(g => g.Title), // Sort by name Z-A
                _ => query.OrderBy(g => g.Id) // Default sorting by ID
            };
        }

        // GET: /Xbox
        public async Task<IActionResult> Index(string? q, string? sort)
        {
            // Redirect search queries to SearchController for consistency
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Retrieve "Xbox New In" products from database
            var products = _context.Games.Where(g => g.Category == "Xbox New In");

            // Apply sorting to products
            products = ApplySort(products, sort);

            // Store search and sort values for use in the View
            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            // Return the view with list of products
            return View(await products.ToListAsync());
        }

        // GET: /Xbox/Consoles
        public async Task<IActionResult> Consoles(string? q, string? sort)
        {
            // Redirect to SearchController if search query is entered
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Retrieve Xbox Consoles category
            var products = _context.Games.Where(g => g.Category == "Xbox Console");

            // Apply sorting
            products = ApplySort(products, sort);

            // Preserve search and sort state for UI
            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            // Return view with product list
            return View(await products.ToListAsync());
        }

        // GET: /Xbox/Accessories
        public async Task<IActionResult> Accessories(string? q, string? sort)
        {
            // Redirect to SearchController if query provided
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Retrieve Xbox Accessories category
            var products = _context.Games.Where(g => g.Category == "Xbox Accessories");

            // Apply sorting
            products = ApplySort(products, sort);

            // Pass search/sort state to the view
            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            // Return view with list of items
            return View(await products.ToListAsync());
        }

        // GET: /Xbox/Games
        public async Task<IActionResult> Games(string? q, string? sort)
        {
            // Redirect search to SearchController if query is entered
            if (!string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index", "Search", new { q, sort });

            // Retrieve Xbox Games category
            var products = _context.Games.Where(g => g.Category == "Xbox Game");

            // Apply sorting
            products = ApplySort(products, sort);

            // Preserve current search/sort parameters
            ViewData["CurrentSearch"] = q;
            ViewData["CurrentSort"] = sort;

            // Return the list of games to the view
            return View(await products.ToListAsync());
        }

        // GET: /Xbox/Details/{slug}/{category}
        [Route("Xbox/Details/{slug}/{category}")]
        public async Task<IActionResult> Details(string slug, string category)
        {
            // Redirect if parameters are missing
            if (string.IsNullOrWhiteSpace(slug) || string.IsNullOrWhiteSpace(category))
                return RedirectToAction(nameof(Index));

            // Fetch single product by slug and category
            var product = await _context.Games
                .FirstOrDefaultAsync(g =>
                    g.Slug.ToLower() == slug.ToLower() &&
                    (g.Category ?? "").ToLower() == category.ToLower());

            // Redirect to index if product not found
            if (product == null)
                return RedirectToAction(nameof(Index));

            // Get related products from the same category
            var related = await _context.Games
                .Where(g => g.Id != product.Id &&
                            (g.Category ?? "").ToLower() == (product.Category ?? "").ToLower())
                .ToListAsync();

            // Store related products for the view
            ViewData["RelatedProducts"] = related;

            // Return product details view
            return View(product);
        }

        // POST: /Xbox/AddToBasket
        [HttpPost]
        public IActionResult AddToBasket(string id, string title, decimal price, string image)
        {
            // Get basket from session or create a new list
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();

            // Check if product already exists in basket
            var existingItem = basket.FirstOrDefault(x => x.Id == id);
            if (existingItem != null)
            {
                existingItem.Quantity++; // Increase quantity if exists
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

            // Save updated basket to session
            HttpContext.Session.SetObjectAsJson("Basket", basket);

            // Redirect to Basket view
            return RedirectToAction(nameof(Basket));
        }

        // GET: /Xbox/Basket
        public IActionResult Basket()
        {
            // Retrieve basket from session or create empty list
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();

            // Return Basket view with items
            return View(basket);
        }
    }
}