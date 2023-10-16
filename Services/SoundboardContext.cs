using Soundboard4MacroDeck.Models;
using SQLite;
using SuchByte.MacroDeck.Backups;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Startup;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Soundboard4MacroDeck.Services;
internal class SoundboardContext
{
    private const string dbFileName = "soundboard.db";
    private static readonly string PluginDir = typeof(PluginInstance).Assembly.Location;
    private static readonly string DbDir = Path.Combine(Path.GetDirectoryName(PluginDir)!, "DB");
    internal static string DbPath { get; } = Path.Combine(DbDir, dbFileName);

    public SoundboardContext()
    {
        Directory.CreateDirectory(DbDir);
        Db = new SQLiteConnection(DbPath);
        var result = Db.CreateTable<AudioFile>();
        Db.CreateTable<AudioCategory>();
        IsInitialCreate = result == CreateTableResult.Created;
    }

    internal static void AddBackupCreationHook()
    {
        BackupManager.BackupSaved += BackupManager_BackupSaved;
    }

    internal static void RemoveBackupCreationHook()
    {
        BackupManager.BackupSaved -= BackupManager_BackupSaved;
    }

    private static void BackupManager_BackupSaved(object sender, EventArgs e)
    {
        try
        {
            PluginInstance.DbContext.Db.Close();
            var backup = new DirectoryInfo(ApplicationPaths.BackupsDirectoryPath).EnumerateFiles().OrderByDescending(f => f.CreationTimeUtc).First();
            using var archive = ZipFile.Open(backup.FullName, ZipArchiveMode.Update);
            archive.CreateEntryFromFile(DbPath, Path.Combine(new DirectoryInfo(ApplicationPaths.PluginsDirectoryPath).Name, Directory.GetParent(PluginDir).Name, "DB", dbFileName));
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Warning(PluginInstance.Current, "Soundboard database was NOT added to backup.");
        }
        finally
        {
            PluginInstance.DbContext = new SoundboardContext();
        }
    }

    public bool IsInitialCreate { get; }

    public SQLiteConnection Db { get; }

    public AudioFile FindAudioFile(int id)
    {
        return Db.Find<AudioFile>(id);
    }

    public IList<AudioFile> GetAudioFiles()
    {
        return Db.Table<AudioFile>().ToList();
    }

    public int InsertAudioFile(AudioFile audioFile)
    {
        Db.Insert(audioFile);
        return audioFile.Id;
    }

    public bool UpdateAudioFile(AudioFile audioFile)
    {
        return Db.Update(audioFile) > 0;
    }

    public AudioCategory FindAudioCategory(int id)
    {
        return Db.Find<AudioCategory>(id);
    }

    public IList<AudioCategory> GetAudioCategories()
    {
        return Db.Table<AudioCategory>().ToList();
    }

    public int InsertAudioCategory(AudioCategory audioCategory)
    {
        Db.Insert(audioCategory);
        return audioCategory.Id;
    }

    public bool UpdateAudioCategory(AudioCategory audioCategory)
    {
        return Db.Update(audioCategory) > 0;
    }
}
