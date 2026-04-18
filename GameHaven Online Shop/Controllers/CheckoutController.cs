// Using statements for data access, models, helpers, and MVC
using GameHaven.DataAccess;
using GameHaven.Models.Models;
using GameHaven_Online_Shop.Helpers;
using GameHaven_Online_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for handling checkout and order processing
    public class CheckoutController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _db;

        // Constructor with dependency injection for database context
        public CheckoutController(ApplicationDbContext db) => _db = db;

        // GET: Display checkout page
        [HttpGet]
        public IActionResult Index()
        {
            // Get basket from session
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            if (basket.Count == 0) return RedirectToAction("Basket", "Home");

            // Prepare view model, pre-filling email if logged in
            var email = HttpContext.Session.GetString("UserEmail");
            var vm = new CheckoutViewModel
            {
                IsLoggedIn = !string.IsNullOrEmpty(email),
                Email = email
            };
            return View(vm);
        }

        // POST: Handle checkout submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutViewModel vm)
        {
            // Get basket from session
            var basket = HttpContext.Session.GetObjectFromJson<List<BasketItem>>("Basket") ?? new List<BasketItem>();
            if (basket.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Your basket is empty.");
                return View(vm);
            }

            // Determine email to use
            var sessionEmail = HttpContext.Session.GetString("UserEmail");
            vm.IsLoggedIn = !string.IsNullOrEmpty(sessionEmail);
            var emailToUse = vm.IsLoggedIn ? sessionEmail! : vm.Email;

            // Validate model for logged-in users
            if (vm.IsLoggedIn)
            {
                vm.Email = sessionEmail;
                ModelState.Clear();
                TryValidateModel(vm);
            }

            if (!ModelState.IsValid)
                return View(vm);

            // Create order and add items from basket
            var order = new Order { EmailAddress = emailToUse! };

            foreach (var item in basket)
            {
                var idText = $"{item.Id}";
                var pid = int.TryParse(idText, out var parsed) ? parsed : 0;

                order.Items.Add(new OrderItem
                {
                    ProductId = pid,
                    ProductName = item.Title,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity
                });
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // Clear basket from session
            HttpContext.Session.Remove("Basket");

            // Redirect to success page
            return RedirectToAction(nameof(Success), new { id = order.Id.ToString() });
        }

        // GET: Display order success page
        [HttpGet]
        public async Task<IActionResult> Success(string id)
        {
            if (!int.TryParse(id, out var orderId))
                return RedirectToAction("Index", "Home");

            var order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return RedirectToAction("Index", "Home");
            return View(order);
        }
    }
}