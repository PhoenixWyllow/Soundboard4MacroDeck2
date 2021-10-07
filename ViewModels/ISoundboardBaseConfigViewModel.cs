using Soundboard4MacroDeck.Models;

namespace Soundboard4MacroDeck.ViewModels
{
    public interface ISoundboardBaseConfigViewModel
    {
        protected ISerializableConfiguration SerializableConfiguration { get; }

        void SaveConfig();
    }
}
