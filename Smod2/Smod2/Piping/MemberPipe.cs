using System;
using System.Reflection;

namespace Smod2.Piping
{
	public abstract class MemberPipe : Attribute
	{
		private bool initialized;

		public Plugin Source { get; private set; }
		public string Name { get; private set; }
		public abstract Type Type { get; protected set; }

		protected void Init(Plugin source, MemberInfo info)
		{
			Source = source;
			Name = info.Name;

			initialized = true;
		}

		protected void CheckInit()
		{
			if (!initialized)
			{
				throw new InvalidOperationException("The pipe member has not fully initialized yet. Use pipes in or after Plugin.PipeRegister");
			}
		}
	}
}
