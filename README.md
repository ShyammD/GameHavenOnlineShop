# GameHaven Online Shop

## Project Overview:
The **GameHaven Online Shop** is a modern e-commerce web application developed using **ASP.NET Core MVC (Razor Views)**, targeting **.NET 8** and **C# 12**.  

The platform enables users to browse video games, consoles, and accessories by platform, manage a shopping basket, and complete purchases through a structured checkout process. It also includes an admin dashboard for managing customers and orders.  

The system is designed with a focus on **clean architecture, maintainability, and a responsive user experience**, making it a strong demonstration of full-stack .NET development.

## Features
- **User Registration & Secure Login**: Users can create accounts and authenticate securely, with password handling centralised in `GameHaven.Utility\SimplePassword.cs`.
- **Product Catalogue**: Browse games, consoles, and accessories organised by platform (Xbox, PlayStation, Nintendo, Computer).
- **Product Details**: View detailed product information and add items to the shopping basket.
- **Shopping Basket & Checkout**: Session-based basket management using helper extensions, with a complete checkout workflow that generates orders and order items.
- **Order Management**: Admin users can create, update, and delete orders; customers can view their order history.
- **Admin Dashboard**: Manage customers, orders, and view detailed order information.
- **Search & Filtering**: Search functionality for locating products efficiently.
- **Access Control**: Role-based authorisation (admin vs. customer) to protect routes and actions.
- **Responsive UI**: Built using Bootstrap and custom CSS for a consistent, mobile-friendly interface.


## Technologies Used

- **ASP.NET Core MVC (Razor Views)** - Server-side rendering with controllers and views  
- **.NET 8**  
- **C# 12**  
- **Entity Framework Core** - ORM for database access, migrations, and seeding  
- **SQL Server / LocalDB** - Configurable relational database  
- **HTML5 / CSS3 / JavaScript** - Frontend structure and interactivity  
- **Bootstrap** - Responsive UI framework  
- **Visual Studio 2022** or **.NET CLI** - Development and debugging  
- **Session Management** - Custom extensions for handling basket data  
- **Custom Utilities** - Password hashing and verification logic  


## How to Run the Project

To get started with **GameHaven Online Shop**, follow these steps:

### 1. Clone the repository
```bash
git clone https://github.com/ShyammD/GameHavenOnlineShop
```

### 2. Open the solution
- Open `GameHaven.sln` in **Visual Studio 2022**

### 3. Configure the database connection
- Open `appsettings.json`
- Update the `DefaultConnection` string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GameHavenDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Restore NuGet packages
- Right-click the solution → **Restore NuGet Packages**  
- Or via CLI:
```bash
dotnet restore
```

### 5. Apply Entity Framework migrations
- Using Visual Studio (**Package Manager Console**):
```powershell
Update-Database
```

- Or via CLI:
```bash
dotnet ef database update
```

> Migrations are located in `GameHaven.DataAccess\Migrations` and may include seeded data (e.g., admin accounts).

### 6. Build and run the application
- Press `Ctrl + F5` in Visual Studio to run with IIS Express  
- Or via CLI:
```bash
dotnet build
dotnet run --project "GameHaven Online Shop"
```

> You can now register, browse products, add items to your basket, complete checkout, and access admin features (if authorised).


## Folder Structure
```
GameHaven Online Shop
│
├── GameHaven Online Shop          # Web project (startup)
│   ├── Controllers
│   │   ├── AccountController.cs
│   │   ├── HomeController.cs
│   │   ├── AdminController.cs
│   │   ├── CheckoutController.cs
│   │   └── Platform Controllers (Xbox, PlayStation, Nintendo, Computer, Search, Customer)
│   │
│   ├── Views
│   │   ├── Home
│   │   ├── Account
│   │   ├── Admin
│   │   ├── Checkout
│   │   └── Shared
│   │
│   ├── wwwroot
│   │   ├── css/site.css
│   │   └── js/passwordStrength.js
│   │
│   ├── Helpers
│   │   └── SessionExtensions.cs
│   │
│   ├── appsettings.json
│   └── Program.cs
│
├── GameHaven.Models              # Domain models & view models
│   └── Models
│       ├── User.cs
│       ├── Game.cs
│       ├── Order.cs
│       ├── OrderItem.cs
│       └── ViewModels
│
├── GameHaven.DataAccess          # EF Core DbContext & migrations
│   ├── Data
│   │   └── ApplicationDbContext.cs
│   └── Migrations
│
├── GameHaven.Utility             # Utilities (password hashing, etc.)
│   └── SimplePassword.cs
│
├── Project Report                # Full system documentation
│   └── Design and Development of the GameHaven Online Shop - An ASP.NET Core MVC E-Commerce System.pdf
│
├── LICENSE
├── README.md
└── GameHaven.sln
```


## Explanation of Key Folders and Files

### Controllers
- **AccountController.cs**: Handles user registration, login, and logout.
- **HomeController.cs**: Displays homepage and product listings.
- **CheckoutController.cs**: Manages basket and checkout workflow.
- **AdminController.cs**: Admin-only features such as order and customer management.
- **Platform Controllers**: Handle category-specific product listings.


### Views
- Razor views organised per controller (`Home`, `Account`, `Admin`, etc.)
- **Shared/_Layout.cshtml**: Main layout used across the application
- **Shared/_AuthModal.cshtml**: Reusable authentication modal


### GameHaven.Models
- Core domain models (`User`, `Game`, `Order`, `OrderItem`)
- ViewModels used for UI and admin dashboards


### GameHaven.DataAccess
- **ApplicationDbContext.cs**: Defines database structure using `DbSet<>`
- **Migrations/**: Tracks schema changes and seed data


### GameHaven.Utility
- **SimplePassword.cs**: Centralised password hashing and verification logic


### Helpers
- **SessionExtensions.cs**: Enables storing complex objects (e.g., basket items) in session


### Project Report
- **Design and Development of the GameHaven Online Shop - An ASP.NET Core MVC E-Commerce System.pdf**: Comprehensive documentation of the system, including:
  - System architecture (MVC and N-Tier design)
  - Design decisions and technology choices
  - Security, usability, and scalability considerations
  - Entity Relationship Diagrams (ERD) and class diagrams
  - Wireframes and final UI screenshots
  - Reflection, challenges faced, and future improvements
  - Individual contribution and teamwork analysis

> This report provides in-depth insight into the design, development, and evaluation of the system.


### wwwroot
- Static frontend assets including CSS, JavaScript, and images


### appsettings.json
- Stores application configuration, including database connection strings


## Contributing
Contributions are welcome.

If you would like to improve the project:
- Open an issue describing your idea or bug
- Submit a pull request with clear, focused commits
- Ensure consistency with the existing codebase and include migrations where necessary


## License

This project is licensed under the MIT License.

See the LICENSE file for details.


## Final Note
This project demonstrates:
- Full-stack **ASP.NET Core MVC development**
- **Entity Framework Core** integration
- **Session-based state management**
- **Role-based access control**
- Clean project structure and separation of concerns
- Strong documentation through a detailed technical report
