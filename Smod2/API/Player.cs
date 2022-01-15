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

	// Based on the base game PlayerStatsSystem.DeathTranslations class with weapons added
	public enum DamageType
	{
		NONE = 0,
		RECONTAINED = 1,
		WARHEAD = 2,
		SCP_049 = 3,
		UNKNOWN = 4,
		ASPHYXIATED = 5,
		BLEEDING = 6,
		FALLING = 7,
		POCKET_DECAY = 8,
		DECONTAMINATION = 9,
		POISON = 10,
		SCP_207 = 11,
		SEVERED_HANDS = 12,
		MICRO_HID = 13,
		TESLA = 14,
		EXPLOSION = 15,
		SCP_096 = 16,
		SCP_173 = 17,
		SCP_939 = 18,
		SCP_049_2 = 19,
		UNKNOWN_FIREARM = 20,
		CRUSHED = 21,
		FEMUR_BREAKER = 22,
		FRIENDLY_FIRE_PUNISHMENT = 23,

		// Remaining weapons
		COM15 = 24,
		E11_SR = 25,
		CROSSVEC = 26,
		FSP9 = 27,
		LOGICER = 28,
		COM18 = 29,
		REVOLVER = 30,
		AK = 31,
		SHOTGUN = 32,

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

	public abstract class Player : ICommandSender, IEquatable<Player>
	{
		internal bool callSetRoleEvent { get; set; }
		protected bool shouldCallSetRoleEvent { get => callSetRoleEvent; } // used in the game
		public abstract TeamRole teamRole { get; set; }
		public abstract string name { get; }
		public abstract string displayedNickname { get; set; } // Differs from the Name in that it can be modified by server.
		public abstract string ipAddress { get; }
		public abstract int playerID { get; }
		public abstract string userID { get; }
		public abstract UserIdType userIDType { get; }
		public abstract RadioStatus radioStatus { get; set; }
		public abstract bool overwatchMode { get; set; }
		public abstract bool doNotTrack { get; }
		public abstract Scp079Data scp079Data { get; }
		public abstract float artificialHealth { get; set; }
		public abstract float health { get; set; }
		public abstract float stamina { get; set; }
		public abstract bool godMode { get; set; }
		public abstract bool bypassMode { get; set; }
		public abstract bool muted { get; set; }
		public abstract bool intercomMuted { get; set; }
		public string GetParsedUserID()
        {
            if (!string.IsNullOrWhiteSpace(userID))
            {
                int charLocation = userID.LastIndexOf('@');

                if (charLocation > 0)
                {
                    return userID.Substring(0, charLocation);
                }
            }

            return null;
        }
		public abstract Room GetCurrentRoom();
		public abstract List<PlayerEffect> GetAllPlayerEffects();
        public abstract PlayerEffect GetPlayerEffect(StatusEffect effectToReturn);
        public abstract void Kill(string reason);
		public abstract void Damage(string reason, float amount, string cassieDeathAnnouncement = "");
		public abstract int GetAmmo(AmmoType type);
		public abstract void SetAmmo(AmmoType type, int amount);
		public abstract Vector GetPosition();
		public abstract void Teleport(Vector pos, bool unstuck = false);
		public abstract void SetRank(string color = null, string text = null, string group = null);
		public abstract void ShowHint(string text, float durationInSeconds = 1);
		public abstract string GetRankName();
		public abstract void Disconnect();
		public abstract void Disconnect(string message);
		public abstract void Ban(int duration);
        public abstract void Ban(int duration, string message);
        public abstract Item GiveItem(ItemType type);
        public abstract List<Item> GetInventory();
        public abstract Item GetCurrentItem();
        public abstract void SetCurrentItem(ItemType type);
        public abstract ushort GetCurrentItemSerialNumber();
        public abstract void SetCurrentItem(ushort serialNumber);
        public abstract bool HasItem(ItemType type);
        public abstract ushort GetInventoryItemSerialNumber(ItemType type);
        public abstract void ClearInventory();
        public abstract bool IsHandcuffed();
        public abstract string[] RunCommand(string command, string[] args);
		public abstract void ChangeRole(RoleType role, bool full = true, bool spawnTeleport = true, bool spawnProtect = true, bool removeHandcuffs = false);
		public abstract object GetGameObject();
		public abstract UserGroup GetUserGroup();
		public abstract Vector GetRotation();
		public abstract void SendConsoleMessage(string message, string color = "green");
		public abstract void Infect(float time);
		public abstract void ThrowGrenade(GrenadeType grenadeType, Vector direction = null, float throwForce = 1f, bool slowThrow = false);
		public abstract void HideTag(bool enable);
		public abstract void PersonalClearBroadcasts();
		public abstract string GetAuthToken();
		public abstract void PersonalBroadcast(uint duration, string message, bool isMonoSpaced);
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

		public override bool Equals(object obj)
		{
			return Equals(obj as Player);
		}

		public bool Equals(Player other)
		{
			return other != null &&
				   playerID == other.playerID;
		}

		public override int GetHashCode()
		{
			return 956575109 + playerID.GetHashCode();
		}

		public static bool operator ==(Player left, Player right)
		{
			return EqualityComparer<Player>.Default.Equals(left, right);
		}

		public static bool operator !=(Player left, Player right)
		{
			return !(left == right);
		}
	}
}
