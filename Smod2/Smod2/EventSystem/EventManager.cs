using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Smod2.EventHandlers;

namespace Smod2.Events
{


	public enum Priority {Highest = 100, High = 80, Normal = 50, Low = 20, Lowest = 0};

	public class EventManager
	{
		private static EventManager singleton;
		
		public static EventManager Manager
		{
			get
			{
				if (singleton == null)
				{
					singleton = new EventManager();
				}
				return singleton;
			}
		}

		private static PriorityComparator priorityCompare = new PriorityComparator();
		private Dictionary<Type, List<EventHandlerWrapper>> event_meta;
		private Dictionary<Type, List<IEventHandler>> event_handlers;
		private readonly Dictionary<Plugin, Snapshot> snapshots;

		public EventManager()
		{
			event_meta = new Dictionary<Type, List<EventHandlerWrapper>>();
			event_handlers = new Dictionary<Type, List<IEventHandler>>();
			snapshots = new Dictionary<Plugin, Snapshot>();
		}


		public void HandleEvent<T>(Event ev)
		{
			var list = this.GetEventHandlers<T>();

			foreach(IEventHandler handler in list)
			{
				try
				{
					ev.ExecuteHandler(handler);
				} catch (Exception e)
				{
					PluginManager.Manager.Logger.Error("Event", "Event Handler: " + handler.GetType().ToString() + " Failed to handle event:" + ev.GetType().ToString());
					PluginManager.Manager.Logger.Error("Event", e.ToString());
				}
			}
		}

		public void AddEventHandlers(Plugin plugin, IEventHandler handler, Priority priority = Priority.Normal)
		{
			foreach(Type intfce in handler.GetType().GetInterfaces()) {
				if (typeof(IEventHandler).IsAssignableFrom(intfce))
				{
					plugin.Debug("Adding event handler for " + intfce.Name);
					AddEventHandler(plugin, intfce, handler, priority);
				}
			}
		}

		public void AddEventHandler(Plugin plugin, Type eventType, IEventHandler handler, Priority priority = Priority.Normal)
		{
			plugin.Debug(string.Format("Adding event handler from: {0} type: {1} priority: {2} handler: {3}", plugin.Details.name, eventType, priority, handler.GetType()));
			EventHandlerWrapper wrapper = new EventHandlerWrapper(plugin, priority, handler);

			// If the plugin is not enabled
			if (PluginManager.Manager.GetEnabledPlugin(plugin.Details?.id) == null)
			{
				if (!snapshots.ContainsKey(plugin))
				{
					snapshots.Add(plugin, new Snapshot());
				}
				snapshots[plugin].Entries.Add(new Snapshot.SnapshotEntry(eventType, wrapper));
			}

			AddEventMeta(eventType, wrapper, handler);
		}

		private void AddEventMeta(Type eventType, EventHandlerWrapper wrapper, IEventHandler handler)
		{
			if (!event_meta.ContainsKey(eventType))
			{
				event_meta.Add(eventType, new List<EventHandlerWrapper>());
				event_handlers.Add(eventType, new List<IEventHandler>());

				event_meta[eventType].Add(wrapper);
				event_handlers[eventType].Add(handler);
			}
			else
			{
				List<EventHandlerWrapper> meta = event_meta[eventType];
				meta.Add(wrapper);
				// Doing this stuff on register instead of when the event is called for events that trigger lots (OnUpdate etc)
				meta.Sort(priorityCompare);
				meta.Reverse();
				RebuildHandlerList(eventType);
			}
		}

		public void AddSnapshotEventHandlers(Plugin plugin)
		{
			if (!snapshots.ContainsKey(plugin) || !snapshots[plugin].Active)
			{
				return;
			}

			snapshots[plugin].Active = false;
			foreach (Snapshot.SnapshotEntry entry in snapshots[plugin].Entries)
			{
				AddEventMeta(entry.Type, entry.Wrapper, entry.Wrapper.Handler);
			}
		}

		public void RemoveEventHandlers(Plugin plugin)
		{
			Dictionary<Type, List<EventHandlerWrapper>> new_event_meta = new Dictionary<Type, List<EventHandlerWrapper>>();
			// loop through the meta dict finding any handlers from this plugin
			foreach (var meta in event_meta)
			{
				List<EventHandlerWrapper> newList = new List<EventHandlerWrapper>();

				foreach (EventHandlerWrapper wrapper in meta.Value)
				{
					if (wrapper.Plugin != plugin)
					{
						newList.Add(wrapper);
					}
				}
				
				if (newList.Count > 0)
				{
					new_event_meta.Add(meta.Key, newList);
				}
			}
			
			event_meta = new_event_meta;
			// rebuild handler list for each type
			foreach (var meta in event_meta)
			{
				RebuildHandlerList(meta.Key);
			}

			if (snapshots.ContainsKey(plugin))
			{
				snapshots[plugin].Active = true;
			}
		}

		private void RebuildHandlerList(Type eventType)
		{
			List<EventHandlerWrapper> meta = event_meta[eventType];
			List<IEventHandler> handlers = new List<IEventHandler>();
			foreach (EventHandlerWrapper metaDetails in meta)
			{
				handlers.Add(metaDetails.Handler);
			}

			if (event_handlers.ContainsKey(eventType))
			{
				event_handlers[eventType] = handlers;
			}
			else
			{
				event_handlers.Add(eventType, handlers);
			}
		}


		public List<T> GetEventHandlers<T>()
		{
			List<T> events;
			if (event_handlers.ContainsKey(typeof(T)))
			{
				events = event_handlers[typeof(T)].Cast<T>().ToList();
			}
			else
			{
				events = new List<T>();
			}

			return events;
		}

		private class PriorityComparator : IComparer<EventHandlerWrapper>
		{
			public int Compare(EventHandlerWrapper x, EventHandlerWrapper y)
			{
				return x.Priority.CompareTo(y.Priority);
			}
		}

		private class Snapshot
		{
			public List<SnapshotEntry> Entries { get; private set; }
			public bool Active { get; set; }

			public Snapshot()
			{
				Entries = new List<SnapshotEntry>();
			}

			public class SnapshotEntry
			{
				public Type Type { get; }
				public EventHandlerWrapper Wrapper { get; }

				public SnapshotEntry(Type type, EventHandlerWrapper wrapper)
				{
					Type = type;
					Wrapper = wrapper;
				}
			}
		}
	}
	
	public class EventHandlerWrapper
	{
		public Priority Priority { get; }
		public IEventHandler Handler { get; }
		public Plugin Plugin { get; }

		public EventHandlerWrapper(Plugin plugin, Priority priority, IEventHandler handler)
		{
			this.Plugin = plugin;
			this.Priority = priority;
			this.Handler = handler;
		}
	}
}
