using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public abstract class Map
	{
		public abstract List<Room> GetAllRooms();
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
		public abstract void SpawnItem(ItemType type, Vector position, Vector rotation);
		public abstract void SpawnItem(WeaponType type, float Ammo, WeaponSight Sight, WeaponBarrel Barrel, WeaponOther Other, Vector pos, Vector rotation);
		public abstract void SpawnItem(AmmoType type, float Ammo, Vector position, Vector rotation);
		/// <summary>  
		/// Note: When FemurBreaker is enabled, SCP-106 can't be contained. This might break the lure configs and mechanism.
		/// </summary> 
		public abstract void FemurBreaker(bool enable);
		public abstract List<Elevator> GetElevators();
		public abstract void SetIntercomContent(IntercomStatus intercomStatus, string content);
		public abstract string GetIntercomContent(IntercomStatus intercomStatus);
		public abstract List<TeslaGate> GetTeslaGates();
		public abstract void AnnounceNtfEntrance(int scpsLeft, int mtfNumber, char mtfLetter);
		public abstract void AnnounceScpKill(string scpNumber, Player killer = null);
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

	public abstract class Door
	{
		public abstract bool Open { get; set; }
		public abstract bool Destroyed { get; set; }
		public abstract bool DontOpenOnWarhead { get; set; }
		public abstract bool BlockAfterWarheadDetonation { get; set; }
		public abstract bool Locked { get; set; }
		public abstract float LockCooldown { get; set; }
		public abstract Vector Position { get; }
		public abstract string Name { get; }
		public abstract string Permission { get; }
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
		LiftA = 0,
		LiftB = 1,
		GateA = 2,
		GateB = 3,
		WarheadRoom = 4,
		SCP049Chamber = 5
	}

	public enum ElevatorStatus
	{
		Up,
		Down,
		Moving
	}

	public enum IntercomStatus
	{
		Ready = 0,
		Transmitting = 1,
		/* TransmittingBypass */
		Transmitting_Bypass = 2,
		Restarting = 3,
		/* AdminSpeaking */
		Transmitting_Admin = 4,
		Muted = 5,
		Custom = 6
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
		Killer = 0,
		Exit = 1
	}

	public abstract class PocketDimensionExit
	{
		public abstract PocketDimensionExitType ExitType { get; set; }
		public abstract Vector Position { get; }
	}

	public enum Spawnable //1st is player and 2nd is Lobby_Playback and I don't know how spawning these would break/destroy existance itself.
	{
		Pickup = 2,
		Work_Station = 3,
		Ragdoll_SCP173 = 4,
		Ragdoll_DClass = 5,
		Ragdoll_SCP106 = 6,
		Ragdoll_MTF = 7,
		Ragdoll_SCIENTIST = 8,
		Ragdoll_SCP049 = 9,
		Ragdoll_CHAOS = 10,
		SCP_096_Ragdoll = 11,
		Ragdoll_SCP049_2 = 12,
		Ragdoll_GUARD = 13,
		Ragdoll_SCP939_53 = 14,
		Ragdoll_SCP939_89 = 15,
		Grenade_Flash = 16,
		Grenade_Frag = 17,
		Grenade_SCP_018 = 18
	}

	public enum ZoneType
	{
		LCZ,
		HCZ,
		ENTRANCE,
		SURFACE
	}

	public enum RoomType
	{
		//Light Containment Zone
		LCZ_012,
		LCZ_173,
		LCZ_372,
		LCZ_914,
		LCZ_AIRLOCK,
		LCZ_ARMORY,
		LCZ_CAFE,
		LCZ_CHECKPOINT_A,
		LCZ_CHECKPOINT_B,
		LCZ_CLASSD_SPAWN,
		LCZ_4_WAY_INTERSECTION,
		LCZ_3_WAY_INTERSECTION,
		LCZ_CURVE,
		LCZ_PLANTS,
		LCZ_STRAIGHT,
		LCZ_TOILETS,
		//Heavy Containment Zone
		HCZ_049,
		HCZ_079,
		HCZ_106,
		HCZ_096,
		HCZ_939,
		HCZ_CHECKPOINT_A,
		HCZ_CHECKPOINT_B,
		HCZ_4_WAY_INTERSECTION,
		HCZ_3_WAY_INTERSECTION,
		HCZ_CURVE,
		HCZ_STRAIGHT,
		HCZ_EZ_CHECKPOINT,
		HCZ_HID,
		HCZ_NUKE,
		HCZ_SERVER,
		HCZ_TARMORY,
		HCZ_TESLA,
		//Entrance Zone
		EZ_STRAIGHT,
		EZ_CAFETERIA,
		EZ_CHEF, //There are 4 straight hallways... how do you name these properly >>>>:(
		EZ_GATE_A,
		EZ_GATE_B,
		EZ_4_WAY_INTERSECTION,
		EZ_3_WAY_INTERSECTION,
		EZ_CURVE,
		EZ_COLLAPSEDTUNNEL,
		EZ_REDGATE,
		EZ_INTERCOM,
		EZ_PC_LARGE,
		EZ_PC_SMALL,
		EZ_SHELTER,
		EZ_SMALLSTRAIGHT,
		EZ_SMALLSTRAIGHT2,
		EZ_UPSTAIRS,
		//End
		SURFACE,
	}

	public enum Scp079InteractionType
	{
		CAMERA = 0,
		SPEAKER = 4
	}

	public abstract class Room
	{
		public abstract ZoneType ZoneType { get; }
		public abstract RoomType RoomType { get; }
		public abstract int GenericID { get; }
		public abstract Vector Position { get; }
		public abstract Vector Forward { get; }
		public abstract Vector SpeakerPosition { get; }
		public abstract void FlickerLights(float duration = 8f);
		[Obsolete("Use FlickerLights(float duration = 8f) instead of FlickerLights()")]
		public abstract void FlickerLights();
		public abstract string[] GetObjectName();
		public abstract object GetGameObject();
	}

	public abstract class Generator
	{
		public abstract bool Open { get; set; }
		public abstract bool Locked { get; }
		public abstract bool HasTablet { get; set; }
		public abstract bool Engaged { get; }
		public abstract float StartTime { get; }
		public abstract float TimeLeft { get; set; }
		public abstract Vector Position { get; }
		public abstract Room Room { get; }
		public abstract void Unlock();
		public abstract object GetComponent();
	}
}
