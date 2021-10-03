using System.Text.Json;

namespace Soundboard4MacroDeck.Base
{
    public abstract class SerializableConfiguration<T> where T : new()
    {
        public abstract string Serialize();

        public static T Deserialize(string configuration) => 
            !string.IsNullOrWhiteSpace(configuration) ? JsonSerializer.Deserialize<T>(configuration) : new T();
    }
}