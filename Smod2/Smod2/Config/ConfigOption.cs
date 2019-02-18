using System;

namespace Smod2.Config
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ConfigOption : Attribute
	{
		public string Key { get; }
		public string Description { get; }
		public bool PrimaryUser { get; }
		public bool Randomized { get; }

		public ConfigOption(bool primaryUser = true, bool randomized = false)
		{
			PrimaryUser = primaryUser;
			Randomized = randomized;
		}
		
		public ConfigOption(string key, bool primaryUser = true, bool randomized = false) : this(primaryUser, randomized)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
		}
		
		public ConfigOption(string key, string description, bool primaryUser = true, bool randomized = false) : this(primaryUser, randomized)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
			Description = description ?? throw new ArgumentNullException(nameof(description));
		}
	}
}