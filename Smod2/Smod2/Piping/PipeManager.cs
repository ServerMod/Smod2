using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Smod2.Piping
{
	public class PipeManager
	{
		private const BindingFlags AllMembers = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		private readonly Dictionary<Type, FieldInfo[]> linkFields;
		private readonly Dictionary<Type, List<FieldInfo>> linkReferences;
		private readonly Dictionary<string, List<EventPipe>> events;
		private readonly Dictionary<Type, Func<PluginPipes, string, object>> pipeGetters;

		private static PipeManager manager;
		public static PipeManager Manager => manager ?? (manager = new PipeManager());

		public PipeManager()
		{
			linkFields = new Dictionary<Type, FieldInfo[]>();
			linkReferences = new Dictionary<Type, List<FieldInfo>>();
			events = new Dictionary<string, List<EventPipe>>();
			pipeGetters = new Dictionary<Type, Func<PluginPipes, string, object>>
			{
				{
					typeof(FieldPipe),
					(pipes, pipeName) => pipes.GetField(pipeName)
				},
				{
					typeof(PropertyPipe),
					(pipes, pipeName) => pipes.GetProperty(pipeName)
				},
				{
					typeof(MethodPipe),
					(pipes, pipeName) => pipes.GetMethod(pipeName)
				}
			};
		}

		private void SetPipeLink(Plugin source, FieldInfo info, string pluginId, string pipeName)
		{
			Plugin target = PluginManager.Manager.GetEnabledPlugin(pluginId);
			if (target == null)
			{
				return;
			}

			if (!pipeGetters.ContainsKey(info.FieldType))
			{
				PluginManager.Manager.Logger.Error("PIPE_MANAGER", $"{info.Name} in {info.DeclaringType?.FullName ?? "namespace"} of {source.Details.id} tried to link to a non-existant pipe type: {info.FieldType}");
				return;
			}

			Type targetType = target.GetType();
			if (!linkReferences.ContainsKey(targetType))
			{
				linkReferences.Add(targetType, new List<FieldInfo>());
			}
			linkReferences[targetType].Add(info);

			info.SetValue(source, pipeGetters[info.FieldType].Invoke(target.Pipes, pipeName));
		}

		public void RegisterPlugin(Plugin plugin)
		{
			plugin.Pipes = new PluginPipes(plugin);
			foreach (EventPipe pipe in plugin.Pipes.GetEvents())
			{
				if (!events.ContainsKey(pipe.EventName))
				{
					events.Add(pipe.EventName, new List<EventPipe>());
				}

				events[pipe.EventName].Add(pipe);
			}
		}

		public void UnregisterPlugin(Plugin plugin)
		{
			foreach (EventPipe pipe in plugin.Pipes.GetEvents())
			{
				if (events.ContainsKey(pipe.EventName))
				{
					events[pipe.EventName].Remove(pipe);
				}
			}
		}

		public void RegisterLinks(Plugin plugin)
		{
			Type type = plugin.GetType();
			if (linkFields.ContainsKey(type))
			{
				return;
			}

			FieldInfo[] infos = type.GetFields(AllMembers);
			linkFields.Add(type, infos);

			foreach (FieldInfo info in infos)
			{
				PipeLink link = info.GetCustomAttribute<PipeLink>();
				if (link != null)
				{
					PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Linking {info.Name} of {plugin.Details.id} to {link.Pipe} of {link.Plugin}");
					SetPipeLink(plugin, info, link.Plugin, link.Pipe);
				}
			}
		}

		public void UnregisterLinks(Plugin plugin)
		{
			Type type = plugin.GetType();
			if (linkFields.ContainsKey(type))
			{
				foreach (FieldInfo info in linkFields[type])
				{
					PipeLink link = info.GetCustomAttribute<PipeLink>();
					if (link != null)
					{
						PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Unlinking {info.Name} of {plugin.Details.id} from {link.Pipe} of {link.Plugin}");
						info.SetValue(plugin, null);
					}
				}
			}

			if (linkReferences.ContainsKey(type))
			{
				PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Unlinking all references to {plugin.Details.id}");

				foreach (FieldInfo info in linkReferences[type])
				{
					PipeLink link = info.GetCustomAttribute<PipeLink>();
					if (link != null)
					{
						info.SetValue(plugin, null);
					}
				}
			}
		}

		internal void InvokeEvent(string eventName, string caller, object[] parameters)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException(nameof(eventName));
			}

			if (!events.ContainsKey(eventName))
			{
				return;
			}

			foreach (EventPipe pipe in events[eventName])
			{
				// Skip if event pipe is disabled OR specific to one plugin AND the scope is not the same as the invoker
				if (PluginManager.Manager.GetDisabledPlugin(pipe.Source.Details.id) == null || pipe.PluginScope != null && !pipe.PluginScope.Contains(caller))
				{
					continue;
				}

				pipe.Invoke(parameters, caller);
			}
		}
	}
}
