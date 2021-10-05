using Soundboard4MacroDeck.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundboard4MacroDeck.ViewModels
{
    public interface ISoundboardBaseConfigViewModel
    {
        protected ISerializableConfiguration SerializableConfiguration { get; }

        void SaveConfig();
    }
}
