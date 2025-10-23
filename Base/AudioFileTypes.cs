﻿using Microsoft.VisualBasic.Devices;

using Soundboard4MacroDeck.MimeSniffer;
using Soundboard4MacroDeck.Services;

using SuchByte.MacroDeck.Logging;

using System;

namespace Soundboard4MacroDeck.Base;

internal class AudioFileTypes
{
    // You can get the file extention name detail in this wikipedia page. https://en.wikipedia.org/wiki/List_of_file_signatures
    // For even more magic number details, you can use this link https://www.garykessler.net/library/file_sigs.html


    public static string[] Extensions { get; } = [
        "*.aif", "*.aiff", "*.mid", "*.midi", "*.m4a", "*.mp3", "*.ogg", "*.oga", "*.aac", "*.flac", "*.wma", "*.wav", "*.weba",
    ];

    public static List<Header> Headers => [
        new("aif aiff", "46 4F 52 4D ?? ?? ?? ?? 41 49 46 46"),
        new("mid midi", "4D 54 68 64"),
        new("m4a", 4, "66 74 79 70 4D 34 41 20", "Apple Lossless Audio Codec file"),
        new("m4a", "00 00 00 20 66 74 79 70 4D 34 41", "Apple audio and video"),
        new("mp3", "FF FB"),
        new("mp3", "FF F3"),
        new("mp3", "FF F2"),
        new("mp3", "49 44 33", "MP3 file with an ID3v2 container"),
        new("ogg oga", "4F 67 67 53"),
        new("aac", "FF F1", "MPEG-4 Advanced Audio Coding (AAC) Low Complexity (LC) audio file"),
        new("flac", "66 4C 61 43"),
        new("wma", "30 26 B2 75 8E 66 CF 11 A6 D9 00 AA 00 62 CE 6C"),
        new("wav", "52 49 46 46 ?? ?? ?? ?? 57 41 56 45"),
        new("wav", 8, "57 41 56 45 66 6D 74 20"),

    ];

    public static List<Header> IncorrectHeaders => [
        new("m4v mp4", "00 00 00 18 66 74 79 70", "MPEG-4 video"),
    ];

    public static Sniffer MimeSniffer => GetMimeSniffer();
    private static Sniffer GetMimeSniffer()
    {
        Sniffer sniffer = new();
        sniffer.Populate(Headers);
        sniffer.Populate(IncorrectHeaders);
        return sniffer;
    }

    public static bool IsValidFile(byte[] data, out string extension)
    {
        byte[] fileHead = new byte[100];

        Array.Copy(data, fileHead, fileHead.Length);

        var matches = MimeSniffer.Match(fileHead);
        if (matches.Count > 0)
        {
            extension = matches[0];
            return true;
        }
        extension = string.Empty;

        MacroDeckLogger.Warning(PluginInstance.Current, typeof(AudioFileTypes), LocalizationManager.Instance.ActionPlaySoundInvalidFile);
        MacroDeckLogger.Info(PluginInstance.Current, typeof(AudioFileTypes), BitConverter.ToString(fileHead[..20])+"[...]");
        return false;
    }
}