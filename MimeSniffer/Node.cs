﻿namespace Soundboard4MacroDeck.MimeSniffer;

/// <summary>
/// node
/// </summary>
public class Node
{
    /// <summary>
    /// Gets or sets children.
    /// </summary>
    public SortedList<byte, Node> Children { get; set; }

    /// <summary>
    /// Gets or sets depth.
    /// </summary>
    public int Depth { get; set; }

    /// <summary>
    /// Gets or sets extentions.
    /// </summary>
    public List<string> Extensions { get; set; }

    /// <summary>
    /// Gets or sets parent node.
    /// </summary>
    public Node Parent { get; set; }
}