using Soundboard4MacroDeck.Models;

namespace Soundboard4MacroDeck.Actions
{
    public interface ISoundboardAction
    {
        SoundboardActions ActionType { get; }
    }
}