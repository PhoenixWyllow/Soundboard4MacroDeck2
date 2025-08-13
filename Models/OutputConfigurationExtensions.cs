namespace Soundboard4MacroDeck.Models;

public static class OutputConfigurationExtensions
{
    public static bool MustGetDefaultDevice(this IOutputConfiguration configuration)
    {
        return configuration.UseDefaultDevice || string.IsNullOrWhiteSpace(configuration.OutputDeviceId);
    }
    public static bool MustGetUniqueRandomSound(this IOutputConfiguration configuration)
    {
        return configuration.UniqueRandomSound;
    }
}