using System;

namespace Smod2.Logging
{
	public abstract class Logger
	{
		// this is mainly here so we can hook into ServerConsole.AddLog with out knowing about it.
		public abstract void Debug(Plugin plugin, String message);
		public abstract void Info(Plugin plugin, String message);
	}
}
