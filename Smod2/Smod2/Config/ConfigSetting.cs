using System;
using System.Collections.Generic;

namespace Smod2.Config
{
	[Obsolete("Use ConfigSetting.Default.GetType() instead.", true)]
	public enum SettingType
	{
		NUMERIC,
		FLOAT,
		STRING,
		BOOL,
		LIST,
		NUMERIC_LIST,
		DICTIONARY,
		NUMERIC_DICTIONARY
	}

	public class ConfigSetting
	{
		public string Key { get; }

		public object Default { get; }

		public bool RandomizedValue { get; }

		public bool PrimaryUser { get; }

		public string Description { get; }

		public ConfigSetting(string key, object defaultValue, bool randomized, bool primaryUser, string description)
		{
			if (!ConfigManager.Manager.typeGetters.ContainsKey(defaultValue.GetType()))
			{
				throw new ArgumentException("Not a supported config type.", nameof(defaultValue));
			}
			
			Key = key.ToUpper();
			Default = defaultValue;
			RandomizedValue = randomized;
			PrimaryUser = primaryUser;
			Description = description;
		}
		
		[Obsolete("Use the constructor without SettingType.")]
		public ConfigSetting(string key, object defaultValue, bool randomized, SettingType type,  bool primaryUser, string description) : this(key, defaultValue, randomized, primaryUser, description) { }

		public ConfigSetting(string key, object defaultValue, bool primaryUser, string description) : this(key, defaultValue, false, primaryUser, description) { }
		[Obsolete("Use the constructor without SettingType.")]
		public ConfigSetting(string key, object defaultValue, SettingType type, bool primaryUser, string description) : this(key, defaultValue, false, type, primaryUser, description) { }
	}
}
