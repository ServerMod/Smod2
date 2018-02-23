
using Smod2.Game;

namespace Smod2.Events
{
	public interface IEventPlayerHurt : IEvent
	{
		void OnPlayerHurt(Player player, out int damage, out DamageType type);
	}

	public interface IEventPlayerDie : IEvent
	{
		void OnPlayerDie(Player player, out bool spawnRagdoll);
	}

	public interface IEventPlayerPickupItem : IEvent
	{
		void OnPlayerPickupItem(Player player, Item item, out bool allow);
	}

	public interface IEventPlayerDropItem : IEvent
	{
		void OnPlayerDropItem(Player player, Item item, out bool allow);
	}

	public interface IEventPlayerJoin : IEvent
	{
		void OnPlayerJoin(Player player);
	}

	public interface IEventPlayerLeave : IEvent
	{
		void OnPlayerLeave(Player player);
	}
}
