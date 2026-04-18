// Namespace for the online shop models
namespace GameHaven_Online_Shop.Models
{
    // Model representing an item in the shopping basket
    public class BasketItem
    {
        // Unique identifier for the basket item
        public string Id { get; set; } = "";

        // Title or name of the product
        public string Title { get; set; } = "";

        // Price of the product
        public decimal Price { get; set; }

        // Image URL or path for the product
        public string Image { get; set; } = "";

        // Quantity of the product in the basket
        public int Quantity { get; set; } = 1;
    }
}