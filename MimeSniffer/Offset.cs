namespace Soundboard4MacroDeck.MimeSniffer;

/// <summary>
/// Representing a section of offset.
/// </summary>
public class Offset
{
    /// <summary>
    /// Gets or sets the count of data array.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the start position of data array.
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    /// Gets or sets the AscII string corresponding to the binary value of this data
    /// </summary>
    public string Value { get; set; }
}