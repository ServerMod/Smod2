using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ExamplePlugin
{
	class RoundEventHandler : IEventHandlerWaitingForPlayers, IEventHandlerRoundStart, IEventHandlerRoundEnd
	{
		private readonly ExamplePlugin plugin;

		public RoundEventHandler(ExamplePlugin plugin) => this.plugin = plugin; //Expression bodies can also be used
		
		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			plugin.Info("Awesomeness score: " + plugin.myAwesomenessScore);
			plugin.Info(plugin._configValue + plugin.GetConfigString("myConfigKey"));
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			plugin.Info("ROUND END EVENT CALLER");
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			plugin.Info("ROUND START EVENT CALLER");
			plugin.Info("Players: " + ev.Server.NumPlayers);
			foreach (Player player in ev.Server.GetPlayers())
			{
				// Print the player info and then their class info
				plugin.Info(player.ToString());
				plugin.Info(player.role.ToString());
			}
		}
	}
}
