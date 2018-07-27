using Smod2.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerPlayerHurt : IEventHandler
	{

		/// <summary>  
		/// This is called before the player is going to take damage.
		/// In case the attacker can't be passed, attacker will be null (fall damage etc)
		/// This may be broken into two events in the future
		/// </summary> 
		void OnPlayerHurt(PlayerHurtEvent ev);
	}

	public interface IEventHandlerPlayerDie : IEventHandler
	{
		/// <summary>  
		/// This is called before the player is about to die. Be sure to check if player is SCP106 (classID 3) and if so, set spawnRagdoll to false.
		/// In case the killer can't be passed, attacker will be null, so check for that before doing something.
		/// </summary> 
		void OnPlayerDie(PlayerDeathEvent ev);
	}

	public interface IEventHandlerPlayerPickupItem : IEventHandler
	{
		/// <summary>  
		/// This is called when a player picks up an item.
		/// </summary> 
		void OnPlayerPickupItem(PlayerPickupItemEvent ev);
	}

	public interface IEventHandlerPlayerDropItem : IEventHandler
	{
		/// <summary>  
		/// This is called when a player drops up an item.
		/// </summary> 
		void OnPlayerDropItem(PlayerDropItemEvent ev);
	}

	public interface IEventHandlerPlayerJoin : IEventHandler
	{
		/// <summary>  
		/// This is called when a player joins and is initialised.
		/// </summary> 
		void OnPlayerJoin(PlayerJoinEvent ev);
	}

	public interface IEventHandlerNicknameSet : IEventHandler
	{
		/// <summary>  
		/// This is called when a player attempts to set their nickname after joining. This will only be called once per game join.
		/// </summary> 
		void OnNicknameSet(PlayerNicknameSetEvent ev);
	}

	public interface IEventHandlerInitialAssignTeam : IEventHandler
	{
		/// <summary>  
		/// Called when a team is picked for a player. Nothing is assigned to the player, but you can change what team the player will spawn as.
		/// <summary>  
		void OnAssignTeam(PlayerInitialAssignTeamEvent ev);
	}

	public interface IEventHandlerSetRole : IEventHandler
	{
		/// <summary>  
		/// Called after the player is set a class, at any point in the game. 
		/// <summary>  
		void OnSetRole(PlayerSetRoleEvent ev);
	}

	public interface IEventHandlerCheckEscape : IEventHandler
	{
		/// <summary>  
		/// Called when a player is checking if they should escape (this is regardless of class)
		/// <summary>  
		void OnCheckEscape(PlayerCheckEscapeEvent ev);
	}

	public interface IEventHandlerSpawn : IEventHandler
	{
		/// <summary>  
		/// Called when a player spawns into the world
		/// <summary>  
		void OnSpawn(PlayerSpawnEvent ev);
	}

	public interface IEventHandlerDoorAccess : IEventHandler
	{
		/// <summary>  
		/// Called when a player attempts to access a door that requires perms
		/// <summary>  
		void OnDoorAccess(PlayerDoorAccessEvent ev);
	}

	public interface IEventHandlerIntercom : IEventHandler
	{
		/// <summary>  
		/// Called when a player attempts to use intercom.
		/// <summary>  
		void OnIntercom(PlayerIntercomEvent ev);
	}

	public interface IEventHandlerIntercomCooldownCheck : IEventHandler
	{
		/// <summary>  
		/// Called when a player attempts to use intercom. This happens before the cooldown check.
		/// <summary>  
		void OnIntercomCooldownCheck(PlayerIntercomCooldownCheckEvent ev);
	}

	public interface IEventHandlerPocketDimensionExit: IEventHandler
	{
		/// <summary>  
		/// Called when a player escapes from Pocket Demension
		/// <summary>  
		void OnPocketDimensionExit(PlayerPocketDimensionExitEvent ev);
	}

	public interface IEventHandlerPocketDimensionEnter: IEventHandler
	{
		/// <summary>  
		/// Called when a player enters Pocket Demension
		/// <summary>  
		void OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev);
	}

	public interface IEventHandlerPocketDimensionDie : IEventHandler
	{
		/// <summary>  
		/// Called when a player enters the wrong way of Pocket Demension. This happens before the player is killed.
		/// <summary>  
		void OnPocketDimensionDie(PlayerPocketDimensionDieEvent ev);
	}

	public interface IEventHandlerThrowGrenade : IEventHandler
	{
		/// <summary>  
		/// Called after a player throws a grenade
		/// <summary>  
		void OnThrowGrenade(PlayerThrowGrenadeEvent ev);
	}

	public interface IEventHandlerInfected : IEventHandler
	{
		/// <summary>  
		/// Called when a player is cured by SCP-049
		/// <summary>  
		void OnPlayerInfected(PlayerInfectedEvent ev);
	}

	public interface IEventHandlerSpawnRagdoll : IEventHandler
	{
		/// <summary>  
		/// Called when a ragdoll is spawned
		/// <summary>  
		void OnSpawnRagdoll(PlayerSpawnRagdollEvent ev);
	}

	public interface IEventHandlerLure : IEventHandler
	{
		/// <summary>  
		/// Called when a player enters FemurBreaker
		/// <summary> 
		void OnLure(PlayerLureEvent ev);
	}

	public interface IEventHandlerContain106 : IEventHandler
	{
		/// <summary>  
		/// Called when a player presses the button to contain SCP-106
		/// <summary>
		void OnContain106(PlayerContain106Event ev);
	}
}
