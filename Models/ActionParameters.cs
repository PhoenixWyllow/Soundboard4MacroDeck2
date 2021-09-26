using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MacroDeckSoundboard.Models
{
    public enum SoundboardActions { Play, Overlap };

    internal class ActionParameters
    {
        public byte[] FileData { get; set; } = null;
        public int Volume { get; set; } = 50;
        public SoundboardActions ActionType { get; set; }

        public string Serialize() => 
            JsonSerializer.Serialize(this);

        public static ActionParameters Deserialize(string configuration) => 
            !string.IsNullOrWhiteSpace(configuration) ? JsonSerializer.Deserialize<ActionParameters>(configuration) : new ActionParameters();
    }
}
