// Using statements for MVC, EF Core, models, and data access
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameHaven.Models.Models;
using GameHaven.DataAccess;
using GameHaven_Online_Shop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for customer dashboard and account management
    public class CustomerController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for database context
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer dashboard with user info and past orders
        public async Task<IActionResult> Index()
        {
            // Get email from session
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            // Get user information
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound("User not found");

            // Get past orders for this user
            var orders = await _context.Orders
                .Where(o => o.EmailAddress == email)
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedUtc)
                .ToListAsync();

            // Prepare view model with user and orders
            var viewModel = new CustomerDashboardViewModel
            {
                User = user,
                Orders = orders
            };

            return View(viewModel);
        }

        // POST: Update account information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccount(CustomerDashboardViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Reload dashboard with current session data and errors
                var email = HttpContext.Session.GetString("UserEmail");
                var orders = await _context.Orders
                    .Where(o => o.EmailAddress == email)
                    .Include(o => o.Items)
                    .OrderByDescending(o => o.CreatedUtc)
                    .ToListAsync();

                vm.Orders = orders;
                return View("Index", vm);
            }

            // Find the user in the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == vm.User.Id);
            if (user == null)
                return NotFound("User not found");

            // Update user fields
            user.FirstName = vm.User.FirstName;
            user.LastName = vm.User.LastName;
            user.HomeAddress = vm.User.HomeAddress;

            await _context.SaveChangesAsync();

            // Update session values
            HttpContext.Session.SetString("UserFirstName", user.FirstName ?? "User");

            TempData["SuccessMessage"] = "Account information updated successfully.";

            return RedirectToAction("Index");
        }
    }
}