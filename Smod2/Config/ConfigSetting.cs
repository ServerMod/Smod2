using System;
using System.Collections.Generic;

namespace Smod2.Config
{
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

		public ConfigSetting(string key, object defaultValue, bool primaryUser, string description) : this(key, defaultValue, false, primaryUser, description) { }
	}
}
