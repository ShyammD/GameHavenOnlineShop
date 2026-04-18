namespace GameHaven_Online_Shop.Models
{
    // View model used for displaying error information to the user
    public class ErrorViewModel
    {
        // Unique identifier for the current request (used for tracking/debugging)
        public string? RequestId { get; set; }

        // Returns true if a RequestId exists — helps show/hide it in the error view
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}