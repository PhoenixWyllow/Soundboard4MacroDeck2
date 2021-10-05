﻿/* Created on 2021/10/03 by @PhoenixWyllow (pw.dev@outlook.com) https://github.com/PhoenixWyllow/Soundboard4MacroDeck2
 * 
 * This file is provided "AS-IS".
 * You may reuse this file as you wish, but please keep this attribution notice as long as the Implementation code is substantially the same. 
 * Thank you.
 */

using SuchByte.MacroDeck.Language;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Soundboard4MacroDeck.Services
{
    public sealed class Localization
    {
        #region LocalizedStrings
        public string _attribution_ { get; set; } = "built-in values";
        public string _language_ { get; set; } = "English (default)";
        public string Soundboard4MacroDeckDescription { get; set; } = "A soundboard plugin for Macro Deck 2";
        public string ActionPlaySoundName { get; set; } = "Play sound";
        public string ActionPlaySoundDescription { get; set; } = "Plays the configured file";
        public string ActionPlaySoundDisplayName { get; set; } = "Play sound";
        public string ActionPlaySoundVolume { get; set; } = "Volume";
        public string ActionPlaySoundFilePath { get; set; } = "File path";
        public string ActionPlaySoundFilePathPlaceholder { get; set; } = "Get local file";
        public string ActionPlaySoundFileBrowse { get; set; } = "Browse";
        public string ActionPlaySoundURLGet { get; set; } = "Get from URL";
        public string ActionPlaySoundURLFile { get; set; } = "Direct URL of audio file";
        public string ActionPlaySoundInvalidFile { get; set; } = "Invalid File";
        public string ActionPlaySoundFileCouldNotUseFile { get; set; } = "Could not use file. Please check the path is valid and try again.";
        public string ActionPlaySoundURLCouldNotUseFile { get; set; } = "Could not use file. Please check the link and your connection and try again.";
        public string GenericLabelOr { get; set; } = "or";
        public string OutputDevicesGlobal { get; set; } = "Default output device";
        public string OutputDevicesAction { get; set; } = "Default output device for button";
        public string UseSystemDefaultDevice { get; set; } = "Use system default device";
        public string OverrideDefaultDevice { get; set; }

        #endregion

        #region Implementation

        public static void CreateInstance()
        {
            if (Instance is null)
            {
                GetLanguage();
            }
        }

        private static readonly object load = new object();

        public static Localization Instance { get; private set; }

        private Localization()
        {
            LanguageManager.LanguageChanged += (s, e) => GetLanguage();
        }

        private void Dispose()
        {
            LanguageManager.LanguageChanged -= (s, e) => GetLanguage();
        }

        private static void GetLanguage()
        {
            lock (load)
            {
                string languageName = LanguageManager.GetLanguageName();
                if (Instance != null)
                {
                    Instance.Dispose();
                    Instance = null;
                }
                try
                {
                    Instance = JsonSerializer.Deserialize<Localization>(GetJsonLanguageResource(languageName));
                }
                catch
                {
                    //fallback - should never occur if things are done properly
                    Instance = new Localization();
                }
            }
        }

        private static string GetJsonLanguageResource(string languageName)
        {
            var assembly = typeof(Localization).Assembly;
            if (string.IsNullOrEmpty(languageName)
                || !assembly.GetManifestResourceNames().Any(name => name.EndsWith($"{languageName}.json")))
            {
                languageName = "English"; //This should always be present as default, otherwise the code goes to fallback implementation.
            }

            string languageFileName = $"Soundboard4MacroDeck.Resources.Languages.{languageName}.json";

            using var resourceStream = assembly.GetManifestResourceStream(languageFileName);
            using var streamReader = new StreamReader(resourceStream);
            return streamReader.ReadToEnd();
        }
        #endregion
    }
}
