using System.Collections.Generic;
using Smod2.Commands;

namespace Smod2.API
{
	public enum DamageType
	{
		NONE,
		LURE,
		NUKE,
		WALL,
		DECONT, // Decontamination
		TESLA, // Tesla
		FALLDOWN,
		FLYING,
		CONTAIN,
		POCKET,
		RAGDOLLLESS,
		COM15,
		P90,
		E11_STANDARD_RIFLE,
		MP7,
		LOGICER, // Chaos Gun
		USP,
		FRAG, // Frag grenade
		SCP_049,
		SCP_049_2,
		SCP_096,
		SCP_106,
		SCP_173,
		SCP_939
	}

	public enum UserRank
	{
		ADMIN = 5,
		PROJECT_MANAGER = 4,
		GAME_STAFF = 3,
		BETATESTER = 2,
		PATREON_SUPPORTED = 1,
		NONE = 0
	}

	public enum AmmoType
	{
		DROPPED_5 = 0, // Epsilon-11 Standard Rifle (Type 0)
		DROPPED_7 = 1, // MP7, Logicer (Type 1)
		DROPPED_9 = 2 // COM15, P90 (Type 2)
	}

	public enum RadioStatus
	{
		CLOSE = 0,
		SHORT_RANGE = 1,
		MEDIUM_RANGE = 2,
		LONG_RANGE = 3,
		ULTRA_RANGE = 4
	}

	public enum ExperienceType
	{
		KILL_ASSIST_CLASSD = 0,
		KILL_ASSIST_CHAOS_INSURGENCY = 1,
		KILL_ASSIST_NINETAILFOX = 2,
		KILL_ASSIST_SCIENTIST = 3,
		KILL_ASSIST_SCP = 4,
		KILL_ASSIST_OTHER = 5,
		USE_DOOR = 6,
		USE_LOCKDOWN = 7,
		USE_TESLAGATE = 8,
		USE_ELEVATOR = 9,
		CHEAT = 10
	}

	public enum WeaponType
	{
		COM15 = 0,
		P90 = 1,
		E11_STANDARD_RIFLE = 2,
		MP7 = 3,
		LOGICER = 4,
		USP = 5,
	}

	public abstract class Player : ICommandSender
	{
		internal bool CallSetRoleEvent { get; set; }
		protected bool ShouldCallSetRoleEvent { get => CallSetRoleEvent; } // used in the game

		public abstract TeamRole TeamRole { get; set; }
		public abstract string Name { get; }
		public abstract string IpAddress { get; }
		public abstract int PlayerId { get; }
		public abstract string SteamId { get; }
		public abstract RadioStatus RadioStatus { get; set; }
		public abstract bool OverwatchMode { get; set; }
		public abstract bool DoNotTrack { get; }
		public abstract Scp079Data Scp079Data { get; }

		public abstract void Kill(DamageType type = DamageType.NUKE);
		public abstract int GetHealth();
		public abstract void AddHealth(int amount);
		public abstract void Damage(int amount, DamageType type = DamageType.NUKE);
		public abstract void SetHealth(int amount, DamageType type = DamageType.NUKE);
		public abstract int GetAmmo(AmmoType type);
		public abstract void SetAmmo(AmmoType type, int amount);
		public abstract Vector GetPosition();
		public abstract void Teleport(Vector pos, bool unstuck = false);
		public abstract void SetRank(string color = null, string text = null, string group = null);
		public abstract string GetRankName();
		public abstract void Disconnect();
		public abstract void Disconnect(string message);
		public abstract void Ban(int duration);
		public abstract void Ban(int duration, string message);
		public abstract Item GiveItem(ItemType type);
		public abstract List<Item> GetInventory();
		public abstract Item GetCurrentItem();
		public abstract void SetCurrentItem(ItemType type);
		public abstract int GetCurrentItemIndex();
		public abstract void SetCurrentItemIndex(int index);
		public abstract bool HasItem(ItemType type);
		public abstract int GetItemIndex(ItemType type);
		public abstract bool IsHandcuffed();
		public abstract void ChangeRole(Role role, bool full = true, bool spawnTeleport = true, bool spawnProtect = true, bool removeHandcuffs = false);
		public abstract object GetGameObject();
		public abstract UserGroup GetUserGroup();
		public abstract string[] RunCommand(string command, string[] args);
		public abstract bool GetGodmode();
		public abstract void SetGodmode(bool godmode);
		public abstract Vector GetRotation();
		public abstract void SendConsoleMessage(string message, string color = "green");
		public abstract void Infect(float time);
		public abstract void ThrowGrenade(ItemType grenadeType, bool isCustomDirection, Vector direction, bool isEnvironmentallyTriggered, Vector position, bool isCustomForce, float throwForce, bool slowThrow = false);
		public abstract bool GetBypassMode();
		public abstract string GetAuthToken();
		public abstract void HideTag(bool enable);
		public abstract void PersonalBroadcast(uint duration, string message, bool isMonoSpaced);
		public abstract void PersonalClearBroadcasts();
		public bool HasPermission(string permissionName)
		{
			return PluginManager.Manager.PermissionsManager.CheckPermission(this, permissionName);
		}
		/// <summary>  
		/// Get SCP-106's portal position. Returns zero if Player is not SCP-106 or SCP-106 hasn't created one.
		/// </summary> 
		public abstract Vector Get106Portal();
		public abstract void SetRadioBattery(int battery);
		public abstract void HandcuffPlayer(Player playerToHandcuff);
		public abstract void RemoveHandcuffs();
		public abstract bool GetGhostMode();
		public abstract void SetGhostMode(bool ghostMode, bool visibleToSpec = true, bool visibleWhenTalking = true);
	}

	public abstract class Scp079Data
	{
		public abstract float Exp { get; set; }
		public abstract int ExpToLevelUp { get; set; }
		public abstract int Level { get; set; }
		public abstract float AP { get; set; }
		public abstract float APPerSecond { get; set; }
		public abstract float MaxAP { get; set; }
		public abstract float SpeakerAPPerSecond { get; set; }
		public abstract float LockedDoorAPPerSecond { get; set; }
		public abstract float Yaw { get; }
		public abstract float Pitch { get; }
		public abstract Room Speaker { get; set; }
		public abstract Vector Camera { get; } //todo: implement api object

		public abstract Door[] GetLockedDoors();
		public abstract void Lock(Door door);
		public abstract void Unlock(Door door);
		public abstract void TriggerTesla(TeslaGate tesla);
		public abstract void Lockdown(Room room);
		public abstract void SetCamera(Vector position, bool lookAt = false);
		public abstract void ShowGainExp(ExperienceType expType);
		public abstract void ShowLevelUp(int level);
		public abstract object GetComponent();
	}
}
