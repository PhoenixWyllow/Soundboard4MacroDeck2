---
Last update: 23/10/2025
Plugin version: Check your installation for current version and available updates
---

# Troubleshooting Guide

This guide helps you resolve common issues with the Soundboard plugin for Macro Deck 2. Please follow these steps before reporting a new issue.

---

## Table of Contents
1. [Audio File Issues](#audio-file-issues)
2. [Playback Problems](#playback-problems)
3. [Output Device Issues](#output-device-issues)
4. [Database and Storage Issues](#database-and-storage-issues)
5. [Performance and Timing Issues](#performance-and-timing-issues)
6. [Configuration and UI Issues](#configuration-and-ui-issues)
7. [Reporting an Issue](#reporting-an-issue)

---

## Audio File Issues

### Problem: "Invalid File" error when adding audio

**Symptoms:**
- Error message: "Could not use file. Please check the path is valid and try again."
- Error message: "Could not use file. Please check the link and your connection and try again." (for URLs)

**Possible causes and solutions:**

1. **Unsupported file format**
   - The plugin checks file signatures (magic numbers), not file extensions
   - **Supported formats:** `aif`, `aiff`, `mid`, `midi`, `m4a`, `mp3`, `ogg`, `oga`, `aac`, `flac`, `wma`, `wav`
   - Some files may have incorrect extensions (e.g., an mp4 file named with .mp3 extension)
   - **What to check:**
     - Verify the actual file format matches the extension
     - Try opening the file in a media player to confirm it's valid
     - If you received a warning about "Detected file type is not supported but may still work", the file might play but is not guaranteed to work correctly
     - Try converting the file to a known supported format using audio conversion software

2. **File path issues (local files)**
   - **What to check:**
     - Ensure the file exists at the specified path
   - Check for special characters in the file name or path
     - Verify you have read permissions for the file
     - Try copying the file to a simpler path (e.g., `C:\Temp\audio.mp3`)

3. **Network issues (URL files)**
   - **What to check:**
     - Verify the URL is a direct link to an audio file (not a webpage)
     - Check your internet connection
     - Ensure the URL is accessible (not behind authentication or firewall)
     - Try the URL in a web browser first
     - Some sites may block direct file access - you may need to download the file first

**When to report:**
- If a valid audio file in a supported format consistently fails to load
- Include: File format, file size, source (local/URL), and any error messages

---

## Playback Problems

### Problem: Audio doesn't play or stops unexpectedly

**Symptoms:**
- Button pressed but no sound
- Audio cuts off mid-playback
- Crackling or distorted audio

**Possible causes and solutions:**

1. **Audio file not found in database**
   - After upgrading the plugin, audio files should be migrated automatically
   - **What to check:**
     - Open the plugin configuration (Soundboard icon in Macro Deck)
     - Go to "Audio files" tab
     - Verify your audio files are listed
     - Check the logs for messages like "Audio file not found. Cannot play sound."

2. **Output device issues**
   - **What to check:**
     - Open plugin configuration -> "Output device" tab
     - Verify the correct output device is selected
     - Try using "Use system default device"
     - Open button's action configuration and check the "Output device" setting: try resetting it as well
     - If using Voicemeeter or virtual devices, ensure they're running and configured correctly
     - Check Windows sound settings to ensure the device is working

3. **Action type conflicts**
   - **What to check:**
     - **Play action:** Stops all other sounds when triggered
     - **Play/Stop action:** Toggles playback on/off
     - **Overlap action:** Plays over other sounds
     - **Loop action:** Plays continuously until stopped
     - Ensure you're using the correct action type for your needs

4. **Volume settings**
   - **What to check:**
     - Volume in button configuration (default: 50%)
     - System volume for the output device
     - Application volume mixer in Windows

**When to report:**
- If audio files randomly fail to play
- If specific files consistently fail
- Include: Action type used, audio file format, output device type

---

## Output Device Issues

### Problem: Sound plays on wrong device or no device available

**Symptoms:**
- Sound plays on speakers instead of headphones (or vice versa)
- No output devices listed in configuration
- Selected device not being used

**Possible causes and solutions:**

1. **Device not detected**
   - **What to check:**
     - Open the plugin configuration and/or button action configuration to see available devices and select the correct one
     - Restart Macro Deck
     - Check Windows sound settings to ensure the device is enabled and working
     - Try unplugging and reconnecting USB audio devices

2. **Default device vs. custom device**
   - **Understanding the hierarchy:**
     - Global default: Set in plugin configuration -> "Output device"
     - Button-specific: Can override global default in button configuration
     - System default: Fallback option available via "Use system default device"
   - **What to check:**
     - If sound plays on unexpected device, check button-specific settings first
     - Use "Use system default device" option to reset to system preferences

3. **Virtual devices (Voicemeeter, VB-Audio Cable, etc.)**
   - **What to check:**
     - Ensure virtual device software is running BEFORE starting Macro Deck
     - Restart Macro Deck after starting virtual device software
     - Check virtual device's own configuration

**When to report:**
- If a working device doesn't appear in the list
- If device selection doesn't persist after restart
- Include: Device type (physical/virtual), device name, Windows version

> [!NOTE]
> Microphones are not supported for playback in this plugin. They are not listed as **output** devices.\
> You may try using a virtual device (e.g. Voicemeeter) to route audio to a microphone input if needed.

---

## Database and Storage Issues

### Problem: Missing audio files after update or restart

**Symptoms:**
- Previously configured buttons show errors
- Audio files missing from configuration
- "Audio file not found" errors in logs

**Possible causes and solutions:**

1. **Database migration issues**
   - Major updates may require database migration
   - A backup is automatically created before migration
   - **What to check:**
     - Look for notification: "A major update was performed. A backup has been made."
     - Check Macro Deck's backup folder
     - Logs may show: "Fixed X audio file(s) with invalid category references"

2. **Orphaned category references**
   - If a category is deleted, associated files are set to "Uncategorized"
   - **What to check:**
     - Open plugin configuration -> "Audio files" tab
     - Look for files with "(Uncategorized)" category
     - Reassign to appropriate categories if needed

3. **Database corruption**
   - Rare but possible if Macro Deck crashes during save
   - **What to check:**
     - Database location: `%APPDATA%\Macro Deck\plugins\PhoenixWyllow.Soundboard4Macrodeck\DB\` - the file is `soundboard.db`
     - Restore from Macro Deck backup if necessary

**When to report:**
- If audio files disappear without explanation
- If database migration fails
- Include: 
  - Plugin version before and after update
  - Whether backup was created
  - Logs

---

## Performance and Timing Issues

### Problem: Delayed playback or stuttering

**Symptoms:**
- Delay between button press and sound
- Audio stuttering or buffering
- High CPU usage

**Possible causes and solutions:**

1. **Large audio files**
   - All audio is stored in the database and loaded into memory
- **What to check:**
     - Check file sizes of your audio files
     - Consider using shorter clips or lower bitrates for frequently-used sounds
- Monitor memory usage in Task Manager

2. **Multiple overlapping sounds**
   - **Overlap action** allows many sounds to play simultaneously
   - **What to check:**
     - Reduce number of overlapping sounds if experiencing issues
     - Use **Play action** instead to stop previous sounds

3. **Output device latency**
   - Some devices have higher inherent latency
   - **What to check:**
     - Try different output devices to compare
     - For virtual devices, check their latency settings

**When to report:**
- If latency is excessive (>500ms) with standard audio files
- Include: 
  - File size
  - File format
  - Number of simultaneous sounds
  - System specs

---

## Configuration and UI Issues

### Problem: Cannot delete category or audio file

**Symptoms:**
- Error: "This category is in use by one or more audio files and cannot be deleted"
- Delete confirmation doesn't work

**Possible causes and solutions:**

1. **Category in use**
   - Categories cannot be deleted if audio files are assigned to them
   - **What to check:**
     - Go to "Audio files" tab
     - Filter or search for files in that category
     - Reassign or delete those files first
     - Then delete the category

2. **File in use by buttons**
   - Deleting audio files will cause buttons using them to stop working
   - **What to check:**
     - Review which buttons use the file before deleting
     - Warning shown: "This may cause some buttons to stop working"
 - Confirm deletion only if you're sure

### Problem: Changes not saving

**Symptoms:**
- Configuration reverts after closing
- Edited names or categories reset

**Possible causes and solutions:**

1. **Not clicking OK**
   - Changes only save when clicking OK button
   - **What to check:**
     - Always click OK to save changes
     - Don't close window with X button if you want to keep changes

2. **Cell edit mode still active**
   - When editing table cells, changes must be committed first
   - **What to check:**
     - Press Enter or click outside the cell before clicking OK
     - Logs may show: "config saved" or "config NOT saved"

**When to report:**
- If changes are confirmed but still don't persist
- Include: 
  - Specific configuration being changed
  - Steps to reproduce

---

## Time Variables Issues

### Problem: Time variables not updating or not visible

**Symptoms:**
- Variables like `sb_[id]_elapsed_[guid]` don't appear
- Time displays show `00:00` or don't update

**Possible causes and solutions:**

1. **Variable visibility**
   - Time variables only appear after the button is played at least once
   - **What to check:**
     - Play the button once to initialize variables
     - Check Macro Deck's variable list after playback

2. **Action type limitations**
   - Elapsed/remains variables only available for **Play** and **Play/Stop** actions
   - Total time (`sb_[id]`) is available for all actions
   - **What to check:**
     - Verify you're using the correct action type
     - Use the correct variable format

3. **Variable format**
   - **Format:** `sb_[id]_[elapsed|remains]_[button_guid]`
     - Button GUID uses underscores (`_`), not hyphens (`-`)
   - **What to check:**
     - Copy GUID from bottom of button configuration window
     - Replace hyphens with underscores

**When to report:**
- If variables don't update for supported actions
- Include: 
  - Action type
  - Variable name used
  - Button GUID

---

## Localization Issues

### Problem: Plugin shows English instead of my language

**Symptoms:**
- Expected language not displaying
- Mixed languages in UI

**Possible causes and solutions:**

> [!IMPORTANT]
> Consider contributing translations or corrections: [Crowdin Project](https://crowdin.com/project/soundboard4macrodeck2)

1. **Language not available**
   - Currently supported: English, Italian, German, Spanish, Russian, Portuguese
   - **What to check:**
     - Check if your language is in the supported list
     - Plugin defaults to English if language unavailable

2. **Macro Deck language setting**
   - Plugin uses Macro Deck's language setting
   - **What to check:**
     - Verify Macro Deck's language setting
     - Restart Macro Deck after changing language

**When to report:**
- If your language is supported but not displaying
- If translations are incorrect or incomplete
- Include: Expected language, Macro Deck language setting

---

## Reporting an Issue

If you've tried the troubleshooting steps above and still have issues, please report it on GitHub with the following information:

### Required Information

1. **Environment:**
   - Macro Deck version
   - Soundboard plugin version
   - Windows version

2. **Issue description:**
   - What you were trying to do
   - What happened instead
   - What you expected to happen

3. **Steps to reproduce:**
   - Detailed steps to recreate the issue
   - Include audio file format and source (if relevant)
   - Configuration settings used

4. **Logs:**
   - Check Macro Deck logs for errors
   - Look for messages from "Soundboard4MacroDeck"
   - Include relevant log excerpts in your report (or the whole file!)

### Finding Logs

1. Open Macro Deck
2. Go to Settings -> Logs
3. Look for entries with source "Soundboard4MacroDeck"
4. Copy relevant error messages, warnings, or traces

### What to Include

**Always include:**
- Environment info (see above)
- Steps to reproduce
- Expected vs actual behavior

**Include if relevant:**
- Audio file format and size
- Output device type
- Button configuration (action type, settings)
- Logs showing errors or warnings
- Screenshots (if UI-related)

**Don't include:**
- Copyrighted audio files
- Personal information
- Complete database files (unless specifically requested)

### Where to Report

- **GitHub Issues:** [https://github.com/PhoenixWyllow/Soundboard4MacroDeck2/issues](https://github.com/PhoenixWyllow/Soundboard4MacroDeck2/issues)
- Search past and present issues first to avoid duplicates
- Use descriptive titles (e.g., "Audio files fail to play on Voicemeeter device" instead of "Not working")

---

## Additional Resources

- **Main README:** [README.md](README.md)
- **Feature Documentation:** See main README for feature descriptions
- **Localization:** [Resources/Languages/README.md](Resources/Languages/README.md)
- **Macro Deck website:** [https://macro-deck.app/](https://macro-deck.app/) - there's a Discord server too!
---

## Common Error Messages Reference

| Error Message | Location | Meaning | Solution Section |
|---------------|----------|---------|-----------------|
| "Invalid File" | File upload | File format not supported or corrupted | [Audio File Issues](#audio-file-issues) |
| "Could not use file" | Local file selection | File path invalid or inaccessible | [Audio File Issues](#audio-file-issues) |
| "Audio file not found" | Playback | File missing from database | [Database and Storage Issues](#database-and-storage-issues) |
| "Category not found or no audio files" | Random category action | Empty category or missing category | [Database and Storage Issues](#database-and-storage-issues) |
| "This category is in use" | Category deletion | Cannot delete category with assigned files | [Configuration and UI Issues](#configuration-and-ui-issues) |
| "Detected file type is not supported" | File upload | File signature doesn't match expected format | [Audio File Issues](#audio-file-issues) |
| "Fixed X audio file(s) with invalid category" | Startup | Auto-repair of orphaned files | [Database and Storage Issues](#database-and-storage-issues) |
