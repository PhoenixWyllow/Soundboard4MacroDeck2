# Contributing Languages 

This plugin has its own localization files, independent of Macro Deck.
These are in a json format and are parsed at application load.

If your language is not available, the plugin will default to English.
***
### If your language is missing or incomplete, please consider helping me out by creating a pull request with the translated file!

To contribute your language, create a new file in your own fork, starting with a copy of English.json.

Change the file name to the same as can be found in Macro Deck 2 (check the available languages [here](https://github.com/SuchByte/Macro-Deck/tree/master/Language) and contribute there if necessary too!)
and change the strings to your language.

The content of your file should now look something like this:
```json
{
  "_attribution_": "PhoenixWyllow",
  "_language_": "English",
  "Soundboard4MacroDeckDescription": "A soundboard plugin for Macro Deck 2",
}
```

Then create a pull request. I will add it and provide a new build as soon as possible!
