namespace Soundboard4MacroDeck.Models;

public interface IOutputConfiguration : ISerializableConfiguration
{
    string? OutputDeviceId { get; set; }
    bool UseDefaultDevice { get; set; }
}