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
		}
	}
}
