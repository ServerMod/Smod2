using System;
using System.Reflection;

namespace Smod2.Piping
{
	public abstract class MemberPipe : Attribute
	{
		public Plugin Source { get; private set; }
		public string Name { get; private set; }
		public abstract Type Type { get; protected set; }

		protected void Init(Plugin source, MemberInfo info)
		{
			Source = source;
			Name = info.Name;
		}
	}
}
