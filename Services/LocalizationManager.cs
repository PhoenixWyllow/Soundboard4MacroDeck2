/* Created on 2021/10/03 by @PhoenixWyllow (pw.dev@outlook.com) https://github.com/PhoenixWyllow/Soundboard4MacroDeck2
 * 
 * This file is provided "AS-IS".
 * You may reuse this file as you wish, but please keep this attribution notice as long as the code is substantially the same. 
 * Thank you.
 */

using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Soundboard4MacroDeck.Services
{
    internal sealed class LocalizationManager
    {
        private static readonly object key = new object();

        internal static Localization Instance { get; private set; }

        internal static void CreateInstance()
        {
            if (Instance is null)
            {
                GetLocalization();
            }
        }

        private LocalizationManager() { }

        private static void GetLocalization()
        {
            lock (key)
            {
                string languageName = LanguageManager.GetLanguageName();
                if (Instance != null)
                {
                    LanguageManager.LanguageChanged -= (s, e) => GetLocalization();
                }
                try
                {
                    Instance = JsonSerializer.Deserialize<Localization>(GetJsonLanguageResource(languageName));
                }
                catch (Exception ex)
                {
                    //fallback - should never occur if things are done properly
                    Instance = new Localization();
                    MacroDeckLogger.Warning(Main.Instance, $"{nameof(LocalizationManager)}.{nameof(GetLocalization)}: {ex.Message}");
                }
                finally
                {
                    LanguageManager.LanguageChanged += (s, e) => GetLocalization();
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
    }
}
