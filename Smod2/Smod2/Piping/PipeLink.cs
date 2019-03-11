using System;

namespace Smod2.Piping
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class PipeLink : Attribute
	{
		public string Plugin { get; }
		public string Pipe { get; }

		public PipeLink(string pluginId, string pipeName)
		{
			Plugin = pluginId ?? throw new ArgumentNullException(nameof(pluginId));
			Pipe = pipeName ?? throw new ArgumentNullException(nameof(pipeName));
		}
	}
}
