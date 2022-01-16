using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public enum ItemType
	{
		NONE = -1,
		KEYCARD_JANITOR = 0,
		KEYCARD_SCIENTIST = 1,
		KEYCARD_RESEARCH_COORDINATOR = 2,
		KEYCARD_ZONE_MANAGER = 3,
		KEYCARD_GUARD = 4,
		KEYCARD_NTF_OFFICER = 5,
		KEYCARD_CONTAINMENT_ENGINEER = 6,
		KEYCARD_NTF_LIEUTENANT = 7,
		KEYCARD_NTF_COMMANDER = 8,
		KEYCARD_FACILITY_MANAGER = 9,
		KEYCARD_CHAOS_INSURGENCY = 10,
		KEYCARD_O5 = 11,
		RADIO = 12,
		GUN_COM15 = 13,
		MEDKIT = 14,
		FLASHLIGHT = 15,
		MICRO_HID = 16,
		SCP500 = 17,
		SCP207 = 18,
		AMMO_12_GAUGE = 19,
		GUN_E11_SR = 20,
		GUN_CROSSVEC = 21,
		AMMO_556_X45 = 22,
		GUN_FSP9 = 23,
		GUN_LOGICER = 24,
		GRENADE_HE = 25,
		GRENADE_FLASH = 26,
		AMMO_44_CAL = 27,
		AMMO_762_X39 = 28,
		AMMO_9_X19 = 29,
		GUN_COM18 = 30,
		SCP018 = 31,
		SCP268 = 32,
		ADRENALINE = 33,
		PAINKILLERS = 34,
		COIN = 35,
		ARMOR_LIGHT = 36,
		ARMOR_COMBAT = 37,
		ARMOR_HEAVY = 38,
		GUN_REVOLVER = 39,
		GUN_AK = 40,
		GUN_SHOTGUN = 41,
		SCP330 = 42,
		SCP2176 = 43
	}

	public enum KnobSetting
	{
		ROUGH = 0,
		COARSE = 1,
		ONE_TO_ONE = 2,
		FINE = 3,
		VERY_FINE = 4
	}

	public abstract class Item : IEquatable<Item>
	{
		public abstract bool InWorld { get; }
		public abstract ItemType ItemType { get; }
		public abstract bool IsWeapon { get; }
        /// <summary>
        /// Used so IEquatable is possible so you can compare items.
        /// </summary>
        public abstract ushort SerialNumber { get; }
		public abstract void Remove();
		public abstract void Drop();
		public abstract Vector GetPosition();
		public abstract void SetPosition(Vector pos);
		public abstract void SetKinematic(bool doPhysics);
		public abstract bool GetKinematic();
		public abstract object GetComponent();
		public abstract Weapon ToWeapon();

		public override bool Equals(object obj)
		{
			return Equals(obj as Item);
		}

		public bool Equals(Item other)
		{
			return other != null && SerialNumber != 0 && other.SerialNumber != 0 &&
				   SerialNumber == other.SerialNumber;
		}

		public override int GetHashCode()
		{
			return 1780733181 + SerialNumber.GetHashCode();
		}

		public static bool operator ==(Item left, Item right)
		{
			return EqualityComparer<Item>.Default.Equals(left, right);
		}

		public static bool operator !=(Item left, Item right)
		{
			return !(left == right);
		}
	}
}
