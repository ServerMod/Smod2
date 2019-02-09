using System;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ExamplePlugin
{
	class MultipleEventsExample : IEventHandlerDoorAccess, IEventHandlerPlayerHurt
	{
		private readonly ExamplePlugin plugin;
		private readonly Random random;

		public MultipleEventsExample(ExamplePlugin plugin)
		{
			this.plugin = plugin;
			random = new Random();
		}

		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			//There is a 0.5% probability for a door to get destroyed upon interaction
			if (random.NextDouble() > 0.95f)
				ev.Destroy = true;
		}

		public void OnPlayerHurt(PlayerHurtEvent ev)
		{
			//If victim's name is John, set damage to 0f
			if (ev.Player.Name == "John")
				ev.Damage = 0f;
			//If attacker's name is Hubert, set damage to 100000f
			else if (ev.Attacker.Name == "Hubert")
				ev.Damage = 100000f;
		}
	}
}
