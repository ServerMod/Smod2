using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public abstract class Server
	{
		public abstract string Name { get; set; }
		public abstract string Port { get; }
		public abstract string IpAddress { get; }
		public abstract Round Round { get; }
		public abstract int NumPlayers { get; }
		public abstract int MaxPlayers { get; set; }
		public abstract bool Verified { get; }
		public abstract bool Visible { get; set; }

		public abstract List<Player> GetPlayers(string filter = "");
		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<TeamClass> GetClasses(string filter = "");
		public abstract List<Item> GetItems(ItemType type, bool world_only); // may be removed.

		public abstract string GetConfigString(string key, string def);
		public abstract bool GetConfigBool(string key, bool def);
		public abstract int GetConfigInt(string key, int def);
	}
}
