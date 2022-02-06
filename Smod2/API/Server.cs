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
		public abstract string PlayerListTitle { get; set; }

		public abstract List<Player> GetPlayers();
		public abstract List<Player> GetPlayers(string filter);
		public abstract List<Player> GetPlayers(RoleType role);
		public abstract List<Player> GetPlayers(RoleType[] roles);
		public abstract List<Player> GetPlayers(TeamType team);
		public abstract List<Player> GetPlayers(Predicate<Player> predicate);
		public abstract Player GetPlayer(int playerId);
		public abstract List<Connection> GetConnections(string filter = "");
		public abstract List<Role> GetRoles(string filter = "");
		public abstract string GetAppFolder(bool addSeparator = true, bool serverConfig = false, string centralConfig = "");

		public abstract bool BanUserId(string username, string userId, int duration, string reason = "", string issuer = "Server");
		public abstract bool BanIpAddress(string username, string ipAddress, int duration, string reason = "", string issuer = "Server");
	}
}
