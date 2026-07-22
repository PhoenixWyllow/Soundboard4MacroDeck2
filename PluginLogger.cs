using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck;

internal static class PluginLogger
{
    public static void Information(string source, string messageTemplate, params object[] propertyValues)
        => Information(PluginInstance.Current, source, messageTemplate, propertyValues);

    public static void Information(MacroDeckPlugin plugin, string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Information(plugin, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Warning(string source, string messageTemplate, params object[] propertyValues)
        => Warning(PluginInstance.Current, source, messageTemplate, propertyValues);

    public static void Warning(MacroDeckPlugin plugin, string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Warning(plugin, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Error(string source, string messageTemplate, params object[] propertyValues)
        => Error(PluginInstance.Current, source, messageTemplate, propertyValues);

    public static void Error(MacroDeckPlugin plugin, string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Error(plugin, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Debug(string source, string messageTemplate, params object[] propertyValues)
        => Debug(PluginInstance.Current, source, messageTemplate, propertyValues);

    public static void Debug(MacroDeckPlugin plugin, string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Debug(plugin, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void DebugException(Exception exception)
        => MacroDeckLogger.Debug(exception, "{Message}", exception.Message);

    private static object[] Prepend(object first, object[] propertyValues)
    {
        var values = new object[propertyValues.Length + 1];
        values[0] = first;
        Array.Copy(propertyValues, 0, values, 1, propertyValues.Length);
        return values;
    }
}
