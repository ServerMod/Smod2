using System;

namespace Smod2.Game
{
	public abstract class Server
	{
		public abstract String Name { get; set; }
		public abstract String Port { get; }
		public abstract String IpAddress { get; }
		public abstract Player[] Players { get; }
		public abstract int NumPlayers { get; }
		public abstract int MaxPlayers { get; set; }
		public abstract Boolean Verified { get; set; } // Note: this doesnt bypass verification, you can just choose to disable it to hide the server from the list
	}
}
