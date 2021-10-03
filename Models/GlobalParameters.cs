using Soundboard4MacroDeck.Base;
using System.Text.Json;

namespace Soundboard4MacroDeck.Models
{
    internal class GlobalParameters : SerializableConfiguration<GlobalParameters>
    {
        public string OutputDeviceId { get; set; }

        public override string Serialize() =>
            JsonSerializer.Serialize(this);
    }
}
