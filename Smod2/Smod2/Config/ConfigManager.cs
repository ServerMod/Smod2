﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Smod2.Attributes;
using Smod2.Config;

namespace Smod2
{
	public class ConfigManager
	{
		private readonly Dictionary<Type, Func<Plugin, string, object>> typeGetters = new Dictionary<Type, Func<Plugin, string, object>>
		{
			{
				typeof(bool),
				(plugin, key) => plugin.GetConfigBool(key)
			},
			{
				typeof(float),
				(plugin, key) => plugin.GetConfigFloat(key)
			},
			{
				typeof(string),
				(plugin, key) => plugin.GetConfigString(key)
			},
			{
				typeof(string[]),
				(plugin, key) => plugin.GetConfigList(key)
			},
			{
				typeof(int),
				(plugin, key) => plugin.GetConfigInt(key)
			},
			{
				typeof(int[]),
				(plugin, key) => plugin.GetConfigIntList(key)
			},
			{
				typeof(Dictionary<string, string>),
				(plugin, key) => plugin.GetConfigDict(key)
			},
			{
				typeof(Dictionary<int, int>),
				(plugin, key) => plugin.GetConfigIntDict(key)
			}
		};
		
		private Dictionary<Plugin, Dictionary<string, ConfigSetting>> settings = new Dictionary<Plugin, Dictionary<string, ConfigSetting>>();
		private Dictionary<string, Plugin> primary_settings_map = new Dictionary<string, Plugin>();
		private Dictionary<string, List<Plugin>> secondary_settings_map = new Dictionary<string, List<Plugin>>();
		private readonly Dictionary<Plugin, Dictionary<string, FieldInfo>> configFields = new Dictionary<Plugin, Dictionary<string, FieldInfo>>();
		
		private readonly Dictionary<Plugin, SnapshotEntry> disabledPlugins = new Dictionary<Plugin, SnapshotEntry>();

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
				if (primary != null)
				{
					PluginManager.Manager.Logger.Debug("DEFAULT_CONFIG_RESOLVER", "Primary map contains " + key + ", using default " + primary.Default);
					def = primary.Default;
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
			if (settings.ContainsKey(plugin))
			{
				return;
			}

			if (!disabledPlugins.ContainsKey(plugin))
			{
				settings.Add(plugin, new Dictionary<string, Config.ConfigSetting>());
				RegisterAttributes(plugin);
			}
			else
			{
				SnapshotEntry snapshots = disabledPlugins[plugin];
				disabledPlugins.Remove(plugin);

				settings.Add(plugin, snapshots.Settings);

				foreach (string setting in snapshots.Primaries)
				{
					primary_settings_map.Add(setting, plugin);
				}

				foreach (string setting in snapshots.Secondaries)
				{
					if (!secondary_settings_map.ContainsKey(setting))
					{
						secondary_settings_map.Add(setting, new List<Plugin>());
					}

					secondary_settings_map[setting].Add(plugin);
				}
			}
		}

		private static string ToUpperSnakeCase(string otherCase)
		{
			string snakeCase = "";
			for (int i = 0; i < otherCase.Length; i++)
			{
				if (i > 0 && char.IsUpper(otherCase[i]) && otherCase[i - 1] != '_')
				{
					snakeCase += "_" + otherCase[i];
				}
				else
				{
					snakeCase += char.ToUpper(otherCase[i]);	
				}
			}

			return snakeCase;
		}

		public void RegisterAttributes(Plugin plugin)
		{
			Type type = plugin.GetType();

			const BindingFlags allMembers = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			foreach (FieldInfo field in type.GetFields(allMembers))
			{
				ConfigOption configOption = field.GetCustomAttribute<ConfigOption>();
				if (configOption != null)
				{
					string key = configOption.Key ?? ToUpperSnakeCase(field.Name);
					
					string prefix = plugin.Details.configPrefix;
					if (prefix == null)
					{ 
						PluginManager.Manager.Logger.Error("CONFIG_MANAGER",  $"{plugin} is trying to register attribute config {field.Name}, but does not have {nameof(PluginDetails.configPrefix)} in its {nameof(PluginDetails)} set.");
						return;
					}
					
					if (!typeGetters.ContainsKey(field.FieldType))
					{
						PluginManager.Manager.Logger.Error("CONFIG_MANAGER", $"{plugin} is trying to register attribute config {field.Name}, but the type ({field.FieldType}) is not a config-allowed type.");
						continue;
					}
					
					if (field.IsInitOnly)
					{
						PluginManager.Manager.Logger.Error("CONFIG_MANAGER", $"{plugin} is trying to register attribute config {field.Name}, but the field is readonly.");
						continue;
					}
					
					if (!RegisterConfig(plugin, new ConfigSetting(prefix + "_" + key, field.GetValue(plugin), configOption.Randomized, configOption.PrimaryUser, configOption.Description)))
					{
						// Failed register so it should not be registered to refresh every round restart.
						continue;
					}

					if (!configFields.ContainsKey(plugin))
					{
						configFields.Add(plugin, new Dictionary<string, FieldInfo>());
					}

					configFields[plugin].Add(key, field);
				}
			}
		}

		public void RefreshAttributes(Plugin plugin)
		{
			if (!configFields.ContainsKey(plugin))
			{
				return;
			}

			foreach (KeyValuePair<string, FieldInfo> configOption in configFields[plugin])
			{
				configOption.Value.SetValue(plugin, typeGetters[configOption.Value.FieldType].Invoke(plugin, plugin.Details.configPrefix + "_" + configOption.Key));
			}
		}

		public bool RegisterConfig(Plugin plugin, Config.ConfigSetting setting)
		{
			if (!settings.ContainsKey(plugin))
			{
				PluginManager.Manager.Logger.Error("CONFIG_MANAGER", "Trying to register a config setting before the plugin has registered with Config Manager");
				return false;
			}

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

			return true;
		}

		private void SetFieldToDefault(Plugin plugin, FieldInfo field, string key)
		{
			field.SetValue(plugin, ResolveSetting(plugin, key).Default);
		}

		public void UnregisterConfig(Plugin plugin, string key)
		{
			if (!settings.ContainsKey(plugin))
			{
				return;
			}
			
			settings[plugin].Remove(key);
			// If the secondary map does not contain the config entry or if the plugin was not registered to it, remove the primary entry.
			if (!secondary_settings_map.ContainsKey(key) || !secondary_settings_map[key].Remove(plugin))
			{
				primary_settings_map.Remove(key);
			}

			if (configFields.ContainsKey(plugin))
			{
				Dictionary<string, FieldInfo> fields = configFields[plugin];
				if (fields.ContainsKey(key))
				{
					SetFieldToDefault(plugin, fields[key], key);
					fields.Remove(key);
				}
			}
		}

		public void UnloadPlugin(Plugin plugin)
		{
			if (!settings.ContainsKey(plugin))
			{
				return;
			}

			SnapshotEntry snapshot = new SnapshotEntry(settings[plugin], new List<string>(), new List<string>());
			disabledPlugins.Add(plugin, snapshot);
			settings.Remove(plugin);

			var updated_primary_settings_map = new Dictionary<string, Plugin>();
			foreach (var pair in primary_settings_map)
			{
				if (plugin != pair.Value)
				{
					updated_primary_settings_map.Add(pair.Key, pair.Value);
				}
				else
				{
					snapshot.Primaries.Add(pair.Key);
				}
			}
			primary_settings_map = updated_primary_settings_map;

			foreach (var pair in secondary_settings_map)
			{
				var updated_list = new List<Plugin>();
				foreach (var secondary_setting_plugin in pair.Value)
				{
					if (secondary_setting_plugin != plugin)
					{
						updated_list.Add(secondary_setting_plugin);
					}
					else
					{
						snapshot.Secondaries.Add(pair.Key);
					}
				}
				pair.Value.Clear();
				pair.Value.AddRange(updated_list);
			}

			// Set all config fields to default
			if (configFields.ContainsKey(plugin))
			{
				foreach (KeyValuePair<string, FieldInfo> configOption in configFields[plugin])
				{
					SetFieldToDefault(plugin, configOption.Value, configOption.Key);
				}
			}
		}

		private class SnapshotEntry
		{
			public Dictionary<string, ConfigSetting> Settings { get; }
			public List<string> Primaries { get; }
			public List<string> Secondaries { get; }

			public SnapshotEntry(Dictionary<string, ConfigSetting> settings, List<string> primaries, List<string> secondaries)
			{
				Settings = settings;
				Primaries = primaries;
				Secondaries = secondaries;
			}
		}
	}
}
