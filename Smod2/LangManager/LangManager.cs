using System;
using Smod2.Lang;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Smod2.Attributes;

namespace Smod2
{
	public class LangManager
	{
		private Dictionary<string, Plugin> settings = new Dictionary<string, Plugin>();
		private Dictionary<string, string> keyvalue = new Dictionary<string, string>();
		private Dictionary<Plugin, Dictionary<string, FieldInfo>> langFields = new Dictionary<Plugin, Dictionary<string, FieldInfo>>();
		private Dictionary<Plugin, Snapshot> snapshots = new Dictionary<Plugin, Snapshot>();

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
				if (settings[key] == plugin)
				{
					isRegistered = true;
				}
			}

			return isRegistered;
		}

		public bool RegisterTranslation(Plugin plugin, LangSetting setting)
		{
			if (!settings.ContainsKey(setting.Key))
			{
				settings.Add(setting.Key, plugin);
				if (PluginManager.Manager.GetDisabledPlugin(plugin.Details.id) != null)
				{
					snapshots[plugin].Settings.Add(setting);
				}
				
				if (!keyvalue.ContainsKey(setting.Key))
				{
					keyvalue.Add(setting.Key, setting.Default);
					File.AppendAllText(Directory.GetCurrentDirectory() + "/./sm_translations/" + setting.Filename + ".txt", setting.Key.ToLower() + ": " + setting.Default + System.Environment.NewLine);
				}
				else
				{
					PluginManager.Manager.Logger.Debug("LANG_MANAGER", setting.Key + " exists in translation files.");
				}
			}
			else
			{
				PluginManager.Manager.Logger.Warn("LANG_MANAGER", plugin.ToString() + " is trying to register a duplicate setting: " + setting.Key);
				return false;
			}

			return true;
		}

		public string GetTranslation(string key)
		{
			if (keyvalue.ContainsKey(key.ToUpper()))
			{
				return keyvalue[key.ToUpper()];
			}
			
			return "NO_TRANSLATION";
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
			List<string> pluginKeys = new List<string>();
			foreach (KeyValuePair<string, Plugin> setting in settings)
			{
				if (plugin == setting.Value)
				{
					pluginKeys.Add(setting.Key);
				}
			}

			foreach (string key in pluginKeys)
			{
				settings.Remove(key);
				keyvalue.Remove(key);
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
						PluginManager.Manager.Logger.Error("LANG_MANAGER",  $"{plugin} is trying to register attribute lang {field.Name}, but does not have {nameof(PluginDetails.langFile)} in its {nameof(PluginDetails)} set.");
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
					
					if (!RegisterTranslation(plugin, new LangSetting(key, (string) field.GetValue(plugin),  file)))
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
				lang.Value.SetValue(plugin, plugin.GetTranslation(lang.Key));
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

					string key = keyvalue[0].ToUpper();
					if (this.keyvalue.ContainsKey(key))
					{
						PluginManager.Manager.Logger.Error("LANG_MANAGER", "Duplicate key detected: " + keyvalue[0]);
					}
					else
					{
						this.keyvalue.Add(key, keyvalue[1]);
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
	}
}
