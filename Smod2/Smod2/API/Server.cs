using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public abstract class Server
	{
		public abstract String Name { get; set; }
		public abstract String Port { get; }
		public abstract String IpAddress { get; }
		public abstract Round Round { get; }
		public abstract int NumPlayers { get; }
		public abstract int MaxPlayers { get; set; }
		public abstract Boolean Verified { get; set; } // Note: this doesnt bypass verification, you can just choose to disable it to hide the server from the list

		public abstract List<Player> GetPlayers(string filter = "");
		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<TeamClass> GetClasses(string filter = "");
		public abstract List<Item> GetItems(string filter = ""); // may be removed.

	}
}
