using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.API;
using Smod2.Logging;
using Smod2.Events;
using Smod2.Piping;

namespace Smod2
{
	public class PluginManager
	{
		public static readonly int SMOD_MAJOR = 3;
		public static readonly int SMOD_MINOR = 3;
		public static readonly int SMOD_REVISION = 1;
		public static readonly string SMOD_BUILD = "A";

		public static readonly string DEPENDENCY_FOLDER = "dependencies";

		public static String GetSmodVersion()
		{
			return String.Format("{0}.{1}.{2}", SMOD_MAJOR, SMOD_MINOR, SMOD_REVISION);
		}

		public static String GetSmodBuild()
		{
			return SMOD_BUILD;
		}

		private Dictionary<string, Plugin> enabledPlugins;
		private Dictionary<string, Plugin> disabledPlugins;

		public List<Plugin> EnabledPlugins
		{
			get
			{
				List<Plugin> plugins = new List<Plugin>();
				foreach (var entry in enabledPlugins)
				{
					plugins.Add(entry.Value);
				}
				return plugins;
			}
		}

		public List<Plugin> DisabledPlugins
		{
			get
			{
				List<Plugin> plugins = new List<Plugin>();
				foreach (var entry in disabledPlugins)
				{
					plugins.Add(entry.Value);
				}
				return plugins;
			}
		}

		public List<Plugin> Plugins
		{
			get
			{
				List<Plugin> plugins = new List<Plugin>();
				foreach (var entry in disabledPlugins)
				{
					plugins.Add(entry.Value);
				}
				foreach (var entry in enabledPlugins)
				{
					plugins.Add(entry.Value);
				}
				return plugins;
			}
		}


		private ICommandManager commandManager;
		public ICommandManager CommandManager
		{
			get { return commandManager; }
			set
			{
				if (commandManager == null)
				{
					commandManager = value;
				}
			}
		}

		private Server server;
		public Server Server
		{
			get { return server; }
			set
			{
				if (server == null)
				{
					server = value;
				}
			}
		}

		private Logger logger;
		public Logger Logger
		{
			get { return logger; }
			set
			{
				if (logger == null)
				{
					logger = value;
				}
			}
		}

		private static PluginManager singleton;
		public static PluginManager Manager
		{
			get
			{
				if (singleton == null)
				{
					singleton = new PluginManager();
				}

				return singleton;
			}
		}

		public PluginManager()
		{
			enabledPlugins = new Dictionary<string, Plugin>();
			disabledPlugins = new Dictionary<string, Plugin>();
		}

		public Plugin GetEnabledPlugin(string id)
		{
			enabledPlugins.TryGetValue(id, out Plugin plugin);
			return plugin;
		}

		public Plugin GetDisabledPlugin(string id)
		{
			disabledPlugins.TryGetValue(id, out Plugin plugin);
			return plugin;
		}

		public List<Plugin> FindEnabledPlugins(string name)
		{
			List<Plugin> matching = new List<Plugin>();
			foreach (var plugin in enabledPlugins.Values)
			{
				if (plugin.Details.name.Contains(name) || plugin.Details.author.Contains(name))
				{
					matching.Add(plugin);
				}
			}

			return matching;
		}

		public List<Plugin> FindDisabledPlugins(string name)
		{
			List<Plugin> matching = new List<Plugin>();
			foreach (var plugin in disabledPlugins.Values)
			{
				if (plugin.Details.name.Contains(name) || plugin.Details.author.Contains(name))
				{
					matching.Add(plugin);
				}
			}

			return matching;
		}

		public void EnablePlugins()
		{
			// .ToArray() prevents a collection modified exception (enabling a plugin removes it from disabled plugins)
			foreach (KeyValuePair<string, Plugin> plugin in disabledPlugins.ToArray())
			{
				EnablePlugin(plugin.Value);
			}
		}

		public void EnablePlugin(Plugin plugin)
		{
			plugin.Info("Enabling plugin " + plugin.Details.name + " " + plugin.Details.version);
			PipeManager.Manager.RegisterLinks(plugin);
			ConfigManager.Manager.RegisterPlugin(plugin);
			EventManager.Manager.AddSnapshotEventHandlers(plugin);
			CommandManager.ReregisterPlugin(plugin);

			plugin.OnEnable();
			disabledPlugins.Remove(plugin.Details.id);
			enabledPlugins.Add(plugin.Details.id, plugin);
		}

		public void DisablePlugins()
		{
			// .ToArray() prevents a collection modified exception (disabling a plugin removes it from enabled plugins)
			foreach (KeyValuePair<string, Plugin> plugin in enabledPlugins.ToArray())
			{
				DisablePlugin(plugin.Value);
			}
		}

		public void DisablePlugin(String id)
		{
			Dictionary<String, Plugin> newEnabled = new Dictionary<string, Plugin>();
			foreach (var plugin in enabledPlugins)
			{
				if (plugin.Value.Details.id == id)
				{
					DisablePlugin(plugin.Value);
				}
				else
				{
					newEnabled.Add(plugin.Key, plugin.Value);
				}
			}
			enabledPlugins = newEnabled;
		}

		public void DisablePlugin(Plugin plugin)
		{
			plugin.OnDisable();
			enabledPlugins.Remove(plugin.Details.id);
			disabledPlugins.Add(plugin.Details.id, plugin);

			CommandManager.UnregisterCommands(plugin);
			EventManager.Manager.RemoveEventHandlers(plugin);
			ConfigManager.Manager.UnloadPlugin(plugin);
			PipeManager.Manager.UnregisterPlugin(plugin);
		}

		public void LoadPlugins(String dir)
		{
			LoadDirectoryPlugins(dir);
			LoadPluginAssemblies(dir);
		}

		public void LoadPluginAssemblies(string dir)
		{
			string[] files = Directory.GetFiles(dir);
			foreach (string file in files)
			{
				if (file.EndsWith(".dll"))
				{
					Logger.Debug("PLUGIN_LOADER", file);
					LoadPluginAssembly(file);
				}
			}
		}

		public void LoadDirectoryPlugins(String pluginDirectory)
		{
			string dependency_folder = pluginDirectory + DEPENDENCY_FOLDER;
			if (Directory.Exists(dependency_folder))
			{
				string[] dependencies = Directory.GetFiles(dependency_folder);
				foreach (String dependency in dependencies)
				{
					if (dependency.Contains(".dll"))
					{
						Logger.Info("PLUGIN_LOADER", "Loading plugin dependency: " + dependency);
						try
						{
							this.LoadAssembly(dependency);
						}
						catch (Exception e)
						{
							Logger.Warn("PLUGIN_LOADER", "Failed to load dependency: " + dependency);
							Logger.Debug("PLUGIN_LOADER", e.Message);
							Logger.Debug("PLUGIN_LOADER", e.StackTrace);
						}
					}
				}
			}
			else
			{
				Logger.Debug("PLUGIN_LOADER", "No dependencies for directory: " + dependency_folder);
			}
		}

		public void LoadAssembly(string path)
		{
			Assembly a = Assembly.LoadFrom(path);
		}

		public void LoadPluginAssembly(string path)
		{
			Logger.Debug("PLUGIN_LOADER", path);
			Assembly a = Assembly.LoadFrom(path);
			try
			{
				foreach (Type t in a.GetTypes())
				{
					if (t.IsSubclassOf(typeof(Plugin)) && t != typeof(Plugin))
					{
						try
						{
							Plugin plugin = (Plugin)Activator.CreateInstance(t);
							PluginDetails details = (PluginDetails)Attribute.GetCustomAttribute(t, typeof(PluginDetails));
							if (details.id != null)
							{
								if (details.SmodMajor != SMOD_MAJOR && details.SmodMinor != SMOD_MINOR)
								{
									Logger.Warn("PLUGIN_LOADER", "Trying to load an outdated plugin " + details.name + " " + details.version);
								}
								else
								{
									plugin.Details = details;
									ConfigManager.Manager.RegisterPlugin(plugin);
									PipeManager.Manager.RegisterPlugin(plugin);
									plugin.Register();

									disabledPlugins.Add(details.id, plugin);
									Logger.Info("PLUGIN_LOADER", "Plugin loaded: " + plugin.ToString());
								}

							}
							else
							{
								Logger.Warn("PLUGIN_LOADER", "Plugin loaded but missing an id: " + t + "[" + path + "]");
							}
						}
						catch (Exception e)
						{
							Logger.Error("PLUGIN_LOADER", "Failed to create instance of plugin " + t + "[" + path + "]");
							Logger.Error("PLUGIN_LOADER", e.GetType().Name + ": " + e.Message);
							Logger.Error("PLUGIN_LOADER", e.StackTrace);
						}
					}
				}
			}
			catch (Exception e)
			{
				Logger.Error("PLUGIN_LOADER", "Failed to load DLL [" + path + "], is it up to date?");
				Logger.Debug("PLUGIN_LOADER", e.Message);
				Logger.Debug("PLUGIN_LOADER", e.StackTrace);
			}
		}
	}
}
