namespace Soundboard4MacroDeck.MimeSniffer;

/// <summary>
/// sniffer
/// </summary>
public class Sniffer
{
    private readonly Node _root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Sniffer"/> class.
    /// </summary>
    public Sniffer()
    {
        _root = new()
        {
            Children = new(128),
            Depth = -1,
        };
        ComplexMetadata = new(10);
    }

    /// <summary>
    /// Gets or sets ComplexMetadata.
    /// </summary>
    public List<Metadata> ComplexMetadata { get; set; }

    /// <summary>
    /// Add a header to metadata tree.
    /// </summary>
    /// <param name="data">file head.</param>
    /// <param name="extensions">file extension list.</param>
    public void Add(byte[] data, string[] extensions)
    {
        Add(data, _root, extensions, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="header"></param>
    public void Add(Header header)
    {
        if (header.IsComplexMetadata)
        {
            ComplexMetadata.Add(header);
        }
        else
        {
            Add(header.Hex.GetByte(), header.ExtensionsArray);
        }
    }

    /// <summary>
    /// Find extensions that match the file hex head.
    /// </summary>
    /// <param name="data">file hex head</param>
    /// <param name="matchAll">match all result or only the first.</param>
    /// <returns>matched result</returns>
    public List<string> Match(byte[] data, bool matchAll = false)
    {
        List<string> extensionStore = new(4);
        Match(data, 0, _root, extensionStore, matchAll);

        if (matchAll || extensionStore.Count == 0)
        {
            // Match data from complex metadata.
            extensionStore.AddRange(ComplexMetadata.Match(data, matchAll));
        }

        // Remove repeated extensions.
        if (matchAll && extensionStore.Count != 0)
        {
            extensionStore = extensionStore.Distinct().ToList();
        }

        return extensionStore;
    }

    private void Add(byte[] data, Node parent, string[] extensions, int depth)
    {
        if (parent.Children == null)
        {
            parent.Children = new(Convert.ToInt32(128 / Math.Pow(2, depth)));
        }

        Node current;
        // if not contains current byte index, create node and put it into children.
        if (!parent.Children.ContainsKey(data[depth]))
        {
            current = new()
            {
                Depth = depth,
                Parent = parent
            };
            parent.Children.Add(data[depth], current);
        }
        else
        {
            if (!parent.Children.TryGetValue(data[depth], out current))
            {
                throw new("Something really messed up...");
            }

        }

        // last byte, put extensions into Extensions.
        if (depth == (data.Length - 1))
        {
            current.Extensions ??= new(4);

            current.Extensions.AddRange(extensions);
            return;
        }

        Add(data, current, extensions, depth + 1);
    }

    private void Match(byte[] data, int depth, Node node, List<string> extensionStore, bool matchAll)
    {
        // if depth out of data.Length's index then data end.
        if (data.Length == depth)
        {
            return;
        }

        node.Children.TryGetValue(data[depth], out Node? current);

        // can't find matched node, match ended.
        if (current is null)
        {
            return;
        }

        // now extensions not null, this node is a final node and this is a result.
        if (current.Extensions is not null)
        {
            extensionStore.AddRange(current.Extensions);

            // if only match first matched.
            if (!matchAll)
            {
                return;
            }
        }

        // children is null, match ended.
        if (current.Children == null)
        {
            return;
        }

        // children not null, keep match.
        Match(data, depth + 1, current, extensionStore, matchAll);
    }
}