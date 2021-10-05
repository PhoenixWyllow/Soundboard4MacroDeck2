using Soundboard4MacroDeck.Base;
using System.Text.Json;

namespace Soundboard4MacroDeck.Models
{
    internal class GlobalParameters : IOutputConfiguration 
    {
        public string OutputDeviceId { get; set; }
        public bool UseDefaultDevice { get; set; }

        public string Serialize() =>
            JsonSerializer.Serialize(this);

        public static GlobalParameters Deserialize(string configuration) =>
            ISerializableConfiguration.Deserialize<GlobalParameters>(configuration);
    }
}
