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
		NONE = -1,
		RECONTAINED,
		WARHEAD,
		SCP_049,
		UNKNOWN,
		ASPHYXIATED,
		BLEEDING,
		FALLING,
		POCKET_DECAY,
		DECONTAMINATION,
		POISON,
		SCP_207,
		SEVERED_HANDS,
		MICRO_HID,
		TESLA,
		EXPLOSION,
		SCP_096,
		SCP_173,
		SCP_939,
		SCP_049_2,
		UNKNOWN_FIREARM,
		CRUSHED,
		FEMUR_BREAKER,
		FRIENDLY_FIRE_PUNISHMENT,
		HYPOTHERMIA,

		// SCP-106 doesn't have a specific entry for it's normal attack in the base game so we add it here too
		SCP_106,
		// SCP-018 also doesn't have an entry at all
		SCP_018,

		// Remaining weapons
		COM15,
		E11_SR,
		CROSSVEC,
		FSP9,
		LOGICER,
		COM18,
		REVOLVER,
		AK,
		SHOTGUN
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
		internal bool CallSetRoleEvent { get; set; }
		protected bool ShouldCallSetRoleEvent { get => CallSetRoleEvent; } // used in the game
		public abstract Role PlayerRole { get; set; }
		public abstract string Name { get; }
		public abstract string DisplayedNickname { get; set; } // Differs from the Name in that it can be modified by server.
		public abstract string IPAddress { get; }
		public abstract int PlayerID { get; }
		public abstract string UserID { get; }
		public abstract UserIdType UserIDType { get; }
		public abstract RadioStatus RadioStatus { get; set; }
		public abstract bool OverwatchMode { get; set; }
		public abstract bool DoNotTrack { get; }
		public abstract Scp079Data SCP079Data { get; }
		public abstract float ArtificialHealth { get; set; }
		public abstract float Health { get; set; }
		public abstract float Stamina { get; set; }
		public abstract bool GodMode { get; set; }
		public abstract bool BypassMode { get; set; }
		public abstract bool Muted { get; set; }
		public abstract bool IntercomMuted { get; set; }
		public string GetParsedUserID()
        {
            if (!string.IsNullOrWhiteSpace(UserID))
            {
                int charLocation = UserID.LastIndexOf('@');

                if (charLocation > 0)
                {
                    return UserID.Substring(0, charLocation);
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
		public abstract void ThrowGrenade(GrenadeType grenadeType, Vector direction = null, bool slowThrow = false);
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
				   PlayerID == other.PlayerID;
		}

		public override int GetHashCode()
		{
			return 956575109 + PlayerID.GetHashCode();
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
