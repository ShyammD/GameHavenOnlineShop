using Microsoft.AspNetCore.Http; // Provides access to session-related interfaces
using System.Text.Json; // Used for JSON serialisation and deserialisation

namespace GameHaven_Online_Shop.Helpers
{
    // Extension methods for storing and retrieving complex objects in session
    public static class SessionExtensions
    {
        // Save an object into session by converting it to JSON
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value)); // Serialize object to JSON and store as string
        }

        // Retrieve an object from session and deserialize it from JSON
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key); // Get JSON string from session
            return value == null ? default : JsonSerializer.Deserialize<T>(value); // Turn JSON back to object
        }
    }
}