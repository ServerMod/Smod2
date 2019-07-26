using Smod2.Attributes;
using Smod2.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Smod2
{
    public class LangManager
    {
        private Dictionary<string, Settings> settings = new Dictionary<string, Settings>();
        private Dictionary<Plugin, Dictionary<string, FieldInfo>> langFields = new Dictionary<Plugin, Dictionary<string, FieldInfo>>();
        private Dictionary<Plugin, Snapshot> snapshots = new Dictionary<Plugin, Snapshot>();

        private Dictionary<string, Dictionary<string, string>> FileNameKeyValue = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<Plugin, HashSet<string>> PluginKeys = new Dictionary<Plugin, HashSet<string>>(); // for rapid removal

        private static LangManager singleton;
        public static LangManager Manager
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new LangManager();
                }
                return singleton;
            }
        }

        public bool IsRegistered(Plugin plugin, string key)
        {
            bool isRegistered = false;
            if (settings.ContainsKey(key))
            {
                isRegistered = settings[key].pluginFiles.ContainsKey(plugin);
            }

            return isRegistered;
        }

        public bool IsRegistered(Plugin plugin, string key, string file)
        {
            bool isRegistered = false;
            if (settings.ContainsKey(key))
            {
                if (settings[key].pluginFiles.ContainsKey(plugin))
                {
                    isRegistered = settings[key].pluginFiles[plugin].Contains(file);
                }
            }

            return isRegistered;
        }

        public bool RegisterTranslation(Plugin plugin, LangSetting setting)
        {
            if (settings.ContainsKey(setting.Key))
            {
                if (settings[setting.Key].pluginFiles.ContainsKey(plugin))
                {
                    if (settings[setting.Key].pluginFiles[plugin].Contains(setting.Filename))
                    {
                        PluginManager.Manager.Logger.Warn("LANG_MANAGER", plugin.ToString() + $" is trying to register a duplicate setting: '{setting.Key}'");
                        return false;
                    }
                    else if (settings[setting.Key].fileLang.ContainsKey(setting.Filename))
                    {
                        PluginManager.Manager.Logger.Warn("LANG_MANAGER", plugin.ToString() + $" is typing to register a duplicate settings: '{setting.Key}' in one file '{setting.Filename}'");
                        return false;
                    }
                    else
                    {
                        settings[setting.Key].pluginFiles[plugin].Add(setting.Filename);
                        settings[setting.Key].fileLang.Add(setting.Filename, setting.Default);
                    }
                }
                else
                {
                    settings[setting.Key].pluginFiles.Add(plugin, new List<string> { { setting.Filename } });
                }
            }
            else
            {
                settings.Add(setting.Key, new Settings(plugin, setting.Filename, setting.Default));
            }

            if (PluginManager.Manager.GetDisabledPlugin(plugin.Details.id) != null)
            {
                snapshots[plugin].Settings.Add(setting);
            }

            if(PluginKeys.ContainsKey(plugin))
            {
                if(!PluginKeys[plugin].Contains(setting.Key))
                {
                    PluginKeys[plugin].Add(setting.Key);
                }
            }
            else
            {
                PluginKeys.Add(plugin, new HashSet<string> { { setting.Key } });
            }

            if (FileNameKeyValue.ContainsKey(setting.Filename))
            {
                if (!FileNameKeyValue[setting.Filename].ContainsKey(setting.Key))
                {
                    File.AppendAllText(Directory.GetCurrentDirectory() + "/./sm_translations/" + setting.Filename + ".txt", setting.Key + ": " + setting.Default + System.Environment.NewLine);
                }
                else
                {
                    PluginManager.Manager.Logger.Debug("LANG_MANAGER", $"'{setting.Key}' exists in translation files.");
                    settings[setting.Key].fileLang[setting.Filename] = FileNameKeyValue[setting.Filename][setting.Key];
                }

            }
            else
            {
                File.AppendAllText(Directory.GetCurrentDirectory() + "/./sm_translations/" + setting.Filename + ".txt", setting.Key + ": " + setting.Default + System.Environment.NewLine);
            }

            return true;
        }

        public string GetTranslation(Plugin plugin, string key)
        {
            if (settings.ContainsKey(key))
            {
                if(settings[key].pluginFiles.ContainsKey(plugin))
                {
                    string fileName = settings[key].pluginFiles[plugin][0];
                    return settings[key].fileLang[fileName].Clone() as string;
                }
            }

            return null;
        }

        public string GetTranslation(Plugin plugin, string key, string file)
        {
            if (settings.ContainsKey(key))
            {
                if(settings[key].pluginFiles.ContainsKey(plugin))
                {
                    if(settings[key].pluginFiles[plugin].Contains(file))
                    {
                        return settings[key].fileLang[file].Clone() as string;
                    }
                }
            }

            return null;
        }

        public void RegisterPlugin(Plugin plugin)
        {
            if (!snapshots.ContainsKey(plugin))
            {
                snapshots.Add(plugin, new Snapshot());
                RegisterAttributes(plugin);
            }
            else
            {
                Snapshot snapshot = snapshots[plugin];
                if (!snapshot.Enabled)
                {
                    return;
                }

                foreach (LangSetting setting in snapshot.Settings)
                {
                    RegisterTranslation(plugin, setting);
                }

                snapshot.Enabled = false;
            }
        }

        public void UnregisterPlugin(Plugin plugin)
        {
            if(PluginKeys.ContainsKey(plugin))
            {
                HashSet<string> keys = PluginKeys[plugin];

                foreach(string key in keys)
                {
                    if(settings.ContainsKey(key))
                    {
                        string[] files = settings[key].pluginFiles[plugin].ToArray(); // to avoid getting an error when deleting and reading data

                        foreach(string file in files)
                        {
                            settings[key].fileLang.Remove(file);
                        }

                        settings[key].pluginFiles.Remove(plugin);
                    }
                }

                PluginKeys.Remove(plugin);

            }

            snapshots[plugin].Enabled = true;
        }

        public void RegisterAttributes(Plugin plugin)
        {
            Type type = plugin.GetType();

            const BindingFlags allMembers = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (FieldInfo field in type.GetFields(allMembers))
            {
                LangOption langOption = field.GetCustomAttribute<LangOption>();
                if (langOption != null)
                {
                    string key = langOption.Key ?? PluginManager.ToUpperSnakeCase(field.Name);

                    string file = plugin.Details.langFile;
                    if (file == null)
                    {
                        PluginManager.Manager.Logger.Error("LANG_MANAGER", $"{plugin} is trying to register attribute lang {field.Name}, but does not have {nameof(PluginDetails.langFile)} in its {nameof(PluginDetails)} set.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(key))
                    {
                        PluginManager.Manager.Logger.Error("LANG_MANAGER", $"{plugin} is trying to register attribute lang {field.Name}, but it has no valid key. Is the variable all underscores with no config key overload?");
                        continue;
                    }

                    if (field.FieldType != typeof(string))
                    {
                        PluginManager.Manager.Logger.Error("LANG_MANAGER", $"{plugin} is trying to register attribute lang {field.Name}, but the type ({field.FieldType}) is not a string.");
                        continue;
                    }

                    if (!RegisterTranslation(plugin, new LangSetting(key, (string)field.GetValue(plugin), file)))
                    {
                        // Failed register so it should not be registered to refresh every round restart.
                        PluginManager.Manager.Logger.Debug("LANG_MANAGER", $"Unable to register attribute translation {field.Name} from {plugin}.");
                        continue;
                    }

                    if (!langFields.ContainsKey(plugin))
                    {
                        langFields.Add(plugin, new Dictionary<string, FieldInfo>());
                    }

                    langFields[plugin].Add(key, field);
                }
            }
        }

        public void RefreshAttributes(Plugin plugin)
        {
            if (!langFields.ContainsKey(plugin))
            {
                return;
            }

            foreach (KeyValuePair<string, FieldInfo> lang in langFields[plugin])
            {
                lang.Value.SetValue(plugin, plugin.GetTranslation(lang.Key, plugin.Details.langFile));
            }
        }

        public LangManager()
        {
            string smTranslationPath = Directory.GetCurrentDirectory() + "/./sm_translations/";
            if (!Directory.Exists(smTranslationPath))
            {
                Directory.CreateDirectory(smTranslationPath);
            }

            DirectoryInfo dir = new DirectoryInfo(smTranslationPath);
            foreach (FileInfo file in dir.GetFiles("*.txt"))
            {
                foreach (string line in File.ReadAllLines(file.FullName))
                {
                    if (line.Length == 0 || line[0] == '#' || line.StartsWith("//"))
                    {
                        continue;
                    }

                    string[] keyvalue = line.Split(':');
                    if (keyvalue.Length < 2)
                    {
                        PluginManager.Manager.Logger.Error("LANG_MANAGER", "Cant load keyvalue from " + file.Name + ": " + line);
                        continue;
                    }

                    keyvalue[1] = keyvalue[1].Substring(1);
                    // If the value contains a colon in it, make sure it isn't cut off
                    if (keyvalue.Length > 2)
                    {
                        for (int i = 2; i < keyvalue.Length; i++)
                        {
                            keyvalue[1] += ":" + keyvalue[i];
                        }
                    }

                    string key = keyvalue[0].ToLower();
                    string filename = file.Name.Substring(0, file.Name.IndexOf('.'));

                    if (FileNameKeyValue.ContainsKey(filename))
                    {
                        if (FileNameKeyValue[filename].ContainsKey(key))
                            PluginManager.Manager.Logger.Error("LANG_MANAGER", "Duplicate key detected: " + keyvalue[0]);
                        else
                        {
                            FileNameKeyValue[filename].Add(key, keyvalue[1]);
                            PluginManager.Manager.Logger.Debug("LANG_MANAGER", line);
                        }
                    }
                    else
                    {
                        FileNameKeyValue.Add(filename, new Dictionary<string, string> { { key, keyvalue[1] } });
                        PluginManager.Manager.Logger.Debug("LANG_MANAGER", line);
                    }
                }
            }
        }

        private class Snapshot
        {
            public List<LangSetting> Settings { get; }
            public bool Enabled { get; set; }

            public Snapshot()
            {
                Settings = new List<LangSetting>();
            }
        }

        private class Settings
        {
            public Dictionary<Plugin, List<string>> pluginFiles;
            public Dictionary<string, string> fileLang;

            public Settings(Plugin plugin, string filename, string lang)
            {
                this.pluginFiles = new Dictionary<Plugin, List<string>> { { plugin, new List<string> { { filename } } } };
                this.fileLang = new Dictionary<string, string> { { filename, lang } };
            }
        }
    }
}
