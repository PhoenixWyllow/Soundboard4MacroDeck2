using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck.Views.Common;

/// <summary>
/// Helper class to create reusable logging callbacks for toolbar operations.
/// Reduces boilerplate when logging CRUD operations in views.
/// </summary>
internal sealed class OperationLogger
{
    private readonly MacroDeckPlugin _plugin;
    private readonly Type _logSource;

    /// <summary>
    /// Initializes a new instance of OperationLogger.
    /// </summary>
    /// <param name="plugin">The plugin instance for logging.</param>
    /// <param name="logSource">The type to use as the logging source.</param>
    public OperationLogger(MacroDeckPlugin plugin, Type logSource)
    {
        ArgumentNullException.ThrowIfNull(plugin);
        ArgumentNullException.ThrowIfNull(logSource);

        _plugin = plugin;
        _logSource = logSource;
    }

    /// <summary>
    /// Creates a logging callback for informational messages.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>An action that logs the message when invoked.</returns>
    public Action<string> Info(string? message = null)
        => msg => MacroDeckLogger.Info(_plugin, _logSource, message ?? msg);

    /// <summary>
    /// Creates a logging callback for error messages.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>An action that logs the error when invoked.</returns>
    public Action<string> Error(string? message = null)
        => msg => MacroDeckLogger.Error(_plugin, _logSource, message ?? msg);

    /// <summary>
    /// Creates a logging callback for warning messages.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>An action that logs the warning when invoked.</returns>
    public Action<string> Warning(string? message = null)
        => msg => MacroDeckLogger.Warning(_plugin, _logSource, message ?? msg);

    /// <summary>
    /// Creates a logging callback for trace messages.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>An action that logs the trace when invoked.</returns>
    public Action<string> Trace(string? message = null)
        => msg => MacroDeckLogger.Trace(_plugin, _logSource, message ?? msg);
}
