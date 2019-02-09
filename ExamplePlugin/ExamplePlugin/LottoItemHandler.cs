using System;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ExamplePlugin
{
	class LottoItemHandler : IEventHandlerPlayerPickupItem
	{
		private readonly ExamplePlugin plugin;
		private readonly Random random;

		public LottoItemHandler(ExamplePlugin plugin)
		{
			random = new Random();
			this.plugin = plugin;
		}

		public void OnPlayerPickupItem(PlayerPickupItemEvent ev)
		{
			plugin.Info(ev.Player.Name + " picked up item " + ev.Item.ItemType);
			//Checks the itemtype of the picked up item
			if (ev.Item.ItemType == ItemType.COIN)
			{
				double luckyValue = random.NextDouble();
				if (luckyValue > 0.8)
				{
					//20% chance of instantly dying
					ev.Player.Kill();
				}

				if (luckyValue < 0.1)
				{
					//10% chance of getting a MicroHID
					ev.ChangeTo = ItemType.MICROHID;
				}
			}
		}
	}
}
