using System.Collections.Generic;

namespace Smod2.API
{
	public enum AuthType
	{
		SERVER,
		GAMESTAFF
	}

	public abstract class Server
	{
		public abstract string Name { get; set; }
		public abstract int Port { get; }
		public abstract string IpAddress { get; }
		public abstract Round Round { get; }
		public abstract int NumPlayers { get; }
		public abstract int MaxPlayers { get; set; }
		public abstract bool Verified { get; }
		public abstract bool Visible { get; set; }

		public abstract List<Player> GetPlayers(string filter = "");
		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<TeamRole> GetRoles(string filter = "");
		public abstract List<Item> GetItems(ItemType type, bool world_only); // may be removed.

	}
}
