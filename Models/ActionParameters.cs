using System.Text.Json;

namespace Soundboard4MacroDeck.Models;

[Obsolete($"Use {nameof(ActionParametersV2)} instead. This is used only for migration purposes")]
public class ActionParameters : IOutputConfiguration
{
    public string FilePath { get; set; } = string.Empty;
    public byte[]? FileData { get; set; } = null;
    public string? FileExt { get; set; }
    public int Volume { get; set; } = 50;
    public bool UseDefaultDevice { get; set; } = true;
    public string? OutputDeviceId { get; set; }
    //public int Latency { get; set; } = 200;
    public bool SyncButtonState { get; set; } = true;

    public string FileName =>
        string.IsNullOrWhiteSpace(FileExt)
            ? Path.GetFileName(FilePath)
            : Path.GetFileNameWithoutExtension(FilePath) + FileExt[1..];

    public string Serialize() =>
        JsonSerializer.Serialize(this);

    public static ActionParameters Deserialize(string configuration) =>
        ISerializableConfiguration.Deserialize<ActionParameters>(configuration);
}