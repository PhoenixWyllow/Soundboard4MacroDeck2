using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soundboard4MacroDeck.Models;

public sealed class AudioFile
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Data { get; set; }

    [ForeignKey(nameof(AudioCategory.Id))]
    public int CategoryId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}

public sealed class AudioCategory
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }

}

public sealed class AudioFileItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}