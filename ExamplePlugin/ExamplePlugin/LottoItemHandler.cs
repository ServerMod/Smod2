using System;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace Smod.TestPlugin
{
    class LottoItemHandler : IEventHandlerPlayerPickupItem
    {
        private Plugin plugin;
        Random rnd;

        public LottoItemHandler(Plugin plugin)
        {
            rnd = new Random();
            this.plugin = plugin;
        }

        public void OnPlayerPickupItem(PlayerPickupItemEvent ev)
        {
            plugin.Info(ev.Player.Name + " picked up item " + ev.Item.ItemType);
            if (ev.Item.ItemType == ItemType.COIN)
            {
                double luckyValue = rnd.NextDouble();
                if (luckyValue > 0.8)
                {
                    ev.ChangeTo = ItemType.MICROHID;
                }

                if (luckyValue < 0.1)
                {
                    ev.Player.Kill();
                }
   
            }
        }

    }
}
