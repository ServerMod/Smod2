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

			string def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is string)
			{
				def = (string) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetStringValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return "";
		}

		public int GetConfigInt(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			int def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is int)
			{
				def = (int)ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetIntValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return -1;
		}

		public float GetConfigFloat(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			float def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is float)
			{
				def = (float) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetFloatValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return -1;
		}

		public bool GetConfigBool(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			bool def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is bool)
			{
				def = (bool) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetBoolValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return false;
		}

		public string[] GetConfigList(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			string[] def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is string[])
			{
				def = (string[]) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetListValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return new string[] { };
		}

		public int[] GetConfigIntList(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			int[] def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is int[])
			{
				def = (int[]) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetIntListValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return new int[] { };
		}

		public Dictionary<string, string> GetConfigDict(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			Dictionary<string, string> def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is Dictionary<string, string>)
			{
				def = (Dictionary<string, string>) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetDictValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return new Dictionary<string, string>();
		}

		public Dictionary<int, int> GetConfigIntDict(string key)
		{
			CheckConfigRegistered(key.ToUpper());

			Dictionary<int, int> def;
			if (ConfigManager.Manager.ResolveDefault(key.ToUpper()) is Dictionary<int, int>)
			{
				def = (Dictionary<int, int>) ConfigManager.Manager.ResolveDefault(key.ToUpper());

				bool randomValues = (ConfigManager.Manager.ResolvePrimary(key.ToUpper()) != null ? ConfigManager.Manager.ResolvePrimary(key.ToUpper()).RandomizedValue : false);
				return ConfigManager.Manager.Config.GetIntDictValue(key.ToUpper(), def: def, randomValues: randomValues);
			}

			return new Dictionary<int, int>();
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
