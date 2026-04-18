// Namespace for the models
namespace GameHaven.Models.Models
{
    // Model representing a customer's order
    public class Order
    {
        // Unique identifier for the order
        public int Id { get; set; }

        // Email address of the person who placed the order
        public string EmailAddress { get; set; } = "";

        // Timestamp when the order was created 
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        // Collection of items included in the order
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        // Total cost of the order calculated from items
        public decimal Total => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}