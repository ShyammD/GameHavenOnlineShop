using GameHaven.DataAccess; // Import data access layer
using Microsoft.EntityFrameworkCore; // Import EF Core

var builder = WebApplication.CreateBuilder(args); // Create web application builder

// Add services to the container.
builder.Services.AddControllersWithViews(); // Add MVC controllers with views

// Register EF Core DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Configure SQL Server connection

// Session support
builder.Services.AddDistributedMemoryCache(); // In-memory cache for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout 30 minutes
    options.Cookie.HttpOnly = true; // Cookie is HTTP only
    options.Cookie.IsEssential = true; // Mark cookie as essential
});

var app = builder.Build(); // Build the application

using (var scope = app.Services.CreateScope()) // Create scope for services
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // Get DbContext
    db.Database.Migrate(); // Creates DB and applies migrations
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Use error handler in production
}

app.UseStaticFiles(); // Serve static files

app.UseRouting(); // Enable routing

app.UseAuthorization(); // Enable authorization

// Enable Session
app.UseSession(); // Enable session middleware

app.MapControllerRoute(
    name: "default", // Default route
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Route pattern

app.Run(); // Run the application