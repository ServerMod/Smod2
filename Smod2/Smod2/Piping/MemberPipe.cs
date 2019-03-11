using System;
using System.Reflection;

namespace Smod2.Piping
{
	public abstract class MemberPipe
	{
		protected readonly Plugin instance;
		public Plugin Source { get; }
		public string Name { get; }
		public Type Type { get; protected set; }

		protected MemberPipe(Plugin source, MemberInfo info, bool @static)
		{
			Source = source;
			Name = info.Name;
			instance = @static ? null : source;
		}
	}
}
