using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.API;
using Smod2.Logging;
using Smod2.Events;
using Smod2.Piping;
using Smod2.Permissions;

namespace Smod2
{
	[Flags]
	public enum SearchFlags
	{
		NAME = 1 << 0,
		AUTHOR = 1 << 1,
		ID = 1 << 2,
		DESCRIPTION = 1 << 3,
		VERSION = 1 << 4
	}
	
	public class PluginManager
	{
		public static readonly int SMOD_MAJOR = 3;
		public static readonly int SMOD_MINOR = 4;
		public static readonly int SMOD_REVISION = 0;
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

		private PermissionsManager permissionsManager;
		public PermissionsManager PermissionsManager
		{
			get { return permissionsManager; }
			set
			{
				if (permissionsManager == null)
				{
					permissionsManager = value;
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
			permissionsManager = new PermissionsManager();
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

		public Plugin GetPlugin(string id)
		{
			return GetEnabledPlugin(id) ?? GetDisabledPlugin(id);
		}

		
		private List<Plugin> GetPluginsOfSearchable(IEnumerable<Plugin> searchable, string query, SearchFlags flags)
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query));
			}
			
			Array searchFlags = Enum.GetValues(typeof(SearchFlags));
			List<Plugin> plugins = new List<Plugin>();
			foreach (Plugin plugin in searchable)
			{
				if (plugin.Details == null)
				{
					continue;
				}

				foreach (SearchFlags flag in searchFlags)
				{
					// Don't check for match if flag is not specified in flags.
					if (!flags.HasFlag(flag))
					{
						continue;
					}

					bool match;
					switch (flag)
					{
						case SearchFlags.ID:
							match = plugin.Details.id == query;
							break;
					
						case SearchFlags.NAME:
							match = plugin.Details.name == query;
							break;
					
						case SearchFlags.AUTHOR:
							match = plugin.Details.author == query;
							break;
					
						case SearchFlags.VERSION:
							match = plugin.Details.version == query;
							break;
					
						case SearchFlags.DESCRIPTION:
							match = plugin.Details.description?.Length / 3 <= query.Length && plugin.Details.description.Contains(query);
							break;
						
						// It should never get here but it won't compile otherwise.
						default:
							match = false;
							break;
					}

					if (match)
					{
						plugins.Add(plugin);
						break;
					}
				}
			}

			return plugins;
		}

		public List<Plugin> GetEnabledPlugins(string query, SearchFlags flags) => GetPluginsOfSearchable(enabledPlugins.Values, query, flags);
		public List<Plugin> GetDisabledPlugins(string query, SearchFlags flags) => GetPluginsOfSearchable(disabledPlugins.Values, query, flags);

		public List<Plugin> GetMatchingPlugins(string query, SearchFlags flags)
		{
			List<Plugin> plugins = GetPluginsOfSearchable(enabledPlugins.Values, query, flags);
			foreach (Plugin plugin in GetPluginsOfSearchable(disabledPlugins.Values, query, flags))
			{
				if (!plugins.Contains(plugin))
				{
					plugins.Add(plugin);
				}
			}

			return plugins;
		}

		[Obsolete("Use GetEnabledPlugins instead.")]
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

		[Obsolete("Use GetDisabledPlugins instead.")]
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

		[Obsolete("Use GetMatchingPlugins instead.")]
		public List<Plugin> FindPlugins(string name)
		{
			List<Plugin> matching = new List<Plugin>();
			
			foreach (var plugin in enabledPlugins.Values)
			{
				if (plugin.Details.name.Contains(name) || plugin.Details.author.Contains(name))
				{
					matching.Add(plugin);
				}
			}
			
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
			// Converting to array is required because disabledPlugins is modified when a plugin is enabled.
			Plugin[] plugins = new Plugin[disabledPlugins.Count];
			disabledPlugins.Values.CopyTo(plugins, 0);
			
			foreach (Plugin plugin in plugins)
			{
				EnablePlugin(plugin);
			}
		}

		public void EnablePlugin(Plugin plugin)
		{
			if (enabledPlugins.ContainsValue(plugin))
			{
				return;
			}
			
			Manager.Logger.Info("PLUGIN_MANAGER", "Enabling plugin " + plugin.Details.name + " " + plugin.Details.version);
			
			Manager.Logger.Debug("PLUGIN_MANAGER", "Registering pipe exports");
			if (!PipeManager.Manager.IsRegistered(plugin)) PipeManager.Manager.RegisterPlugin(plugin); // In case it got disabled
			Manager.Logger.Debug("PLUGIN_MANAGER", "Registering pipe imports");
			PipeManager.Manager.RegisterLinks(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Registering configs");
			if (!ConfigManager.Manager.IsRegistered(plugin)) ConfigManager.Manager.RegisterPlugin(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Registering langs");
			LangManager.Manager.RegisterPlugin(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Loading event snapshot");
			EventManager.Manager.AddSnapshotEventHandlers(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Loading command snapshot");
			CommandManager.ReregisterPlugin(plugin);

			Manager.Logger.Debug("PLUGIN_MANAGER", "Invoking OnEnable");
			plugin.OnEnable();
			
			Manager.Logger.Debug("PLUGIN_MANAGER", "Altering dictionaries");
			disabledPlugins.Remove(plugin.Details.id);
			enabledPlugins.Add(plugin.Details.id, plugin);
			
			Manager.Logger.Info("PLUGIN_MANAGER", "Enabled plugin  " + plugin.Details.name + " " + plugin.Details.version);
		}

		public void DisablePlugins()
		{
			Plugin[] plugins = new Plugin[disabledPlugins.Count];
			disabledPlugins.Values.CopyTo(plugins, 0);
			
			foreach (Plugin plugin in plugins)
			{
				DisablePlugin(plugin);
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
			if (disabledPlugins.ContainsValue(plugin))
			{
				return;
			}
			
			Manager.Logger.Info("PLUGIN_MANAGER", "Disabling plugin " + plugin.Details.name + " " + plugin.Details.version);
			
			Manager.Logger.Debug("PLUGIN_MANAGER", "Altering dictionaries");
			enabledPlugins.Remove(plugin.Details.id);
			disabledPlugins.Add(plugin.Details.id, plugin);
			
			Manager.Logger.Debug("PLUGIN_MANAGER", "Invoking OnDisable");
			plugin.OnDisable();

			Manager.Logger.Debug("PLUGIN_MANAGER", "Unloading commands");
			CommandManager.UnregisterCommands(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Unloading event handlers");
			EventManager.Manager.RemoveEventHandlers(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Unloading configs");
			ConfigManager.Manager.UnloadPlugin(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Unloading translations");
			LangManager.Manager.UnregisterPlugin(plugin);
			Manager.Logger.Debug("PLUGIN_MANAGER", "Unloading pipe imports/exports");
			PipeManager.Manager.UnregisterPlugin(plugin);
			Manager.Logger.Info("PLUGIN_MANAGER", "Disabled plugin " + plugin.Details.name + " " + plugin.Details.version);
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
									plugin.Pipes = new PluginPipes(plugin);
									
									ConfigManager.Manager.RegisterPlugin(plugin);
									LangManager.Manager.RegisterPlugin(plugin);
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
		
		public static string ToUpperSnakeCase(string otherCase)
		{
			string snakeCase = "";
			for (int i = 0; i < otherCase.Length; i++)
			{
				if (snakeCase.Length == 0 && otherCase[i] == '_')
				{
					continue;
				}
				
				if (i > 0 && char.IsUpper(otherCase[i]) && otherCase[i - 1] != '_')
				{
					snakeCase += "_" + otherCase[i];
				}
				else
				{
					snakeCase += char.ToUpper(otherCase[i]);	
				}
			}

			return snakeCase;
		}

		public void RefreshPluginAttributes()
		{
			foreach (Plugin plugin in enabledPlugins.Values)
			{
				ConfigManager.Manager.RefreshAttributes(plugin);
				LangManager.Manager.RefreshAttributes(plugin);
			}
		}
	}
}
