using Soundboard4MacroDeck.Base;
using System.Text.Json;

namespace Soundboard4MacroDeck.Models
{
    public enum SoundboardActions { None, Play, Overlap };

    public class ActionParameters : IOutputConfiguration
    {
        public string FilePath { get; set; }
        public byte[] FileData { get; set; } = null;
        public int Volume { get; set; } = 50;
        public string FileExt { get; set; }
        public string FileName => FilePath + FileExt;
        public string OutputDeviceId { get; set; }
        public bool UseDefaultDevice { get; set; }

        public string Serialize() =>
            JsonSerializer.Serialize(this);

        public static ActionParameters Deserialize(string configuration) =>
            ISerializableConfiguration.Deserialize<ActionParameters>(configuration);
    }
}
