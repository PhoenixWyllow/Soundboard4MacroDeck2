using SQLite;

namespace Soundboard4MacroDeck.Models;

public sealed class AudioFile
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[] Data { get; set; } = [];

    [Indexed]
    public int CategoryId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }

    public override bool Equals(object? obj)
    {
        return obj is AudioFile audioFile && Equals(audioFile);
    }

    public bool Equals(AudioFile audioFile)
    {
        return Id == audioFile.Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public AudioFileItem ToAudioFileItem()
    {
        return new() { Id = Id, Name = Name, CategoryId = CategoryId };
    }
}

public sealed class AudioCategory
{
    public static readonly AudioCategory NoneOrUncategorized = new() { Id = 0, Name = "(Uncategorized)" };

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }

    public override bool Equals(object? obj)
    {
        return obj is AudioCategory audioCategory && Equals(audioCategory);
    }

    public bool Equals(AudioCategory audioCategory)
    {
        return Id == audioCategory.Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public sealed class AudioFileItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}