using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace Smod.TestPlugin
{
    class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd
    {
		private Plugin plugin;

		public RoundEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			plugin.Info("ROUND END EVENT CALLER");
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
