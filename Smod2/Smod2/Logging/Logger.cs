using System;

namespace Smod2.Logging
{
	public abstract class Logger
	{
		// Use for non-plugin logging
		public abstract void Debug(string tag, string message);
		public abstract void Info(string tag, string message);
		public abstract void Warn(string tag, string message);
		public abstract void Error(string tag, string message);

	}
}
