using System;

namespace Smod2
{
	public abstract class ConfigFile
	{
		public abstract int GetInt(String key, int def);
		public abstract String GetString(String key, String def);
		public abstract Boolean GetBoolean(String key, Boolean def);
	}
}
