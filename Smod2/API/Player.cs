using System;
using System.Collections.Generic;
using Smod2.Commands;

namespace Smod2.API
{
	public enum UserIdType
	{
		STEAM,
		DISCORD,
		NORTHWOOD
	}

	public enum DamageType
	{
		NONE = 0,
		LURE = 1,
		NUKE = 2,
		WALL = 3,
		DECONT = 4,
		TESLA = 5,
		FALLDOWN = 6,
		FLYING_DETECTION = 7,
		FRIENDLY_FIRE_DETECTOR = 8,
		CONTAIN = 9,
		POCKET = 10,
		RAGDOLLLESS = 11,
		COM15 = 12,
		P90 = 13,
		E11_STANDARD_RIFLE = 14,
		MP7 = 15,
		LOGICIER = 16,
		USP = 17,
		MICROHID = 18,
		GRENADE = 19,
		SCP_049 = 20,
		SCP_049_2 = 21,
		SCP_096 = 22,
		SCP_106 = 23,
		SCP_173 = 24,
		SCP_939 = 25,
		SCP_207 = 26,
		RECONTAINMENT = 27,
		BLEEDING = 28,
		POISONED = 29,
		ASPHYXIATION = 30
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
		NONE = -1,     // Has no base in-game.
		AMMO556 = 0,
		DROPPED_5 = 0, // Epsilon-11 Standard Rifle (Type 0)
		AMMO762 = 1,
		DROPPED_7 = 1, // MP7, Logicer (Type 1)
		AMMO9MM = 2,
		DROPPED_9 = 2  // COM15, P90 (Type 2)
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

	public enum GrenadeType
	{
		FRAG_GRENADE = 0,
		FLASHBANG = 1,
		SCP018 = 2
	}

	public enum StatusEffect
	{
		SCP207 = 0,
		SCP268 = 1,
		CORRODING = 2,
		VISUALS939 = 3,
		DECONTAMINATING = 4,
		SINKHOLE = 5,
		FLASHED = 6,
		AMNESIA = 7,
		BLINDED = 8,
		HEMORRHAGE = 9,
		POISONED = 10,
		BLEEDING = 11,
		DISABLED = 12,
		ENSNARED = 13,
		CONCUSSED = 14,
		BURNED = 15,
		DEAFENED = 16,
		ASPHYXIATED = 17,
		EXHAUSTED = 18,
		PANIC = 19,
		INVIGORATED = 20,
	}

	public abstract class Player : ICommandSender
	{
		internal bool CallSetRoleEvent { get; set; }
		protected bool ShouldCallSetRoleEvent { get => CallSetRoleEvent; } // used in the game

		public abstract TeamRole TeamRole { get; set; }
		public abstract string Name { get; }
		public abstract string IpAddress { get; }
		public abstract int PlayerId { get; }
		public abstract string UserId { get; }
		public abstract UserIdType UserIdType { get; }
		[Obsolete("Use UserId instead of SteamId")]
		public abstract string SteamId { get; }
		public abstract RadioStatus RadioStatus { get; set; }
		public abstract bool OverwatchMode { get; set; }
		public abstract bool DoNotTrack { get; }
		public abstract Scp079Data Scp079Data { get; }

		public string GetParsedUserID()
		{
			if (!string.IsNullOrWhiteSpace(UserId))
			{
				int charLocation = UserId.LastIndexOf('@');

				if (charLocation > 0)
				{
					return UserId.Substring(0, charLocation);
				}
			}

			return null;
		}

		//Status Effects
		public abstract List<PlayerEffect> GetAllPlayerEffects();
		public abstract PlayerEffect GetPlayerEffect(StatusEffect effectToReturn);
		//End

		public abstract void Kill(DamageType type = DamageType.NUKE);
		[Obsolete("Use HP property instead.")]
		public abstract float GetHealth();
		[Obsolete("Use HP property instead.")]
		public abstract void AddHealth(float amount);
		[Obsolete("Use AHP property instead.")]
		public abstract float GetArtificialHealth();
		[Obsolete("Use AHP property instead.")]
		public abstract void SetArtificialHealth(float amount);
		public abstract float AHP { get; set; }
		public abstract float HP { get; set; }
		public abstract void Damage(float amount, DamageType type = DamageType.NUKE);
		public abstract void SetHealth(float amount, DamageType type = DamageType.NUKE);
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
		public abstract void ChangeRole(RoleType role, bool full = true, bool spawnTeleport = true, bool spawnProtect = true, bool removeHandcuffs = false);
		public abstract object GetGameObject();
		public abstract UserGroup GetUserGroup();
		public abstract string[] RunCommand(string command, string[] args);
		public abstract bool GetGodmode();
		public abstract void SetGodmode(bool godmode);
		public abstract Vector GetRotation();
		public abstract void SendConsoleMessage(string message, string color = "green");
		public abstract void Infect(float time);
		[Obsolete("Use the other grenadetype overload since this is outdated with base game.")]
		public abstract void ThrowGrenade(GrenadeType grenadeType, bool isCustomDirection, Vector direction, bool isEnvironmentallyTriggered, Vector position, bool isCustomForce, float throwForce, bool slowThrow = false);
		[Obsolete("Use the overload with GrenadeType instead of ItemType", true)]
		public abstract void ThrowGrenade(ItemType grenadeType, bool isCustomDirection, Vector direction, bool isEnvironmentallyTriggered, Vector position, bool isCustomForce, float throwForce, bool slowThrow = false);
		public abstract void ThrowGrenade(GrenadeType grenadeType, Vector direction = null, float throwForce = 1f, bool slowThrow = false);
		public abstract bool BypassMode { get; set; }
		[Obsolete("Use BypassMode property instead.")]
		public abstract bool GetBypassMode();
		public abstract string GetAuthToken();
		public abstract void HideTag(bool enable);
		public abstract void PersonalBroadcast(uint duration, string message, bool isMonoSpaced);
		public abstract void PersonalClearBroadcasts();
		public abstract bool Muted { get; set; }
		public abstract bool IntercomMuted { get; set; }
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
