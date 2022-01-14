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
			plugin.Info(ev.Player.name + " picked up item " + ev.Item.itemType);
			//Checks the itemtype of the picked up item
			if (ev.Item.itemType == ItemType.COIN)
			{
				double luckyValue = random.NextDouble();

				// Chance of instantly dying
				if (luckyValue < plugin.killChance)
				{
					ev.Player.Kill("Was unlucky");
				}
				else
				{
					// An array lets other plugins set the values of it in events.
					ItemType[] itemPtr = { ItemType.MICRO_HID };
					plugin.InvokeEvent("courtney.example.plugin", itemPtr);

					// Get the singular item from the array since indexing it every time is annoying.
					ItemType item = itemPtr[0];

					// lottoItemCount is automatically read and converted to an int. This gives the item lottoItemCount times to the player.
					int count = plugin.lottoItemCount;
					plugin.Info($"{nameof(plugin.lottoItemCount)} is {count}");
					for (int i = 0; i < count; i++)
					{
						ev.Player.GiveItem(item);
					}
				}
			}
		}
	}
}
