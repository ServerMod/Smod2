using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2..API;
using Smod2.Logging;

namespace Smod2
{
    public class PluginManager
    {
		private Dictionary<String, Plugin> plugins;
		private static PluginManager singleton;

		public CommandManager CommandManager
		{
			get { return CommandManager; }
			set
			{
				if (CommandManager == null)
				{
					CommandManager = value;
				}
			}
		}

		public ConfigFile Config
		{
			get { return Config; }
			set
			{
				if (Config == null)
				{
					Config = value;
				}
			}
		}

		public Server Server
		{
			get { return Server; }
			set
			{
				if (Server == null)
				{
					Server = value;
				}
			}
		}

		public Logger Logger
		{
			get { return Logger; }
			set
			{
				if (Logger == null)
				{
					Logger = value;
				}
			}
		}

		public PluginManager()
		{
			plugins = new Dictionary<String, Plugin>();
		}

		public static PluginManager GetPluginManager()
		{
			if (singleton == null)
			{
				singleton = new PluginManager();
			}

			return singleton;
		}

		public Plugin GetPlugin(String id)
		{
			plugins.TryGetValue(id, out Plugin plugin);
			return plugin;
		}

		public List<Plugin> FindPlugins(String name)
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
				plugin.Value.Register();
				plugin.Value.OnEnable();
			}
		}

		public void LoadAssemblies(String dir)
		{
			String[] files = Directory.GetFiles(dir);
			foreach (String file in files)
			{
				if (file.Contains(".dll"))
				{
					LoadAssembly(file);
				}
			}
		}

		public void LoadAssembly(String path)
		{
			Assembly.LoadFrom(path);
			foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
			{
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
								plugin.Details = details;
								plugins.Add(details.id, plugin);
							}
							else
							{
								// print message
								Console.WriteLine("Plugin loaded but missing an id: " + t + "(" + path + ")");
							}
						}
						catch
						{
							Console.WriteLine("Failed to create instance of plugin " + t + "(" + path + ")");
						}
					}
				}
			}
		}


    }
}
