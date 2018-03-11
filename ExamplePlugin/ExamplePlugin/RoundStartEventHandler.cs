using Smod2;
using Smod2.API;
using Smod2.Events;

namespace Smod.TestPlugin
{
    class RoundStartHandler : IEventRoundStart
    {
		private Plugin plugin;

		public RoundStartHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnRoundStart(Server server)
		{
			plugin.Info("ROUND START EVENT CALLER");
			plugin.Info("Players: " + server.GetPlayers().Count);
			foreach (Player player in server.GetPlayers())
			{
				// Print the player info and then their class info
				plugin.Info(player.ToString());
				plugin.Info(player.Class.ToString());
			}
		}
	}
}
