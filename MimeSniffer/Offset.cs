namespace Soundboard4MacroDeck.MimeSniffer;



/// <summary>
/// Representing a section of offset.
/// </summary>
/// <param name="Count"> Gets or sets the count of data array. </param>
/// <param name="Start"> Gets or sets the start position of data array. </param>
/// <param name="Value"> Gets or sets the AscII string corresponding to the binary value of this data </param>
public record Offset(int Count, int Start, string Value);
