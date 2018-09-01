using Smod2.Lang;
using System.Collections.Generic;
using System.IO;

namespace Smod2
{
	public class LangManager
	{
		private Dictionary<string, Plugin> settings = new Dictionary<string, Plugin>();
		private Dictionary<string, string> keyvalue = new Dictionary<string, string>();

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

		public void RegisterTranslation(Plugin plugin, LangSetting setting)
		{
			if (!settings.ContainsKey(setting.Key))
			{
				settings.Add(setting.Key, plugin);
				if (!keyvalue.ContainsKey(setting.Key))
				{
					keyvalue.Add(setting.Key, setting.Default);
					File.AppendAllText(Directory.GetCurrentDirectory() + "/../sm_translations/" + setting.Filename + ".txt", setting.Key + " = " + setting.Default + System.Environment.NewLine);
				}
				else
				{
					PluginManager.Manager.Logger.Debug("LANG_MANAGER", setting.Key + " exists in translation files.");
				}
			}
			else
			{
				PluginManager.Manager.Logger.Warn("LANG_MANAGER", plugin.ToString() + " is trying to register a duplicate setting: " + setting.Key);
			}
		}

		public string GetTranslation(string key)
		{
			if (keyvalue.ContainsKey(key.ToUpper()))
			{
				return keyvalue[key.ToUpper()];
			}
			return "NO_TRANSLATION";
		}

		public LangManager()
		{
			DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/../sm_translations/");
			foreach (FileInfo file in dir.GetFiles(".txt"))
			{
				foreach (string line in File.ReadAllLines(file.FullName))
				{
					string[] keyvalue = System.Text.RegularExpressions.Regex.Split(line, " = ");
					if (keyvalue.Length == 2)
					{
						if (this.keyvalue.ContainsKey(keyvalue[0].ToUpper()))
						{
							PluginManager.Manager.Logger.Error("LANG_MANAGER", "Duplicate key detected: " + keyvalue[0]);
						}
						else
						{
							this.keyvalue.Add(keyvalue[0].ToUpper(), keyvalue[1]);
							PluginManager.Manager.Logger.Debug("LANG_MANAGER", line);
						}
					}
					else
					{
						PluginManager.Manager.Logger.Error("LANG_MANAGER", "Cant load keyvalue from " + file.Name + ": " + line);
					}
				}
			}
		}
	}
}
