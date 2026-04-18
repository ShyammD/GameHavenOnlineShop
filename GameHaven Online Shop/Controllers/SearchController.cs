using GameHaven.DataAccess;
using GameHaven.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller to handle search queries across the store
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public SearchController(ApplicationDbContext context) => _context = context;

        // GET /Search - Handles search queries and sorting
        [HttpGet("/Search")]
        public async Task<IActionResult> Index(string q, string sort)
        {
            ViewData["q"] = q ?? "";                // Preserve search query in view
            ViewData["CurrentSort"] = sort ?? "";   // Preserve sort order in view

            if (string.IsNullOrWhiteSpace(q))
                return View("Results", new List<Game>());  // Empty results if no query

            var term = q.Trim().ToLower();
            IQueryable<Game> query = _context.Games;

            // Platform-specific filtering shortcuts
            if (term is "ps5" or "playstation" or "playstation 5")
            {
                query = query.Where(g =>
                    (g.Platform != null && EF.Functions.Like(g.Platform, "%PlayStation%")) ||
                    (g.Category != null && EF.Functions.Like(g.Category, "PlayStation%")));
            }
            else if (term is "xbox")
            {
                query = query.Where(g =>
                    (g.Platform != null && EF.Functions.Like(g.Platform, "%Xbox%")) ||
                    (g.Category != null && EF.Functions.Like(g.Category, "Xbox%")));
            }
            else if (term is "switch" or "nintendo" or "nintendo switch")
            {
                query = query.Where(g =>
                    (g.Platform != null && EF.Functions.Like(g.Platform, "%Switch%")) ||
                    (g.Category != null && EF.Functions.Like(g.Category, "Nintendo%")));
            }
            else
            {
                // General search across multiple fields
                var tokens = term.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in tokens)
                {
                    var tok = $"%{t}%";
                    query = query.Where(g =>
                        EF.Functions.Like(g.Title, tok) ||
                        (g.Description != null && EF.Functions.Like(g.Description, tok)) ||
                        (g.Platform != null && EF.Functions.Like(g.Platform, tok)) ||
                        (g.Genre != null && EF.Functions.Like(g.Genre, tok)) ||
                        (g.Category != null && EF.Functions.Like(g.Category, tok)));
                }
            }

            // Apply sorting
            query = (sort ?? "").ToLower() switch
            {
                "price-asc" => query.OrderBy(g => g.Price),
                "price-desc" => query.OrderByDescending(g => g.Price),
                "name-asc" => query.OrderBy(g => g.Title),
                "name-desc" => query.OrderByDescending(g => g.Title),
                _ => query.OrderBy(g => g.Id)
            };

            var results = await query.Take(200).ToListAsync(); // Limit to 200 results
            return View("Results", results);
        }
    }
}