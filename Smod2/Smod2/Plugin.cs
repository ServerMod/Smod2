using System;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.Config;
using Smod2.Events;
using System.Collections.Generic;

namespace Smod2
{
    public abstract class Plugin
    {
		public PluginDetails Details
		{
			get;
			set;
		}

		public readonly EventManager eventManager = EventManager.Manager;
		public readonly PluginManager pluginManager = PluginManager.Manager;
		public abstract void Register();
		public abstract void OnEnable();
		public abstract void OnDisable();

		public void AddEventHandler(Type eventType, IEvent handler, Priority priority=Priority.Normal)
		{
			eventManager.AddEventHandler(this, eventType, handler, priority);
		}

		public void AddCommand(string command, ICommandHandler handler)
		{
			if (PluginManager.Manager.CommandManager == null)
			{
				this.Error("Failed to register command handler becuase the command manager is null");
			}
			else
			{
				this.Debug("Command handler registered for command " + command);
				PluginManager.Manager.CommandManager.RegisterCommand(this, command, handler);
			}
		}

		public void AddConfig(ConfigSetting setting)
		{
			ConfigManager.Manager.RegisterConfig(this, setting);
		}

		private void CheckConfigRegistered(string key)
		{
			if (!ConfigManager.Manager.IsRegistered(this, key))
			{
				this.Warn("Trying to access a config setting that isnt registered to the plugin, this is bad practice.");
			}
		}

		public string GetConfigString(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			string def = ConfigManager.Manager.ResolveDefault(key.ToUpper());
			return ConfigManager.Manager.Config.GetStringValue(key.ToUpper(), def);
		}

		public int GetConfigInt(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			Int32.TryParse(ConfigManager.Manager.ResolveDefault(key.ToUpper()), out int def);
			return ConfigManager.Manager.Config.GetIntValue(key.ToUpper(), def);
		}

		public bool GetConfigBool(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			bool.TryParse(ConfigManager.Manager.ResolveDefault(key.ToUpper()), out bool def);
			return ConfigManager.Manager.Config.GetBoolValue(key.ToUpper(), def);
		}

		public string[] GetConfigList(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			return ConfigManager.Manager.Config.GetListValue(key.ToUpper());
		}

		public int[] GetConfigIntList(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			return ConfigManager.Manager.Config.GetIntListValue(key.ToUpper());
		}

		public Dictionary<string, string> GetConfigDict(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			return ConfigManager.Manager.Config.GetDictValue(key.ToUpper());
		}

		public Dictionary<int, int> GetConfigIntDict(string key)
		{
			CheckConfigRegistered(key.ToUpper());
			return ConfigManager.Manager.Config.GetIntDictValue(key.ToUpper());
		}

		public void Debug(string message)
		{
			PluginManager.Manager.Logger.Debug(this.Details.id, message);
		}

		public void Info(string message)
		{
			PluginManager.Manager.Logger.Info(this.Details.id, message);
		}

		public void Warn(string message)
		{
			PluginManager.Manager.Logger.Warn(this.Details.id, message);
		}

		public void Error(string message)
		{
			PluginManager.Manager.Logger.Error(this.Details.id, message);
		}

		public override string ToString()
		{
			if (Details == null)
			{
				return base.ToString();
			}
			return Details.name + "(" + Details.id + ")";
		}
	}
}
