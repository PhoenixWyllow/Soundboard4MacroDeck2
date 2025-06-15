using System.Diagnostics;
using System.Text;

namespace Soundboard4MacroDeck.MimeSniffer;

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Add metadata into metadata list.
    /// </summary>
    /// <param name="list">The target list.</param>
    /// <param name="header">The metadata header to add.</param>
    public static void Add(this List<Metadata> list, Header header)
    {
        static void AddOffset(Metadata metadata, string[] byteStringArray, bool lastCharIsQuestionMark, int start, int i)
        {
            if (!lastCharIsQuestionMark)
            {
                int count = i - start;
                metadata.Offsets.Add(MakeOffset(byteStringArray, start, count));
            }
        }
        
        var metadata = new Metadata()
        {
            Extensions = header.Extensions.Split(',', ' ').ToList()
        };
        var hex = header.Hex;
        if (header.Offset > 0)
        {
            hex = Repeat("??", header.Offset, ',') + hex;
        }
        var byteStringArray = hex.Split(',', ' ');

        var lastCharIsQuestionMark = true;

        var start = 0;
        for (int i = 0; i <= byteStringArray.Length; i++)
        {
            if (i == byteStringArray.Length)
            {
                AddOffset(metadata, byteStringArray, lastCharIsQuestionMark, start, i);
            }
            else if (byteStringArray[i] == "??")
            {
                AddOffset(metadata, byteStringArray, lastCharIsQuestionMark, start, i);
                lastCharIsQuestionMark = true;
            }
            else
            {
                if (lastCharIsQuestionMark)
                {
                    start = i;
                }
                lastCharIsQuestionMark = false;
            }
        }

        list.Add(metadata);
    }

    /// <summary>
    /// Get byte array from string.
    /// </summary>
    /// <param name="source">byte format string.</param>
    /// <returns>result byte array.</returns>
    public static byte[] GetByte(this string source)
    {
        var array = source.Split(',', ' ');
        var byteArr = new byte[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            byteArr[i] = Convert.ToByte(array[i], 16);
        }

        return byteArr;
    }

    /// <summary>
    /// Match result from complex metadata.
    /// </summary>
    /// <param name="list">The complex metadata list.</param>
    /// <param name="data">Data to be match.</param>
    /// <param name="matchAll">Match all result or only first that sniffer matched.</param>
    /// <returns>Match result list.</returns>
    public static List<string> Match(this List<Metadata> list, byte[] data, bool matchAll = false)
    {
        List<string> extentionStore = new(4);
        foreach (var metatata in list)
        {
            if (metatata.Match(data))
            {
                extentionStore.AddRange(metatata.Extensions);
                if (!matchAll)
                {
                    break;
                }
            }
        }
        return extentionStore;
    }

    /// <summary>
    /// Populate matadata tree use record list.
    /// </summary>
    /// <param name="sniffer"></param>
    /// <param name="headers">Matadate record list.</param>
    public static void Populate(this Sniffer sniffer, IList<Header> headers)
    {
        foreach (var header in headers)
        {
            sniffer.Add(header);
        }
    }

    private static Offset MakeOffset(string[] byteStringArray, int start, int count)
    {
        return new()
        {
            Start = start,
            Count = count,
            Value = Encoding.ASCII.GetString(string.Join(",", byteStringArray, start, count).GetByte())
        };
    }

    [DebuggerStepThrough]
    private static string Repeat(string source, int count, char seprator)
    {
        var sb = new StringBuilder(count);
        for (int i = 0; i < count; i++)
        {
            sb.Append(source).Append(seprator);
        }

        return sb.ToString();
    }
}