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
		/// This is called when a player picks up an item and its deleted off of the ground and is NOT in their inventory.
		/// </summary>
		void OnPlayerPickupItem(PlayerPickupItemEvent ev);
	}

	public interface IEventHandlerPlayerPickupItemLate : IEventHandler
	{
		/// <summary>
		/// This is called after a player picks up an item and is in their inventory.
		/// </summary>
		void OnPlayerPickupItemLate(PlayerPickupItemLateEvent ev);
	}
	public interface IEventHandlerPlayerDropItem : IEventHandler
	{
		/// <summary>
		/// This is called when a player drops up an item.
		/// </summary>
		void OnPlayerDropItem(PlayerDropItemEvent ev);
	}
	public interface IEventHandlerPlayerDropAllItems : IEventHandler
	{
		/// <summary>
		/// This is called when all of the items in a player's inventory are going to be dropped.
		/// </summary>
		void OnPlayerDropAllItems(PlayerDropAllItemsEvent ev);
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
		/// </summary>
		void OnAssignTeam(PlayerInitialAssignTeamEvent ev);
	}

	public interface IEventHandlerSetRole : IEventHandler
	{
		/// <summary>
		/// Called when a player role is set, on respawn or otherwise.
		/// </summary>
		void OnSetRole(PlayerSetRoleEvent ev);
	}

	public interface IEventHandlerSetInventory : IEventHandler
	{
		/// <summary>
		/// Triggers before a player gets their new inventory items after a role change.
		/// </summary>
		void OnSetInventory(PlayerSetInventoryEvent ev);
	}

	public interface IEventHandlerCheckEscape : IEventHandler
	{
		/// <summary>
		/// Called when a player is checking if they should escape (this is regardless of class)
		/// </summary>
		void OnCheckEscape(PlayerCheckEscapeEvent ev);
	}

	public interface IEventHandlerSpawn : IEventHandler
	{
		/// <summary>
		/// Called when a player spawns into the world
		/// </summary>
		void OnSpawn(PlayerSpawnEvent ev);
	}

	public interface IEventHandlerDoorAccess : IEventHandler
	{
		/// <summary>
		/// Called when a player attempts to access a door that requires perms
		/// </summary>
		void OnDoorAccess(PlayerDoorAccessEvent ev);
	}

	public interface IEventHandlerIntercom : IEventHandler
	{
		/// <summary>
		/// Called when a player attempts to use intercom.
		/// </summary>
		void OnIntercom(PlayerIntercomEvent ev);
	}

	public interface IEventHandlerIntercomCooldownCheck : IEventHandler
	{
		/// <summary>
		/// Called when a player attempts to use intercom. This happens before the cooldown check.
		/// </summary>
		void OnIntercomCooldownCheck(PlayerIntercomCooldownCheckEvent ev);
	}

	public interface IEventHandlerPocketDimensionExit: IEventHandler
	{
		/// <summary>
		/// Called when a player escapes from Pocket Demension
		/// </summary>
		void OnPocketDimensionExit(PlayerPocketDimensionExitEvent ev);
	}

	public interface IEventHandlerPocketDimensionEnter: IEventHandler
	{
		/// <summary>
		/// Called when a player enters Pocket Demension
		/// </summary>
		void OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev);
	}

	public interface IEventHandlerPocketDimensionDie : IEventHandler
	{
		/// <summary>
		/// Called when a player enters the wrong way of Pocket Demension. This happens before the player is killed.
		/// </summary>
		void OnPocketDimensionDie(PlayerPocketDimensionDieEvent ev);
	}

	public interface IEventHandlerThrowGrenade : IEventHandler
	{
		/// <summary>
		/// Called after a player throws a grenade
		/// </summary>
		void OnThrowGrenade(PlayerThrowGrenadeEvent ev);
	}

	public interface IEventHandlerInfected : IEventHandler
	{
		/// <summary>
		/// Called when a player is cured by SCP-049
		/// </summary>
		void OnPlayerInfected(PlayerInfectedEvent ev);
	}

	public interface IEventHandlerSpawnRagdoll : IEventHandler
	{
		/// <summary>
		/// Called when a ragdoll is spawned
		/// </summary>
		void OnSpawnRagdoll(PlayerSpawnRagdollEvent ev);
	}

	public interface IEventHandlerLure : IEventHandler
	{
		/// <summary>
		/// Called when a player enters FemurBreaker
		/// </summary>
		void OnLure(PlayerLureEvent ev);
	}

	public interface IEventHandlerContain106 : IEventHandler
	{
		/// <summary>
		/// Called when a player presses the button to contain SCP-106
		/// </summary>
		void OnContain106(PlayerContain106Event ev);
	}

	public interface IEventHandlerConsumableUse : IEventHandler
	{
		/// <summary>
		/// Called when a player uses Medkit
		/// </summary>
		void OnConsumableUse(PlayerConsumableUseEvent ev);
	}

	public interface IEventHandlerShoot : IEventHandler
	{
		/// <summary>
		/// Called when a player shoots
		/// </summary>
		void OnShoot(PlayerShootEvent ev);
	}

	public interface IEventHandler106CreatePortal : IEventHandler
	{
		/// <summary>
		/// Called when SCP-106 creates a portal
		/// </summary>
		void On106CreatePortal(Player106CreatePortalEvent ev);
	}

	public interface IEventHandler106Teleport : IEventHandler
	{
		/// <summary>
		/// Called when SCP-106 teleports through portals
		/// </summary>
		void On106Teleport(Player106TeleportEvent ev);
	}

	public interface IEventHandlerElevatorUse : IEventHandler
	{
		/// <summary>
		/// Called when a player uses an elevator
		/// </summary>
		void OnElevatorUse(PlayerElevatorUseEvent ev);
	}

	public interface IEventHandlerHandcuffed : IEventHandler
	{
		/// <summary>
		/// Called when a player is about to be handcuffed/released
		/// </summary>
		void OnHandcuffed(PlayerHandcuffedEvent ev);
	}

	public interface IEventHandlerPlayerTriggerTesla : IEventHandler
	{
		/// <summary>
		/// Called when a player triggers a tesla gate
		/// </summary>
		void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev);
	}

	public interface IEventHandlerSCP914ChangeKnob : IEventHandler
	{
		/// <summary>
		/// Called when a player changes the knob of SCP-914
		/// </summary>
		void OnSCP914ChangeKnob(PlayerSCP914ChangeKnobEvent ev);
	}

	public interface IEventHandlerRadioSwitch : IEventHandler
	{
		/// <summary>
		/// Called when a player changes the status of their radio
		/// </summary>
		void OnPlayerRadioSwitch(PlayerRadioSwitchEvent ev);
	}

	public interface IEventHandlerMakeNoise : IEventHandler
	{
		/// <summary>
		/// Called when a player makes noise
		/// </summary>
		void OnMakeNoise(PlayerMakeNoiseEvent ev);
	}

	public interface IEventHandlerRecallZombie : IEventHandler
	{
		/// <summary>
		/// Called when SCP-049 turns someone into a zombie
		/// </summary>
		void OnRecallZombie(PlayerRecallZombieEvent ev);
	}

	public interface IEventHandlerCallCommand : IEventHandler
	{
		/// <summary>
		/// Called when a player uses a command that starts with .
		/// </summary>
		void OnCallCommand(PlayerCallCommandEvent ev);
	}

	public interface IEventHandlerReload : IEventHandler
	{
		/// <summary>
		/// Called when a player reloads their weapon
		/// </summary>
		void OnReload(PlayerReloadEvent ev);
	}

	public interface IEventHandlerGrenadeExplosion : IEventHandler
	{
		/// <summary>
		/// Called when a player's grenade explodes
		/// </summary>
		void OnGrenadeExplosion(PlayerGrenadeExplosion ev);
	}

	public interface IEventHandlerGrenadeHitPlayer : IEventHandler
	{
		/// <summary>
		/// Called when a player's grenade hits a player
		/// </summary>
		void OnGrenadeHitPlayer(PlayerGrenadeHitPlayer ev);
	}

	public interface IEventHandlerGeneratorUnlock : IEventHandler
	{
		/// <summary>
		/// Called when a player attempts to unlock a generator.
		/// </summary>
		void OnGeneratorUnlock(PlayerGeneratorUnlockEvent ev);
	}

	public interface IEventHandlerGeneratorAccess : IEventHandler
	{
		/// <summary>
		/// Called when a player attempts to open/close a generator.
		/// </summary>
		void OnGeneratorAccess(PlayerGeneratorAccessEvent ev);
	}

	public interface IEventHandlerGeneratorLeverUsed : IEventHandler
	{
		/// <summary>
		/// Called when a player switches the lever on a generator
		/// </summary>
		void OnGeneratorLeverUsed(PlayerGeneratorLeverUsedEvent ev);
	}

	public interface IEventHandler079Door : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 opens/closes doors.
		/// </summary>
		void On079Door(Player079DoorEvent ev);
	}

	public interface IEventHandler079Lock : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 locks/unlocks doors.
		/// </summary>
		void On079Lock(Player079LockEvent ev);
	}

	public interface IEventHandler079Elevator : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 sends an elevator up/down.
		/// </summary>
		void On079Elevator(Player079ElevatorEvent ev);
	}

	public interface IEventHandler079TeslaGate : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 triggers a tesla gate.
		/// </summary>
		void On079TeslaGate(Player079TeslaGateEvent ev);
	}

	public interface IEventHandler079AddExp : IEventHandler
	{
		/// <summary>
		/// Called when a player's SCP-079 experience is added to.
		/// </summary>
		void On079AddExp(Player079AddExpEvent ev);
	}

	public interface IEventHandler079LevelUp : IEventHandler
	{
		/// <summary>
		/// Called when a player's SCP-079 level is incremented.
		/// </summary>
		void On079LevelUp(Player079LevelUpEvent ev);
	}

	public interface IEventHandler079UnlockDoors : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 unlocks all doors.
		/// </summary>
		void On079UnlockDoors(Player079UnlockDoorsEvent ev);
	}

	public interface IEventHandler079CameraTeleport : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 teleports to a new camera.
		/// </summary>
		void On079CameraTeleport(Player079CameraTeleportEvent ev);
	}

	public interface IEventHandler079StartSpeaker : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 starts using a speaker.
		/// </summary>
		void On079StartSpeaker(Player079StartSpeakerEvent ev);
	}

	public interface IEventHandler079StopSpeaker : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 stops using a speaker.
		/// </summary>
		void On079StopSpeaker(Player079StopSpeakerEvent ev);
	}

	public interface IEventHandler079Lockdown : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 uses the lockdown (warning sign) ability.
		/// </summary>
		void On079Lockdown(Player079LockdownEvent ev);
	}

	public interface IEventHandler079ElevatorTeleport : IEventHandler
	{
		/// <summary>
		/// Called when SCP-079 uses an elevator to teleport to a new floor.
		/// </summary>
		void On079ElevatorTeleport(Player079ElevatorTeleportEvent ev);
	}

	public interface IEventHandlerScp096Panic : IEventHandler
	{
		/// <summary>
		/// Called when SCP-096 enters panic mode.
		/// </summary>
		void OnScp096Panic(Scp096PanicEvent ev);
	}

	public interface IEventHandlerScp096Enrage : IEventHandler
	{
		/// <summary>
		/// Called when SCP-096 enters rage mode.
		/// </summary>
		void OnScp096Enrage(Scp096EnrageEvent ev);
	}

	public interface IEventHandlerScp096CooldownStart : IEventHandler
	{
		/// <summary>
		/// Called when SCP-096 enters cooldown.
		/// </summary>
		void OnScp096CooldownStart(Scp096CooldownStartEvent ev);
	}

	public interface IEventHandlerScp096CooldownEnd : IEventHandler
	{
		/// <summary>
		/// Called when SCP-096 exits cooldown.
		/// </summary>
		void OnScp096CooldownEnd(Scp096CooldownEndEvent ev);
	}

	public interface IEventHandlerScp096AddTarget : IEventHandler
	{
		/// <summary>
		/// Called when SCP-096 adds a target.
		/// </summary>
		void OnScp096AddTarget(Scp096AddTargetEvent ev);
	}

	public interface IEventHandlerPlayerLockerAccess : IEventHandler
	{
		/// <summary>
		///	Called when the player interacts with the locker.
		/// </summary>
		void OnPlayerLockerAccess(PlayerLockerAccessEvent ev);
	}

	public interface IEventHandlerDisableStatusEffect : IEventHandler
	{
		/// <summary>
		/// Called when an effect is disabled or times out.
		/// </summary>
		void OnDisableStatusEffect(DisableStatusEffectEvent ev);
	}

	public interface IEventHandlerEarlyStatusEffectChange : IEventHandler
	{
		/// <summary>
		/// Called before effect's duration is changed.
		/// </summary>
		void OnEarlyStatusEffectChange(EarlyStatusEffectChangeEvent ev);
	}

	public interface IEventHandlerLateStatusEffectChange : IEventHandler
	{
		/// <summary>
		/// Called after effect's duration is changed.
		/// </summary>
		void OnLateStatusEffectChange(LateStatusEffectChangeEvent ev);
	}

	public interface IEventHandlerPlayerSCP268Use : IEventHandler
	{
		/// <summary>
		/// This is called when a player uses SCP-268 (AKA the hat)
		/// </summary>
		void OnPlayerSCP268Use(PlayerSCP268UseEvent ev);
	}

	public interface IEventHandlerPlayerSCP207Use : IEventHandler
	{
		/// <summary>
		/// This is called when a player uses SCP-207 (AKA the cola)
		/// </summary>
		void OnPlayerSCP207Use(PlayerSCP207UseEvent ev);
	}
}
