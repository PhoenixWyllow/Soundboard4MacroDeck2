﻿using System.Text.Json;

namespace Soundboard4MacroDeck.Models;

public interface ISerializableConfiguration
{
    public string Serialize();

    protected static T Deserialize<T>(string configuration) where T : ISerializableConfiguration, new() =>
        !string.IsNullOrWhiteSpace(configuration) ? JsonSerializer.Deserialize<T>(configuration)! : new();
}