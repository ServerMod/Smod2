using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace Smod.TestPlugin
{
    class RoundStartHandler : IEventHandlerRoundStart
    {
		private Plugin plugin;

		public RoundStartHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

        public void OnRoundStart(RoundStartEvent ev)
        {
            plugin.Info("ROUND START EVENT CALLER");
            plugin.Info("Players: " + ev.Server.GetPlayers().Count);
            foreach (Player player in ev.Server.GetPlayers())
            {
                // Print the player info and then their class info
                plugin.Info(player.ToString());
                plugin.Info(player.TeamRole.ToString());
            }
        }
    }
}
