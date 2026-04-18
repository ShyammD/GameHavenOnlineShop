// Using statements for data annotations
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameHaven.Models.Models
{
    // Model representing a game in the system
    public class Game
    {
        // Primary key for the Game
        [Key]
        public int Id { get; set; }

        // Title of the game
        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        // Price of the game with decimal precision
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // URL or path to the game's image
        [MaxLength(255)]
        public string? ImageUrl { get; set; }

        // Category of the game
        [MaxLength(50)]
        public string? Category { get; set; } = "General";

        // URL-friendly identifier for the game
        [Required, MaxLength(150)]
        public string Slug { get; set; } = string.Empty;

        // Description of the game
        [MaxLength(500)]
        public string? Description { get; set; }

        // Platform for the game (e.g., PC, Xbox, PlayStation)
        [MaxLength(100)]
        public string? Platform { get; set; }

        // Release date of the game
        [MaxLength(50)]
        public string? ReleaseDate { get; set; }

        // Genre of the game
        [MaxLength(100)]
        public string? Genre { get; set; }
    }
}