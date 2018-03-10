using System.Collections.Generic;

namespace Smod2.API
{
	public enum DamageType
	{
		LURE,
		SCP_049_2,
		NUKE,
		TESLA,
		SCP_173,
		SCP_106,
		SCP_049,
		POCKET,
		FALLDOWN,
		PULSE_RIFLE,
		PULSE_PISTOL,
		SCORPION,
		PISTOL,
		HEAVY_MG // grenade?
	}
	public abstract class Player
	{
		public abstract TeamClass Class { get; set; }
		public abstract string Name { get; }
		public abstract string IpAddress { get; }
		public abstract string HardwareId { get; }

		public abstract void Kill();
		public abstract void Damage(int amount, DamageType type = DamageType.NUKE);
		public abstract void Disconnect();
		public abstract void Ban(int duration);
		public abstract void GiveItem(ItemType type);
		public abstract List<Item> GetInventory();
	}
}
