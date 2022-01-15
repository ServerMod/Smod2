using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public abstract class Map
	{
		/// <summary>
		/// Gets every Room in the game including surface (Pocket excluded)
		/// </summary>
		/// <returns>Every room in the game</returns>
		public abstract List<Room> GetRooms();
		public abstract List<Item> GetItems(ItemType type, bool world_only);
		public abstract Vector GetRandomSpawnPoint(RoleType role);
		[Obsolete("Use RoleType instead of Role")]
		public abstract Vector GetRandomSpawnPoint(Role role);
		public abstract List<Vector> GetSpawnPoints(RoleType role);
		[Obsolete("Use RoleType instead of Role")]
		public abstract List<Vector> GetSpawnPoints(Role role);
		public abstract List<Vector> GetBlastDoorPoints();
		public abstract List<Door> GetDoors();
		public abstract List<PocketDimensionExit> GetPocketDimensionExits();
		public abstract Dictionary<Vector, Vector> GetElevatorTeleportPoints();
		public abstract Generator[] GetGenerators();
		public abstract Room[] Get079InteractionRooms(Scp079InteractionType type);
		public abstract void DetonateWarhead();
		public abstract void StartWarhead();
		public abstract void StopWarhead();
		public abstract void Shake();
		public abstract bool WarheadDetonated { get; }
		public abstract bool LCZDecontaminated { get; }
		public abstract void SpawnItem(ItemType type, Vector position, Vector rotation = null);
		public abstract void SpawnItem(WeaponType type, int ammo, AttachmentType[] attachments, Vector pos, Vector rotation = null);
		public abstract void SpawnItem(AmmoType type, int ammo, Vector position, Vector rotation = null);
		/// <summary>
		/// Note: When FemurBreaker is enabled, SCP-106 can't be contained. This might break the lure configs and mechanism.
		/// </summary>
		public abstract void FemurBreaker(bool enable);
		public abstract List<Elevator> GetElevators();
		public abstract void SetIntercomContent(IntercomStatus intercomStatus, string content);
		public abstract string GetIntercomContent(IntercomStatus intercomStatus);
		public abstract List<TeslaGate> GetTeslaGates();
		public abstract void AnnounceNtfEntrance(int scpsLeft, int mtfNumber, char mtfLetter);
		public abstract void AnnounceScpKill(RoleType scp, string cassieAnnouncement = null);
		public abstract void AnnounceCustomMessage(string words);
		public abstract void AnnounceCustomMessage(string words, bool makeHold, bool makeNoise);
		public abstract void SetIntercomSpeaker(Player player);
		public abstract Player GetIntercomSpeaker();
		public abstract void Broadcast(uint duration, string message, bool isMonoSpaced);
		public abstract void ClearBroadcasts();
		public abstract bool WarheadLeverEnabled { get; set; }
		public abstract bool WarheadKeycardEntered { get; set; }
		public abstract void OverchargeLights(float forceDuration, bool onlyHeavy);

		/// <summary>
		/// Spawns an item for everyone then returns that object.
		/// </summary>
		/// <param name="ThingToSpawn">Which prefab to use</param>
		/// <param name="position">Global position</param>
		/// <param name="rotation">Rotation</param>
		/// <param name="size">Scale the prefab</param>
		/// <param name="spawnRightAway">Should it spawn the Spawnable right away or let the plugin do it.</param>
		/// <returns>Gameobject that has been spawned</returns>
		public abstract object SpawnSpawnable(Spawnable ThingToSpawn, Vector position, Vector rotation, Vector size, bool spawnRightAway = true);
	}

	[Flags]
	public enum KeycardPermission : ushort
	{
		NONE = 0x0,
		CHECKPOINTS = 0x1,
		EXIT_GATES = 0x2,
		INTERCOM = 0x4,
		ALPHA_WARHEAD = 0x8,
		CONTAINMENT_LEVEL_ONE = 0x10,
		CONTAINMENT_LEVEL_TWO = 0x20,
		CONTAINMENT_LEVEL_THREE = 0x40,
		ARMORY_LEVEL_ONE = 0x80,
		ARMORY_LEVEL_TWO = 0x100,
		ARMORY_LEVEL_THREE = 0x200,
		SCP_OVERRIDE = 0x400
	}

	[Flags]
	public enum DoorLockReasons : ushort
	{
		NONE = 0x0,
		REGULAR079 = 0x1,
		LOCKDOWN079 = 0x2,
		WARHEAD = 0x4,
		ADMIN_COMMAND = 0x8,
		DECONTAMINATION_LOCKDOWN = 0x10,
		DECONTAMINATION_EVACUATE = 0x20,
		SPECIAL_DOOR_FEATURE = 0x40,
		NO_POWER = 0x80,
		ISOLATION = 0x100
	}

	[Flags]
	public enum DoorLockModes : byte
	{
		FULL_LOCK = 0x0,
		CAN_OPEN = 0x1,
		CAN_CLOSE = 0x2,
		SCP_OVERRIDE = 0x4
	}

	public enum DoorActions
	{
		OPENED,
		CLOSED,
		ACCESS_DENIED,
		LOCKED,
		UNLOCKED,
		DESTROYED
	}

	public abstract class Door
	{
		public abstract bool Open { get; set; }
		public abstract bool Destroyed { get; set; }
		[Obsolete("DontOpenOnWarhead removed from base game.")]
		public abstract bool DontOpenOnWarhead { get; set; }
		[Obsolete("BlockAfterWarheadDetonation removed from base game.")]
		public abstract bool BlockAfterWarheadDetonation { get; set; }
		public abstract bool Locked { get; set; }
		[Obsolete("LockCooldown removed from base game.")]
		public abstract float LockCooldown { get; set; }
		public abstract Vector Position { get; }
		public abstract string Name { get; }
		[Obsolete("Permission replaced with RequiredPermission")]
		public abstract string Permission { get; }
		public abstract KeycardPermission RequiredPermission { get; set; }
		public abstract DoorLockReasons LockReasons { get; set; }
		public abstract DoorLockModes LockModes { get; }
		public abstract void TriggerAction(DoorActions action);
		public abstract object GetComponent();
	}

	public abstract class TeslaGate
	{
		public abstract void Activate(bool instant = false);
		public abstract float TriggerDistance { get; set; }
		public abstract Vector Position { get; }
		public abstract object GetComponent();
	}

	public enum ElevatorType
	{
		LIFT_A = 0,
		LIFT_B = 1,
		GATE_A = 2,
		GATE_B = 3,
		WARHEAD_ROOM = 4,
		SCP_049_CHAMBER = 5
	}

	public enum ElevatorStatus
	{
		UP,
		DOWN,
		MOVING
	}

	public enum IntercomStatus
	{
		READY = 0,
		TRANSMITTING = 1,
		/* TransmittingBypass */
		TRANSMITTING_BYPASS = 2,
		RESTARTING = 3,
		/* AdminSpeaking */
		TRANSMITTING_ADMIN = 4,
		MUTED = 5,
		CUSTOM = 6
	}

	public abstract class Elevator
	{
		public abstract ElevatorType ElevatorType { get; }
		public abstract ElevatorStatus ElevatorStatus { get; }
		public abstract void Use();
		public abstract bool Locked { get; set; }
		public abstract bool Lockable { get; set; }
		public abstract float MovingSpeed { get; set; }
		public abstract List<Vector> GetPositions();
		public abstract object GetComponent();
	}

	public enum PocketDimensionExitType
	{
		KILLER = 0,
		EXIT = 1
	}

	public abstract class PocketDimensionExit
	{
		public abstract PocketDimensionExitType ExitType { get; set; }
		public abstract Vector Position { get; }
	}

	public enum Spawnable //1st is player and 2nd is Lobby_Playback and I don't know how spawning these would break/destroy existance itself.
	{
		PICKUP = 2,
		WORK_STATION = 3,
		RAGDOLL_SCP173 = 4,
		RAGDOLL_D_CLASS = 5,
		RAGDOLL_SCP106 = 6,
		RAGDOLL_MTF = 7,
		RAGDOLL_SCIENTIST = 8,
		RAGDOLL_SCP049 = 9,
		RAGDOLL_CHAOS = 10,
		SCP_096_RAGDOLL = 11,
		RAGDOLL_SCP049_2 = 12,
		RAGDOLL_GUARD = 13,
		RAGDOLL_SCP939_53 = 14,
		RAGDOLL_SCP939_89 = 15,
		GRENADE_FLASH = 16,
		GRENADE_FRAG = 17,
		GRENADE_SCP_018 = 18
	}

	public enum ZoneType
	{
		NONE,
		LIGHT_CONTAINMENT,
		HEAVY_CONTAINMENT,
		ENTRANCE,
		SURFACE,
		OTHER
	}

	public enum RoomType
	{
		UNNAMED,
		LCZ_CLASS_D_SPAWN,
		LCZ_COMPUTER_ROOM,
		LCZ_CHECKPOINT_A,
		LCZ_CHECKPOINT_B,
		LCZ_TOILETS,
		LCZ_ARMORY,
		LCZ173,
		LCZ_GLASS_ROOM,
		LCZ_012,
		LCZ_914,
		LCZ_GREENHOUSE,
		LCZ_AIRLOCK,
		HCZ_CHECKPOINT_TO_ENTRANCE_ZONE,
		HCZ_CHECKPOINT_A,
		HCZ_CHECKPOINT_B,
		HCZ_WARHEAD,
		HCZ_049,
		HCZ_079,
		HCZ_096,
		HCZ_106,
		HCZ_939,
		HCZ_MICRO_HID,
		HCZ_ARMORY,
		HCZ_SERVERS,
		HCZ_TESLA,
		EZ_COLLAPSED_TUNNEL,
		EZ_GATE_A,
		EZ_GATE_B,
		EZ_RED_ROOM,
		EZ_EVAC_SHELTER,
		EZ_INTERCOM,
		EZ_OFFICE_STORIED,
		EZ_OFFICE_LARGE,
		EZ_OFFICE_SMALL,
		OUTSIDE,
		POCKET
	}

	public enum Scp079InteractionType
	{
		CAMERA = 0,
		SPEAKER = 4
	}

	public abstract class Room
	{
		public abstract ZoneType zoneType { get; }
		public abstract RoomType roomType { get; }
		public abstract Vector position { get; }
		public abstract Vector forward { get; }
		public abstract Vector speakerPosition { get; }
		public abstract void FlickerLights(float duration = 8.0f);
		public abstract string[] GetObjectName();
		public abstract object GetGameObject();
	}

	public abstract class Generator
	{
		public abstract bool isOpen { get; set; }
		public abstract bool isUnlocked { get; set; }
		public abstract bool isEngaged { get; }
		/// <summary>
		/// The amount of time it takes for a generator to activate from when the lever is pulled
		/// </summary>
		public abstract float activationTime { get; }
		public abstract float timeLeft { get; }
		public abstract Vector position { get; }
		public abstract Room room { get; }
		public abstract object GetComponent();
	}
}
