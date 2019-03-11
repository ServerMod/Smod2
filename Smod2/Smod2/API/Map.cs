using System.Collections.Generic;

namespace Smod2.API
{
	public abstract class Map
	{
		public abstract List<Item> GetItems(ItemType type, bool world_only);
		public abstract Vector GetRandomSpawnPoint(Role role);
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
		public abstract void SetIntercomSpeaker(Player player);
		public abstract Player GetIntercomSpeaker();
		public abstract void Broadcast(uint duration, string message, bool isMonoSpaced);
		public abstract void ClearBroadcasts();
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
		Muted,
		Restarting,
		Transmitting_Admin,
		Transmitting_Bypass,
		Transmitting,
		Ready
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

	public enum ZoneType
	{
		UNDEFINED = 0,
		LCZ = 1,
		HCZ = 2,
		ENTRANCE = 3
	}

	public enum RoomType
	{
		UNDEFINED = 0,
		WC00 = 1,
		SCP_914 = 2,
		AIRLOCK_00 = 3,
		AIRLOCK_01 = 4,
		CHECKPOINT_A = 5,
		CHECKPOINT_B = 6,
		HCZ_ARMORY = 7,
		SERVER_ROOM = 8,
		MICROHID = 9,
		NUKE = 10,
		SCP_012 = 11,
		SCP_049 = 12,
		SCP_079 = 13,
		SCP_096 = 14,
		SCP_106 = 15,
		SCP_173 = 16,
		SCP_372 = 17,
		SCP_939 = 18,
		ENTRANCE_CHECKPOINT = 19,
		TESLA_GATE = 20,
		PC_SMALL = 21,
		PC_LARGE = 22,
		GATE_A = 23,
		GATE_B = 24,
		CAFE = 25,
		INTERCOM = 26,
		DR_L = 27,
		STRAIGHT = 28,
		CURVE = 29,
		T_INTERSECTION = 30,
		X_INTERSECTION = 31,
		LCZ_ARMORY = 32,
		CLASS_D_CELLS = 33,
		CUBICLES = 34
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
