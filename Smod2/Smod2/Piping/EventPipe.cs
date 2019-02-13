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

			if (info.ReturnType != typeof(void))
			{
				PluginManager.Manager.Logger.Warn("PIPE_MANAGER", $"Pipe event {source} returns a value. This is bad practice.");
			}
			Type = info.ReturnType;

			base.Init(info.IsStatic ? null : source, info);
		}

		internal void Invoke(object[] parameters, string caller)
		{
			CheckInit();

			try
			{
				info.Invoke(Source, parameters);
			}
			catch (TargetParameterCountException e)
			{
				if (e.TargetSite.Name != "ConvertValues")
				{
					throw;
				}

				Source.Error($"Failed to handle event pipe: {EventName} (method: {Name}, caller: {caller}). Possible error: invalid number of parameters");
				Source.Error(e.Message);
				Source.Error(e.StackTrace);
			}
			catch (ArgumentException e)
			{
				if (e.TargetSite.Name != "CheckValue")
				{
					throw;
				}

				Source.Error($"Failed to handle event pipe: {EventName} (method: {Name}, caller: {caller}). Possible error: invalid type of parameters");
				Source.Error(e.Message);
				Source.Error(e.StackTrace);
			}
			catch (Exception e)
			{
				Source.Error($"Failed to handle event pipe: {EventName} (method: {Name}, caller: {caller})");
				Source.Error(e.Message);
				Source.Error(e.StackTrace);
			}
		}
	}
}
