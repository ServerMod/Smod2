using System.Collections.Generic;

namespace Smod2.Config
{
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
		private string key;
		public string Key
		{
			get
			{
				return key;
			}
		}

		private object defaultValue;
		public object Default
		{
			get
			{
				return defaultValue;
			}
		}

		private bool randomized;
		public bool RandomizedValue
		{
			get
			{
				return randomized;
			}
		}

		private SettingType configType;
		public SettingType ConfigValueType
		{
			get
			{
				return configType;
			}
		}

		private bool primaryUser;
		public bool PrimaryUser
		{
			get
			{
				return primaryUser;
			}
		}

		private string description;
		public string Description
		{
			get
			{
				return description;
			}
		}

		public ConfigSetting(string key, object defaultValue, bool randomized, SettingType type,  bool primaryUser, string description)
		{
			this.key = key.ToUpper();
			this.defaultValue = defaultValue;
			this.randomized = randomized;
			this.configType = type;
			this.primaryUser = primaryUser;
			this.description = description;
		}
		
		public ConfigSetting(string key, bool defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.BOOL, primaryUser, description) { }
		public ConfigSetting(string key, string defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.STRING, primaryUser, description) { }
		public ConfigSetting(string key, string[] defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.LIST, primaryUser, description) { }
		public ConfigSetting(string key, int defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.NUMERIC, primaryUser, description) { }
		public ConfigSetting(string key, int[] defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.NUMERIC_LIST, primaryUser, description) { }
		public ConfigSetting(string key, float defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.FLOAT, primaryUser, description) { }
		public ConfigSetting(string key, Dictionary<string, string> defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.DICTIONARY, primaryUser, description) { }
		public ConfigSetting(string key, Dictionary<int, int> defaultValue, bool randomized, bool primaryUser, string description) : this(key, defaultValue, randomized, SettingType.NUMERIC_DICTIONARY, primaryUser, description) { }

		public ConfigSetting(string key, object defaultValue, SettingType type, bool primaryUser, string description) : this(key, defaultValue, false, type, primaryUser, description) { }
		public ConfigSetting(string key, bool defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.BOOL, primaryUser, description) { }
		public ConfigSetting(string key, string defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.STRING, primaryUser, description) { }
		public ConfigSetting(string key, string[] defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.LIST, primaryUser, description) { }
		public ConfigSetting(string key, int defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.NUMERIC, primaryUser, description) { }
		public ConfigSetting(string key, int[] defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.NUMERIC_LIST, primaryUser, description) { }
		public ConfigSetting(string key, float defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.FLOAT, primaryUser, description) { }
		public ConfigSetting(string key, Dictionary<string, string> defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.DICTIONARY, primaryUser, description) { }
		public ConfigSetting(string key, Dictionary<int, int> defaultValue, bool primaryUser, string description) : this(key, defaultValue, SettingType.NUMERIC_DICTIONARY, primaryUser, description) { }
	}
}
