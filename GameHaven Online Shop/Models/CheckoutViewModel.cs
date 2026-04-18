using System.ComponentModel.DataAnnotations; // Provides validation attributes like [Required], [EmailAddress], etc.
using System.Text.RegularExpressions; // Enables use of regex for input format validation

namespace GameHaven_Online_Shop.Models
{
    // Represents checkout form data and includes validation logic
    public class CheckoutViewModel : IValidatableObject
    {
        // Email field (only shown when user is not logged in)
        [EmailAddress, Display(Name = "Email")]
        public string? Email { get; set; }

        // Credit card number (required)
        [Required, Display(Name = "Card number")]
        public string CardNumber { get; set; } = "";

        // Card expiry date in MM/YY format (required)
        [Required, Display(Name = "Expiry (MM/YY)")]
        public string Expiry { get; set; } = "";

        // CVV security code (required)
        [Required, Display(Name = "CVV")]
        public string CVV { get; set; } = "";

        // Used to determine if email input is needed
        public bool IsLoggedIn { get; set; }

        // Custom validation logic for all fields
        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            // Require email only if user is not logged in
            if (!IsLoggedIn && string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult("Email is required.", new[] { nameof(Email) });

            // Ensure card number contains only digits and passes Luhn algorithm
            var digits = new string(CardNumber.Where(char.IsDigit).ToArray());
            if (digits.Length < 13 || digits.Length > 19 || !Luhn(digits))
                yield return new ValidationResult("Enter a valid card number.", new[] { nameof(CardNumber) });

            // Validate expiry format (MM/YY)
            if (!Regex.IsMatch(Expiry ?? "", @"^\d{2}/\d{2}$"))
                yield return new ValidationResult("Use MM/YY.", new[] { nameof(Expiry) });
            else
            {
                // Extract month and year from expiry string
                var mm = int.Parse(Expiry.Substring(0, 2));
                var yy = int.Parse(Expiry.Substring(3, 2));

                // Ensure valid month value
                if (mm < 1 || mm > 12)
                    yield return new ValidationResult("Invalid month.", new[] { nameof(Expiry) });

                // Build a full expiry date and ensure card isn't expired
                var year = 2000 + yy;
                var lastDay = DateTime.DaysInMonth(year, Math.Clamp(mm, 1, 12));
                var endOfMonth = new DateTime(year, mm, lastDay, 23, 59, 59);
                if (endOfMonth < DateTime.Now)
                    yield return new ValidationResult("Card is expired.", new[] { nameof(Expiry) });
            }

            // Ensure CVV is 3–4 digits
            if (!Regex.IsMatch(CVV ?? "", @"^\d{3,4}$"))
                yield return new ValidationResult("Enter a valid CVV.", new[] { nameof(CVV) });
        }

        // Implements the Luhn algorithm to validate credit card numbers
        private static bool Luhn(string n)
        {
            int sum = 0;
            bool alt = false; // Tracks whether to double the current digit

            // Process digits from right to left
            for (int i = n.Length - 1; i >= 0; i--)
            {
                int d = n[i] - '0'; // Convert char to integer
                if (alt)
                {
                    d *= 2;
                    if (d > 9) d -= 9; // Subtract 9 if doubling exceeds 9
                }
                sum += d;
                alt = !alt; // Flip doubling flag for next iteration
            }

            return sum % 10 == 0; // Valid if divisible by 10
        }
    }
}