using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.API;
using Smod2.Logging;

namespace Smod2
{
    public class PluginManager
    {
        public static readonly int SMOD_MAJOR = 3;
        public static readonly int SMOD_MINOR = 0;
        public static readonly int SMOD_REVISION = 0;

        public static String GetSmodVersion()
        {
            return String.Format("{0}.{1}.{2}", SMOD_MAJOR, SMOD_MINOR, SMOD_FIX);
        }

		private Dictionary<string, Plugin> plugins;

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
			plugins = new Dictionary<string, Plugin>();
		}

		public Plugin GetPlugin(string id)
		{
			plugins.TryGetValue(id, out Plugin plugin);
			return plugin;
		}

		public List<Plugin> FindPlugins(string name)
		{
			List<Plugin> matching = new List<Plugin>();
			foreach (var plugin in plugins.Values)
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
			foreach(var plugin in plugins)
			{
				plugin.Value.Info("Enabling plugin " + plugin.Value.Details.name + " " + plugin.Value.Details.version);
				ConfigManager.Manager.RegisterPlugin(plugin.Value);
				plugin.Value.Register();
				plugin.Value.OnEnable();
			}
		}

		public void LoadAssemblies(string dir)
		{
			string[] files = Directory.GetFiles(dir);
			foreach (string file in files)
			{
				if (file.Contains(".dll"))
				{
					Logger.Debug("PLUGIN_LOADER", file);
					LoadAssembly(file);
				}
			}
		}

		public void LoadAssembly(string path)
		{
			Logger.Debug("PLUGIN_LOADER", path);
			Assembly a = Assembly.LoadFrom(path);

			foreach (Type t in a.GetTypes())
			{
				if (t.IsSubclassOf(typeof(Plugin)) && t != typeof(Plugin))
				{
					try
					{
						Plugin plugin = Activator.CreateInstance(t) as Plugin;
						PluginDetails details = (PluginDetails) Attribute.GetCustomAttribute(t, typeof(PluginDetails));
						if (details.id != null)
						{
							// should do version checking too
                            if (details.SmodMajor != SMOD_MAJOR && details.SmodMinor != SMOD_MINOR)
                            {
                                Logger.Warn("PLUGIN_LOADER", "Trying to load an outdated plugin " + details.name + " " + details.version);
                            }
                            else
                            {
                                plugin.Details = details;
                                plugins.Add(details.id, plugin);
                                Logger.Info("PLUGIN_LOADER", "Plugin loaded: " + plugin.ToString());
                            }

						}
						else
						{
							// print message
							Logger.Warn("PLUGIN_LOADER", "Plugin loaded but missing an id: " + t + "(" + path + ")");
						}
					}
					catch
					{
						Logger.Error("PLUGIN_LOADER", "Failed to create instance of plugin " + t + "(" + path + ")");
					}
				}
			}
		}
		


    }
}
