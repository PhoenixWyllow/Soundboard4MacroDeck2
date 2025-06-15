using Soundboard4MacroDeck.Actions;
using Soundboard4MacroDeck.Models;

using SuchByte.MacroDeck.Backups;
using SuchByte.MacroDeck.Notifications;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Profiles;

namespace Soundboard4MacroDeck.Services;
internal class ConfigUpdater
{
    public static void MoveToContext()
    {
        int categoryId = PluginInstance.DbContext.InsertAudioCategory(new AudioCategory { Name = "Default category" });

        bool hasActions = ProfileManager.Profiles.Any(profile => profile.Folders.Any(folder => folder.ActionButtons.Any()));
        if (hasActions)
        {
            SoundboardContext.RemoveBackupCreationHook();
            PluginInstance.DbContext.Db.Close();
            BackupManager.CreateBackup();
            PluginInstance.DbContext = new SoundboardContext();

            var db = PluginInstance.DbContext;
            db.Db.RunInTransaction(() =>
            {
                foreach (var profile in ProfileManager.Profiles)
                {
                    foreach (var folder in profile.Folders)
                    {
                        foreach (var actionButton in folder.ActionButtons)
                        {
                            foreach (var action in actionButton.Actions)
                            {
                                ChangeConfiguration(action, db, categoryId);
                            }
                            foreach (var action in actionButton.ActionsLongPress)
                            {
                                ChangeConfiguration(action, db, categoryId);
                            }
                            foreach (var action in actionButton.ActionsLongPressRelease)
                            {
                                ChangeConfiguration(action, db, categoryId);
                            }
                            foreach (var action in actionButton.ActionsRelease)
                            {
                                ChangeConfiguration(action, db, categoryId);
                            }

                            foreach (var action in actionButton.EventListeners.SelectMany(eventListener => eventListener.Actions))
                            {
                                ChangeConfiguration(action, db, categoryId);
                            }
                        }
                    }
                }
                ProfileManager.Save();
            });
            SoundboardContext.AddBackupCreationHook();
            filesAdded.Clear();

            NotificationManager.Notify(PluginInstance.Current, "SoundBoard Upgrade", "A major update was performed. A backup has been made.");
        }
    }

    private static readonly Type[] ActionTypes =
        [
            typeof(SoundboardPlayAction),
            typeof(SoundboardPlayStopAction),
            typeof(SoundboardOverlapAction),
            typeof(SoundboardLoopAction),
        ];

    private static readonly Dictionary<string, int> filesAdded = [];

    private static void ChangeConfiguration(PluginAction? action, SoundboardContext db, int categoryId)
    {
        if (action is not null && ActionTypes.Contains(action.GetType()))
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var actionParametersLegacy = ActionParameters.Deserialize(action.Configuration);
#pragma warning restore CS0618 // Type or member is obsolete
            var data = BitConverter.ToString(actionParametersLegacy.FileData!);
            if (!filesAdded.TryGetValue(data, out var entryId))
            {
                entryId = db.InsertAudioFile(new AudioFile { Data = actionParametersLegacy.FileData!, Name = actionParametersLegacy.FileName, CategoryId = categoryId });
                filesAdded.Add(data, entryId);
            }

            using (AudioReader reader = new(actionParametersLegacy.FileName, actionParametersLegacy.FileData!, false))
            {
                SuchByte.MacroDeck.Variables.VariableManager.SetValue($"sb_{entryId}", reader.TotalTime.ToString(@"mm\:ss"), SuchByte.MacroDeck.Variables.VariableType.String, PluginInstance.Current, null);
            }

            var actionParameters = new ActionParametersV2
            {
                FileName = actionParametersLegacy.FileName,
                Volume = actionParametersLegacy.Volume,
                UseDefaultDevice = actionParametersLegacy.UseDefaultDevice,
                OutputDeviceId = actionParametersLegacy.OutputDeviceId,
                SyncButtonState = actionParametersLegacy.SyncButtonState,
                AudioFileId = entryId
            };
            action.ConfigurationSummary = $"{actionParameters.AudioFileId} - {actionParameters.FileName}";
            action.Configuration = actionParameters.Serialize();

        }
    }
}
