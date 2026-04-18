// Using statements for necessary namespaces
using GameHaven.Models.Models;
using System.Collections.Generic;

namespace GameHaven.Models.ViewModels
{
    // ViewModel for displaying a customer's orders in the admin panel
    public class AdminCustomerOrdersViewModel
    {
        // Property to hold the user information
        public User User { get; set; } = new User();

        // Property to hold a list of orders for the user
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}