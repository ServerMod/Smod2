
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventPlayerHurt : IEventHandler
	{
		// This is called before the player is going to take damage. Be sure to set damageOutput to damage and typeOutput to type at the start of the plugin.
		// In case the attacker can't be passed, attacker will be null, so check for that before doing something.
		void OnPlayerHurt(Player player, Player attacker, float damage, out float damageOutput, DamageType type, out DamageType typeOutput);
	}

	public interface IEventPlayerDie : IEventHandler
	{
		// This is called before the player is about to die. Be sure to check if player is SCP106 (classID 3) and if so, set spawnRagdoll to false.
		// In case the killer can't be passed, attacker will be null, so check for that before doing something.
		void OnPlayerDie(Player player, Player killer, out bool spawnRagdoll);
	}

	public interface IEventPlayerPickupItem : IEventHandler
	{
		void OnPlayerPickupItem(Player player, Item item, out bool allow);
	}

	public interface IEventPlayerDropItem : IEventHandler
	{
		void OnPlayerDropItem(Player player, Item item, out bool allow);
	}

	public interface IEventPlayerJoin : IEventHandler
	{
		void OnPlayerJoin(Player player);
	}

	public interface IEventPlayerLeave : IEventHandler
	{
		void OnPlayerLeave(Player player);
	}
}
