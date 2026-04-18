// Using statements for cryptography and text encoding
using System.Security.Cryptography;
using System.Text;

namespace GameHaven.Utility
{
    // Utility class for simple password hashing and verification
    public static class SimplePassword
    {
        // Constant salt value for hashing
        private const string Salt = "gamehaven_v1_salt";

        // Hashes a password with the salt using SHA256
        public static string Hash(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(Salt + password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        // Verifies a password against a given hash
        public static bool Verify(string password, string hashHex)
            => Hash(password).Equals(hashHex, StringComparison.OrdinalIgnoreCase);
    }
}