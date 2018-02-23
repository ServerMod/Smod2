using System;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.Events;

namespace Smod2
{
    public abstract class Plugin
    {
		public PluginDetails Details
		{
			get;
			set;
		}

		public readonly EventManager eventManager = EventManager.GetEventManager();
		public readonly PluginManager pluginManager = PluginManager.GetPluginManager();
		public abstract void Register();
		public abstract void OnEnable();
		public abstract void OnDisable();

		public void AddEventHandler(Type eventType, IEvent handler, Priority priority=Priority.Normal)
		{
			eventManager.AddEventHandler(this, eventType, handler, priority);
		}

		public void AddCommand(String command, ICommandHandler handler)
		{
			if (PluginManager.GetPluginManager().CommandManager == null)
			{

			}
			else
			{
				PluginManager.GetPluginManager().CommandManager.RegisterCommand(this, command, handler);
			}
		}

		public void Debug(String message)
		{
			PluginManager.GetPluginManager().Logger.Debug(this, message);
		}

		public void Info(String message)
		{
			PluginManager.GetPluginManager().Logger.Info(this, message);
		}
	}
}
