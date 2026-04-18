// Using statements for data access, models, utilities, identity, and MVC
using GameHaven.DataAccess;
using GameHaven.Models.Models;
using GameHaven.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHaven_Online_Shop.Controllers
{
    // Controller for handling user account actions like login, register, and logout
    public class AccountController : Controller
    {
        // Database context for accessing users
        private readonly ApplicationDbContext _context;

        // Password hasher for hashing and verifying passwords
        private readonly PasswordHasher<User> _hasher = new();

        // Constructor with dependency injection for the database context
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Login

        // GET: Display login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle login submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            ViewData["Email"] = email;

            // Find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View();
            }

            // Check if it's the seeded admin
            bool isSeededAdmin = email.Equals("admin@gamehaven.com", StringComparison.OrdinalIgnoreCase);

            // Verify the password
            bool isPasswordValid = isSeededAdmin
                ? SimplePassword.Verify(password, user.PasswordHash)
                : _hasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;

            if (!isPasswordValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View();
            }

            // Store session values
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
            HttpContext.Session.SetString("UserFirstName", user.FirstName ?? "User");

            // Redirect user based on their role
            return user.Role == UserRole.Admin
                ? RedirectToAction("Index", "Admin")
                : RedirectToAction("Index", "Customer");
        }

        // Register

        // POST: Handle registration submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, string password)
        {
            if (!ModelState.IsValid)
                return View(user);

            // Exclude seeded admin
            bool isSeededAdmin = user.Email.Equals("admin@gamehaven.com", StringComparison.OrdinalIgnoreCase);

            // Check if user already exists
            bool exists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (exists)
            {
                ModelState.AddModelError(nameof(user.Email), "Email already registered.");
                return View(user);
            }

            // Apply password validation only for non-admin and new users
            if (!isSeededAdmin)
            {
                var passwordErrors = ValidatePassword(password);
                if (passwordErrors.Any())
                {
                    foreach (var error in passwordErrors)
                        ModelState.AddModelError(nameof(password), error);
                    return View(user);
                }
            }

            // Assign role based on email domain
            user.Role = user.Email.EndsWith("@gamehaven.com", StringComparison.OrdinalIgnoreCase)
                ? UserRole.Admin
                : UserRole.Customer;

            // Hash the password
            user.PasswordHash = _hasher.HashPassword(user, password);

            // Add user to database and save changes
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Store session values
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
            HttpContext.Session.SetString("UserFirstName", user.FirstName ?? "User");

            // Redirect based on role
            return user.Role == UserRole.Admin
                ? RedirectToAction("Index", "Admin")
                : RedirectToAction("Index", "Customer");
        }

        // Validate password according to rules
        private List<string> ValidatePassword(string password)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                errors.Add("Password must be at least 8 characters long.");
            if (!password.Any(char.IsUpper))
                errors.Add("Password must contain at least one uppercase letter.");
            if (!password.Any(char.IsLower))
                errors.Add("Password must contain at least one lowercase letter.");
            if (!password.Any(char.IsDigit))
                errors.Add("Password must contain at least one number.");
            if (!password.Any(ch => "!@#$%^&*()_-+=<>?".Contains(ch)))
                errors.Add("Password must contain at least one special character (!@#$%^&*()_-+=<>?).");

            return errors;
        }

        // Logout

        // Logout the user and clear session
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}