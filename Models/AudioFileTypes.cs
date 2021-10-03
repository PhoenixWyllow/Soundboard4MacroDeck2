using Myrmec;
using System.Collections.Generic;

namespace Soundboard4MacroDeck.Models
{
    internal class AudioFileTypes
    {
        public static string[] Extensions { get; } = {
            "*.aif", "*.aiff", "*.mid", "*.midi", "*.m4a", "*.mp3", "*.ogg", "*.oga", "*.aac", "*.flac", "*.wma", "*.wav", "*.weba",
            };

        public static List<Record> Records => new List<Record>
        {
            new Record("aif aiff", "46 4F 52 4D ?? ?? ?? ?? 41 49 46 46"),
            new Record("mid midi", "4D 54 68 64"),
            new Record("m4a", "66 74 79 70 4D 34 41 20", 4),
            new Record("mp3", "FF FB"),
            new Record("mp3", "FF F3"),
            new Record("mp3", "FF F2"),
            new Record("mp3", "49 44 33", "MP3 file with an ID3v2 container"),
            new Record("ogg oga", "4F 67 67 53"),
            new Record("aac", "FF F1", "MPEG-4 Advanced Audio Coding (AAC) Low Complexity (LC) audio file"),
            new Record("flac", "66 4C 61 43"),
            new Record("wma", "30 26 B2 75 8E 66 CF 11 A6 D9 00 AA 00 62 CE 6C"),
            new Record("wav", "52 49 46 46 ?? ?? ?? ?? 57 41 56 45"),
        };
    }
}
