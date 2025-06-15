using System.Text.Json.Serialization;
using System.Text.Json;

namespace Soundboard4MacroDeck.Models;
public sealed class ActionParametersV2 : IOutputConfiguration
{
    public string FileName { get; set; } = string.Empty;
    [JsonIgnore] public byte[]? FileData { get; set; } = null;
    public int AudioFileId { get; set; }
    public int AudioCategoryId { get; set; }
    public int Volume { get; set; } = 50;
    public bool UseDefaultDevice { get; set; } = true;
    public string? OutputDeviceId { get; set; }
    //public int Latency { get; set; } = 200;
    public bool SyncButtonState { get; set; } = true;

    public string Serialize() =>
        JsonSerializer.Serialize(this);

    public static ActionParametersV2 Deserialize(string configuration) =>
        ISerializableConfiguration.Deserialize<ActionParametersV2>(configuration);
}