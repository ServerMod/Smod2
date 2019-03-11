using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Smod2.Piping
{
	public class PipeManager
	{
		private const BindingFlags AllMembers = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		private readonly List<Plugin> registered;
		
		private readonly Dictionary<Plugin, List<FieldInfo>> linkFields;
		private readonly Dictionary<Plugin, Dictionary<Plugin, List<FieldInfo>>> linkFieldReferences;
		
		private readonly Dictionary<string, List<EventPipe>> events;
		private readonly Dictionary<Type, Func<PluginPipes, string, object>> pipeGetters;

		private static PipeManager manager;
		public static PipeManager Manager => manager ?? (manager = new PipeManager());

		public PipeManager()
		{
			registered = new List<Plugin>();
			
			linkFields = new Dictionary<Plugin, List<FieldInfo>>();
			linkFieldReferences = new Dictionary<Plugin, Dictionary<Plugin, List<FieldInfo>>>();
			
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

		public bool IsRegistered(Plugin plugin) => registered.Contains(plugin);
		
		private void SetPipeLink(Plugin source, FieldInfo info, string pluginId, string pipeName)
		{
			Plugin target = PluginManager.Manager.GetEnabledPlugin(pluginId);
			if (target == null)
			{
				return;
			}

			Type fieldType = info.FieldType;
			if (!pipeGetters.ContainsKey(fieldType))
			{
				// Set type of field to the base (non-generic type) if the generic is a MemberPipe
				if (typeof(MemberPipe).IsAssignableFrom(fieldType))
				{
					fieldType = fieldType.BaseType;
				}

				if (fieldType == null || !pipeGetters.ContainsKey(fieldType))
				{
					PluginManager.Manager.Logger.Error("PIPE_MANAGER", $"{info.Name} of {source.Details.id} tried to link to a non-existant pipe type: {info.FieldType}");
					return;	
				}
			}

			if (!linkFieldReferences.ContainsKey(target))
			{
				linkFieldReferences.Add(target, new Dictionary<Plugin, List<FieldInfo>>());
			}
			Dictionary<Plugin, List<FieldInfo>> references = linkFieldReferences[target];

			if (!references.ContainsKey(source))
			{
				references.Add(source, new List<FieldInfo>());
			}
			references[source].Add(info);

			info.SetValue(source, Convert.ChangeType(pipeGetters[fieldType].Invoke(target.Pipes, pipeName), info.FieldType));
		}

		public void RegisterPlugin(Plugin plugin)
		{
			if (registered.Contains(plugin))
			{
				throw new InvalidOperationException($"The plugin is already registered to PipeManager.");
			}
			
			foreach (EventPipe pipe in plugin.Pipes.GetEvents())
			{
				if (!events.ContainsKey(pipe.EventName))
				{
					events.Add(pipe.EventName, new List<EventPipe>());
				}

				events[pipe.EventName].Add(pipe);
			}
			
			registered.Add(plugin);
		}

		public void UnregisterPlugin(Plugin plugin)
		{
			if (!registered.Contains(plugin))
			{
				return;
			}
			
			foreach (EventPipe pipe in plugin.Pipes.GetEvents())
			{
				if (events.ContainsKey(pipe.EventName))
				{
					events[pipe.EventName].Remove(pipe);
				}
			}
			
			UnregisterLinks(plugin);
			
			registered.Remove(plugin);
		}

		public List<string> GetLinkDependencies(Plugin plugin)
		{
			Type type = plugin.GetType();
			List<string> dependencies = new List<string>();
			
			foreach (MemberInfo info in type.GetMembers(AllMembers))
			{
				PipeLink link = info.GetCustomAttribute<PipeLink>();
				if (!dependencies.Contains(link.Plugin))
				{
					dependencies.Add(link.Plugin);
				}
			}

			return dependencies;
		}
		
		public void RegisterLinks(Plugin plugin)
		{
			Type type = plugin.GetType();

			List<FieldInfo> fields = new List<FieldInfo>();
			foreach (FieldInfo info in type.GetFields(AllMembers))
			{
				PipeLink link = info.GetCustomAttribute<PipeLink>();
				if (link != null)
				{
					PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Linking {info.Name} of {plugin.Details.id} to {link.Pipe} of {link.Plugin}.");
					SetPipeLink(plugin, info, link.Plugin, link.Pipe);
				}
			}
			linkFields.Add(plugin, fields);
		}

		public void UnregisterLinks(Plugin plugin)
		{
			foreach (FieldInfo info in linkFields[plugin])
			{
				PipeLink link = info.GetCustomAttribute<PipeLink>();
				if (link != null)
				{
					PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Unlinking {info.Name} of {plugin.Details.id} from {link.Pipe} of {link.Plugin}");
					info.SetValue(plugin, null);
				}
			}
			linkFields.Remove(plugin);

			if (linkFieldReferences.ContainsKey(plugin))
			{
				PluginManager.Manager.Logger.Debug("PIPE_MANAGER", $"Unlinking all field references to {plugin.Details.id}");

				foreach (KeyValuePair<Plugin, List<FieldInfo>> infos in linkFieldReferences[plugin])
				{
					foreach (FieldInfo info in infos.Value)
					{
						PipeLink link = info.GetCustomAttribute<PipeLink>();
						if (link != null)
						{
							info.SetValue(infos.Key, null);
						}	
					}
				}
			}
			linkFieldReferences.Remove(plugin);
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
				// Skip if event pipe is disabled
				if (PluginManager.Manager.GetDisabledPlugin(pipe.Source.Details.id) != null)
				{
					continue;
				}
				
				// Skip if event pipe is specific to certain plugins AND the scope does not contain the invoker
				string[] pluginScope = pipe.GetPluginScope();
				if (pluginScope.Length > 0 && !pluginScope.Contains(caller))
				{
					continue;
				}

				pipe.Invoke(parameters, caller);
			}
		}
	}
}
