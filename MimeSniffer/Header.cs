namespace Soundboard4MacroDeck.MimeSniffer;

/// <summary>
/// Present one record.
/// </summary>
public class Header
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Header"/> class.
    /// </summary>
    /// <param name="extensions">extensions string ,split with "," what if it has many.</param>
    /// <param name="hex">hex string, split with ",".</param>
    public Header(string extensions, string hex) : this(extensions, 0, hex, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Header"/> class.
    /// </summary>
    /// <param name="extensions">extensions format string.</param>
    /// <param name="offset">Offset of this record.</param>
    /// <param name="hex">File hex head format string.</param>
    public Header(string extensions, int offset, string hex) : this(extensions, offset, hex, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Header"/> class.
    /// </summary>
    /// <param name="extensions">extensions string ,split with "," what if it has many.</param>
    /// <param name="hex">hex string, split with ",".</param>
    /// <param name="description">description</param>
    public Header(string extensions, string hex, string description) : this(extensions, 0, hex, description) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Header"/> class.
    /// </summary>
    /// <param name="extensions">extensions string ,split with "," what if it has many.</param>
    /// <param name="offset"></param>
    /// <param name="hex">hex string, split with ",".</param>
    /// <param name="description">description</param>
    public Header(string extensions, int offset, string hex, string description)
    {
        Offset = offset;
        Extensions = extensions;
        Hex = hex;
        Description = description;
    }

    /// <summary>
    /// Gets or sets Description
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets or sets file extensions.
    /// </summary>
    public string Extensions { get; private set; }

    /// <summary>
    /// Gets or sets Hex String.
    /// </summary>
    public string Hex { get; private set; }

    /// <summary>
    /// Gets or sets offset
    /// </summary>
    public int Offset { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this record has offset or contain a variable byte or not.
    /// </summary>
    public bool IsComplexMetadata
    {
        get => (Offset > 0) || (Hex.Contains("?"));
    }

}