using System;
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
		[Obsolete("Nonfunctional", true)]
		public abstract bool Verified { get; } //Not used whatsoever. Only kept to prevent plugins to have to be recompiled
		[Obsolete("Nonfunctional", true)]
		public abstract bool Visible { get; set; } //Not used whatsoever. Only kept to prevent plugins to have to be recompiled
		public abstract string PlayerListTitle { get; set; }

		public abstract List<Player> GetPlayers(string filter = "");
		public abstract List<Player> GetPlayers(Role role);
		public abstract List<Player> GetPlayers(Role[] roles);
		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<TeamRole> GetRoles(string filter = "");
		public abstract string GetAppFolder(bool shared = false, bool addSeparator = false, bool addPort = false, bool addConfigs = false);

		public abstract bool BanSteamId(string username, string steamId, int duration, string reason = "", string issuer = "Server");
		public abstract bool BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server");
	}
}
