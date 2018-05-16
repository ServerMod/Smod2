using System.Collections.Generic;
using Smod2.Config;

namespace Smod2
{
	public class ConfigManager
	{
		private Dictionary<Plugin, Dictionary<string, ConfigSetting>> settings = new Dictionary<Plugin, Dictionary<string, ConfigSetting>>();
		private Dictionary<string, Plugin> primary_settings_map = new Dictionary<string, Plugin>();
		private Dictionary<string, List<Plugin>> secondary_settings_map = new Dictionary<string, List<Plugin>>();

		private static ConfigManager singleton;
		public static ConfigManager Manager
		{
			get
			{
				if (singleton == null)
				{
					singleton = new ConfigManager();
				}
				return singleton;
			}
		}

		private IConfigFile config;
		public IConfigFile Config
		{
			get { return config; }
			set
			{
				if (config == null)
				{
					config = value;
				}
			}
		}

		public bool IsRegistered(Plugin plugin, string key)
		{
			bool isRegistered = false;
			if (primary_settings_map.ContainsKey(key))
			{
				if (primary_settings_map[key] == plugin)
				{
					isRegistered = true;
				}
			}
			else if (secondary_settings_map.ContainsKey(key))
			{
				if (secondary_settings_map[key].Contains(plugin))
				{
					isRegistered = true;
				}
			}

			return isRegistered;
		}

		public object ResolveDefault(string key)
		{
			object def = null;
			if (primary_settings_map.ContainsKey(key))
			{
				ConfigSetting primary = ResolvePrimary(key);
				PluginManager.Manager.Logger.Debug("DEFAULT_CONFIG_RESOLVER", "Primary map contains " + key + ", using default " + primary.Default.ToString());
				if (primary != null)
				{
					def = (primary.Default != null) ? primary.Default : null;
				}
			}
			else
			{
				PluginManager.Manager.Logger.Warn("DEFAULT_CONFIG_RESOLVER", "Default setting for config " + key + " does not exist");
			}

			return def;
		}


		public ConfigSetting ResolvePrimary(string key)
		{
			Plugin plugin;
			primary_settings_map.TryGetValue(key, out plugin);
			return ResolveSetting(plugin, key);
		}

		public ConfigSetting ResolveSetting(Plugin plugin, string key)
		{
			ConfigSetting setting = null;
			Dictionary<string, ConfigSetting> pluginSettings;
			settings.TryGetValue(plugin, out pluginSettings);

			if (pluginSettings != null)
			{
				pluginSettings.TryGetValue(key, out setting);
			}

			return setting;
		}

		public void RegisterPlugin(Plugin plugin)
		{
			if (!settings.ContainsKey(plugin))
			{
				settings.Add(plugin, new Dictionary<string, Config.ConfigSetting>());
			}
		}

		public void RegisterConfig(Plugin plugin, Config.ConfigSetting setting)
		{
			if (!settings.ContainsKey(plugin))
			{
				PluginManager.Manager.Logger.Error("CONFIG_MANAGER", "Trying to register a config setting before the plugin has registered with Config Manager");
			}
			else if (!CheckTypeMatch(setting))
			{
				PluginManager.Manager.Logger.Error("CONFIG_MANAGER", "Trying to register a config setting \"" + setting.Key + "\" with a mismatched default and setting type (Expected \"" + GetTypeString(setting.ConfigValueType) + "\", got \"" + (setting.Default != null ? setting.Default.GetType().ToString() : "null") + "\" instead)");
			}
			else
			{
				Dictionary<string, Config.ConfigSetting> pluginSettings = settings[plugin];

				// Warn if registering an existing config as the primary user
				if (setting.PrimaryUser)
				{
					if (primary_settings_map.ContainsKey(setting.Key))
					{
						if (primary_settings_map[setting.Key] != plugin)
						{
							PluginManager.Manager.Logger.Warn("CONFIG_MANAGER", plugin.ToString() + " is trying to register as a primary user of config setting " + setting.Key + " this may cause some weird behaviour");
						}
					}
					else
					{
						PluginManager.Manager.Logger.Debug("CONFIG_MANAGER", "Adding primary config setting " + setting.Key + " for plugin " + plugin.Details.id);
						primary_settings_map.Add(setting.Key, plugin);
					}
				}
				else
				{
					if (!secondary_settings_map.ContainsKey(setting.Key))
					{
						secondary_settings_map.Add(setting.Key, new List<Plugin>());
					}

					secondary_settings_map[setting.Key].Add(plugin);
				}

				if (!pluginSettings.ContainsKey(setting.Key))
				{
					pluginSettings.Add(setting.Key, setting);
				}
				else
				{
					PluginManager.Manager.Logger.Warn("CONFIG_MANAGER", plugin.ToString() + " is trying to register a duplicate setting: " + setting.Key);
				}
			}
		}

		public bool CheckTypeMatch(ConfigSetting setting)
		{
			if (setting.Default != null)
			{
				switch (setting.ConfigValueType)
				{
					case SettingType.NUMERIC:
						if (setting.Default is int)
						{
							return true;
						}
						break;

					case SettingType.FLOAT:
						if (setting.Default is float)
						{
							return true;
						}
						break;

					case SettingType.STRING:
						if (setting.Default is string)
						{
							return true;
						}
						break;

					case SettingType.BOOL:
						if (setting.Default is bool)
						{
							return true;
						}
						break;

					case SettingType.LIST:
						if (setting.Default is string[])
						{
							return true;
						}
						break;

					case SettingType.NUMERIC_LIST:
						if (setting.Default is int[])
						{
							return true;
						}
						break;

					case SettingType.DICTIONARY:
						if (setting.Default is Dictionary<string, string>)
						{
							return true;
						}
						break;

					case SettingType.NUMERIC_DICTIONARY:
						if (setting.Default is Dictionary<int, int>)
						{
							return true;
						}
						break;
				}
			}

			return false;
		}

		public string GetTypeString(SettingType setting)
		{
			switch (setting)
			{
				case SettingType.NUMERIC:
					return typeof(int).ToString();

				case SettingType.FLOAT:
					return typeof(float).ToString();

				case SettingType.STRING:
					return typeof(string).ToString();

				case SettingType.BOOL:
					return typeof(bool).ToString();

				case SettingType.LIST:
					return typeof(string[]).ToString();

				case SettingType.NUMERIC_LIST:
					return typeof(int[]).ToString();

				case SettingType.DICTIONARY:
					return typeof(Dictionary<string, string>).ToString();

				case SettingType.NUMERIC_DICTIONARY:
					return typeof(Dictionary<int, int>).ToString();

				default:
					return "null";
			}
		}

	}
}
