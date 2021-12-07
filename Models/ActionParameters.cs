using System;
using System.Text.Json;

namespace Soundboard4MacroDeck.Models
{
    public class ActionParameters : IOutputConfiguration
    {
        public string FilePath { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = null;
        public string FileExt { get; set; }
        public int Volume { get; set; } = 50;
        public bool UseDefaultDevice { get; set; } = true;
        public string OutputDeviceId { get; set; }
        public bool SyncButtonState { get; set; } = true;

        public string FileName =>
            string.IsNullOrWhiteSpace(FileExt)
            ? System.IO.Path.GetFileName(FilePath)
            : System.IO.Path.GetFileNameWithoutExtension(FilePath) + FileExt.Replace("*", string.Empty);

        public string Serialize() =>
            JsonSerializer.Serialize(this);

        public static ActionParameters Deserialize(string configuration) =>
            ISerializableConfiguration.Deserialize<ActionParameters>(configuration);
    }
}
