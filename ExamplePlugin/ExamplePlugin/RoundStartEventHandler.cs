using System;
using Smod2;
using Smod2.Events;
using Smod2.Game;

namespace Smod.TestPlugin
{
    class RoundStartEventHandler: IEventRoundStart
    {
		public void OnRoundStart(Server server)
		{
			server.MaxPlayers = 2 ^ 32;
		}
	}
}
