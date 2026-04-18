// Using statements for data access, models, view models, MVC, and Entity Framework
using GameHaven.DataAccess;
using GameHaven.Models.Models;
using GameHaven.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for admin-only actions like managing games and customer orders
    public class AdminController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _db;

        // Constructor with dependency injection for the database context
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Check if the current user is the admin
        private bool IsAdmin()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            return email != null && email.ToLower() == "admin@gamehaven.com";
        }

        // Admin dashboard - list all games
        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var games = _db.Games.ToList();
            return View(games);
        }

        // View all customer orders
        public IActionResult CustomerOrders()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var orders = _db.Orders
                .Include(o => o.Items)
                .ToList();

            var users = _db.Users.ToList();

            // Map users to their orders
            var customerOrders = users.Select(u => new AdminCustomerOrdersViewModel
            {
                User = u,
                Orders = orders.Where(o => o.EmailAddress == u.Email).ToList()
            }).ToList();

            return View(customerOrders);
        }

        // View details of a specific order
        public IActionResult OrderDetails(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var order = _db.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // GET: Display create game form
        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Handle create game submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(game.Category))
                    game.Category = "Home Page Best Seller";

                _db.Games.Add(game);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Display edit game form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var game = _db.Games.Find(id);
            if (game == null) return NotFound();
            return View(game);
        }

        // POST: Handle edit game submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Game game)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            if (id != game.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _db.Update(game);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Display delete confirmation
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var game = _db.Games.Find(id);
            if (game == null) return NotFound();
            return View(game);
        }

        // POST: Handle deletion of a game
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var game = await _db.Games.FindAsync(id);
            if (game != null)
            {
                _db.Games.Remove(game);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}