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
				
				// Chance of instantly dying
				if (luckyValue > plugin.killChance)
				{
					ev.Player.Kill();
				}
				else
				{
					// An array lets other plugins set the values of it in events.
					ItemType[] itemPtr = {ItemType.MICROHID};
					plugin.InvokeEvent("courtney.example.plugin", itemPtr);

					// Get the singular item from the array since indexing it every time is annoying.
					ItemType item = itemPtr[0];
					
					// lottoItemCount is automatically read and converted to an int. This gives the item lottoItemCount times to the player.
					for (int i = 0; i < plugin.lottoItemCount; i++)
					{
						ev.Player.GiveItem(item);
					}
				}
			}
		}
	}
}
