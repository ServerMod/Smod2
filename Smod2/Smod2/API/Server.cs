using System.Collections.Generic;
using Smod2.Commands;

namespace Smod2.API
{
	public enum AuthType
	{
		SERVER,
		GAMESTAFF
	}

	public abstract class Server : ICommandSender
	{
		public abstract string Name { get; set; }
		public abstract int Port { get; }
		public abstract string IpAddress { get; }
		public abstract Round Round { get; }
		public abstract Map Map { get; }
		public abstract int NumPlayers { get; }
		public abstract int MaxPlayers { get; set; }
		public abstract bool Verified { get; }
		public abstract bool Visible { get; set; }
		public abstract string PlayerListTitle { get; set; }

		public abstract List<Player> GetPlayers(string filter = "");
		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<TeamRole> GetRoles(string filter = "");

		public abstract string BanSteamId(string username, string steamId, int duration, string reason = "", string issuer = "Server");
		public abstract string BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server");
	}
}
