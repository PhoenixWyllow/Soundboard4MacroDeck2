using Soundboard4MacroDeck.Base;
using System.Text.Json;

namespace Soundboard4MacroDeck.Models
{
    public enum SoundboardActions { None, Play, Overlap };

    public class ActionParameters : SerializableConfiguration<ActionParameters>
    {
        public string FilePath { get; set; }
        public byte[] FileData { get; set; } = null;
        public int Volume { get; set; } = 50;
        public SoundboardActions ActionType { get; set; }
        public string FileExt { get; set; }
        public string FileName => FilePath + FileExt;

        public override string Serialize() =>
            JsonSerializer.Serialize(this);
    }
}
