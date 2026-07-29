using SuchByte.MacroDeck.Logging;

namespace Soundboard4MacroDeck;

internal static class PluginLogger
{
    public static void Error(string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Error(PluginInstance.Current, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Error(string source, Exception exception, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Error(PluginInstance.Current, exception, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Warning(string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Warning(PluginInstance.Current, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Warning(string source, Exception exception, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Warning(PluginInstance.Current, exception, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Information(string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Information(PluginInstance.Current, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Information(string source, Exception exception, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Information(PluginInstance.Current, exception, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Debug(string source, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Debug(PluginInstance.Current, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    public static void Debug(string source, Exception exception, string messageTemplate, params object[] propertyValues)
        => MacroDeckLogger.Debug(PluginInstance.Current, exception, "{Source}: " + messageTemplate, Prepend(source, propertyValues));

    private static object[] Prepend(object first, object[] propertyValues)
    {
        var values = new object[propertyValues.Length + 1];
        values[0] = first;
        Array.Copy(propertyValues, 0, values, 1, propertyValues.Length);
        return values;
    }
}
