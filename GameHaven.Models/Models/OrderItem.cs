// Namespace for the models
namespace GameHaven.Models.Models
{
    // Model representing an individual item in an order
    public class OrderItem
    {
        // Unique identifier for the order item
        public int Id { get; set; }

        // Foreign key to the related order
        public int OrderId { get; set; }

        // Navigation property for the related order
        public Order Order { get; set; } = default!;

        // Identifier for the product
        public int ProductId { get; set; }

        // Name of the product
        public string ProductName { get; set; } = "";

        // Price per unit of the product
        public decimal UnitPrice { get; set; }

        // Quantity of the product in the order
        public int Quantity { get; set; }
    }
}