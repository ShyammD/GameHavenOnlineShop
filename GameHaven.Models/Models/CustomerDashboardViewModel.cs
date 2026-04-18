// Using statements for necessary namespaces
using System.Collections.Generic;
using GameHaven.Models.Models;

namespace GameHaven_Online_Shop.Models
{
    // ViewModel for displaying customer dashboard information
    public class CustomerDashboardViewModel
    {
        // Property to hold the user information
        public User User { get; set; }

        // Property to hold a list of orders for the customer
        public List<Order> Orders { get; set; } = new();
    }
}