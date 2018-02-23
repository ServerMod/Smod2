using System;
using Smod2;
using Smod2.Events;
using Smod2.Game;

namespace Smod.TestPlugin
{
    class ExampleHandler : IEventRoundStart
    {
		private Priority priority;

		public ExampleHandler(Priority priority)
		{
			this.priority = priority;
		}

		public void OnRoundStart(Server server)
		{
			server.MaxPlayers = 2 ^ 32;
		}
	}
}
