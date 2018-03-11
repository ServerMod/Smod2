namespace Smod2.Config
{
	public enum SettingType
	{
		NUMERIC,
		STRING,
		BOOL,
		LIST
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

		private string defaultValue;
		public string Default
		{
			get
			{
				return defaultValue;
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
		private string Description
		{
			get
			{
				return description;
			}
		}

		public ConfigSetting(string key, string defaultValue, SettingType type,  bool primaryUser, string description)
		{
			this.key = key.ToUpper();
			this.defaultValue = defaultValue;
			this.configType = type;
			this.primaryUser = primaryUser;
			this.description = description;
		}

	}
}
