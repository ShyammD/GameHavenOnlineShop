// Namespace for the models
namespace GameHaven.Models.Models
{
    // Enum representing user roles
    public enum UserRole
    {
        Customer = 0,
        Admin = 1
    }

    // Model representing a user
    public class User
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // User's profile information
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        // User authentication information
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        // User's home address
        public string? HomeAddress { get; set; }

        // Role of the user (Customer or Admin)
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}