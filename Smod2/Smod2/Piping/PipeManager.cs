﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Smod2.Piping
{
	public class PipeManager
	{
		private readonly Dictionary<Type, FieldInfo[]> links;
		private readonly Dictionary<string, List<EventPipe>> events;
		private readonly Dictionary<Type, Func<PluginPipes, string, object>> pipeGetters;

		private static PipeManager manager;
		public static PipeManager Manager => manager ?? (manager = new PipeManager());

		public PipeManager()
		{
			links = new Dictionary<Type, FieldInfo[]>();
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
				PluginManager.Manager.Logger.Error("PIPE_MANAGER", source + " tried to link to a non-existant pipe type: " + info.FieldType);
				return;
			}

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
			if (links.ContainsKey(type))
			{
				return;
			}

			FieldInfo[] infos = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			links.Add(type, infos);

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
			if (!links.ContainsKey(type))
			{
				return;
			}

			foreach (FieldInfo info in links[type])
			{
				PipeLink link = info.GetCustomAttribute<PipeLink>();
				if (link != null)
				{
					PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Unlinking {info.Name} of {plugin.Details.id} from {link.Pipe} of {link.Plugin}");
					info.SetValue(plugin, null);
				}
			}
		}

		internal void InvokeEvent(string eventName, string caller, object[] parameters)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException(nameof(eventName));
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
