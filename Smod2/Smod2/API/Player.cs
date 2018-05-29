using System.Collections.Generic;


namespace Smod2.API
{
	public enum DamageType
	{
		NONE,
		LURE,
		SCP_049_2,
		NUKE,
		WALL,
		TESLA, // Tesla and Grenade
		SCP_173,
		SCP_106,
		CONTAIN,
		SCP_096,
		SCP_049,
		POCKET,
		FALLDOWN,
		COM15,
		E11_STANDARD_RIFLE,
		P90,
		MP7,
		LOGICER // Chaos Gun
	}

	public enum Role
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

	public abstract class Player
	{
		public abstract TeamClass Class { get; set; }
		public abstract string Name { get; }
		public abstract string IpAddress { get; }
		public abstract string SteamId { get; }

		public abstract void Kill(DamageType type = DamageType.NUKE);
		public abstract int GetHealth();
		public abstract void AddHealth(int amount);
		public abstract void Damage(int amount, DamageType type = DamageType.NUKE);
		public abstract void SetHealth(int amount, DamageType type = DamageType.NUKE);
		public abstract int GetAmmo(AmmoType type);
		public abstract void SetAmmo(AmmoType type, int amount);
		public abstract Vector GetPosition();
		public abstract void Teleport(Vector pos);
		public abstract void SetRole(string color = "", string text = "", string group = "");
		public abstract void Disconnect();
		public abstract void Disconnect(string message);
		public abstract void Ban(int duration);
		public abstract void GiveItem(ItemType type);
		public abstract List<Item> GetInventory();
		public abstract bool IsHandcuffed();
		public abstract void ChangeClass(Classes newClass, bool full = true, bool force = false);
		public abstract object GetGameObject();
	}
}
