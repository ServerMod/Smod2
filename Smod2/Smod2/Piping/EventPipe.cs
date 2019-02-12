using System;
using System.Reflection;

namespace Smod2.Piping
{
	[AttributeUsage(AttributeTargets.Method)]
	public class EventPipe : MemberPipe
	{
		private MethodInfo info;
		
		public override Type Type { get; protected set; }
		public string[] PluginScope { get; }
		public string EventName { get; }

		public EventPipe(string eventName)
		{
			EventName = eventName ?? throw new ArgumentNullException(nameof(eventName));
			PluginScope = new string[0];
		}

		public EventPipe(string eventName, params string[] pluginScope) : this(eventName)
		{
			PluginScope = pluginScope ?? throw new ArgumentNullException(nameof(pluginScope));
		}

		internal void Init(Plugin source, MethodInfo info)
		{
			this.info = info;

			Type = typeof(void);

			base.Init(source, info);
		}

		public void Invoke(object[] parameters, string source)
		{
			try
			{
				info.Invoke(Source, parameters);
			}
			catch (Exception e)
			{
				Source.Error("Failed to handle event pipe: " + EventName + " (source: " + source + ", method: " + Name + ")");
				Source.Error(e.Message);
				Source.Error(e.StackTrace);
			}
		}
	}
}
