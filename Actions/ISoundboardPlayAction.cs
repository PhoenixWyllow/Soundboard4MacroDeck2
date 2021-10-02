using Soundboard4MacroDeck.Models;

namespace Soundboard4MacroDeck.Actions
{
    public interface ISoundboardPlayAction
    {
        SoundboardActions ActionType { get; }
    }
}