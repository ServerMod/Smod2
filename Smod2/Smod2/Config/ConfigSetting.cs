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

		public ConfigSetting(string key, object defaultValue, SettingType type, bool primaryUser, string description)
		{
			this.key = key.ToUpper();
			this.defaultValue = defaultValue;
			this.randomized = false;
			this.configType = type;
			this.primaryUser = primaryUser;
			this.description = description;
		}
	}
}
