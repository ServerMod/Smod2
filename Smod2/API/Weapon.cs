using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public enum WeaponType
	{
		COM15     = ItemType.GUN_COM15,
		MICRO_HID = ItemType.MICRO_HID,
		E11_SR    = ItemType.GUN_E11_SR,
		CROSSVEC  = ItemType.GUN_CROSSVEC,
		FSP9      = ItemType.GUN_FSP9,
		LOGICER   = ItemType.GUN_LOGICER,
		COM18     = ItemType.GUN_COM18,
		REVOLVER  = ItemType.GUN_REVOLVER,
		AK        = ItemType.GUN_AK,
		SHOTGUN   = ItemType.GUN_SHOTGUN
	}

	public enum WeaponSound
	{
		COM15 = 0,
		P90 = 1,
		E11_STANDARD_RIFLE = 2,
		MP7 = 3,
		LOGICER = 4,
		USP = 5,
	}

	public enum AttachmentType
	{
		NONE = 0,
		IRON_SIGHTS = 1,
		DOT_SIGHT = 2,
		HOLO_SIGHT = 3,
		NIGHT_VISION_SIGHT = 4,
		AMMO_SIGHT = 5,
		SCOPE_SIGHT = 6,
		STANDARD_STOCK = 7,
		EXTENDED_STOCK = 8,
		RETRACTED_STOCK = 9,
		LIGHTWEIGHT_STOCK = 10,
		HEAVY_STOCK = 11,
		RECOIL_REDUCING_STOCK = 12,
		FOREGRIP = 13,
		LASER = 14,
		FLASHLIGHT = 15,
		AMMO_COUNTER = 16,
		STANDARD_BARREL = 17,
		EXTENDED_BARREL = 18,
		SOUND_SUPPRESSOR = 19,
		FLASH_HIDER = 20,
		MUZZLE_BRAKE = 21,
		MUZZLE_BOOSTER = 22,
		STANDARD_MAG_FMJ = 23,
		STANDARD_MAG_AP = 24,
		STANDARD_MAG_JHP = 25,
		EXTENDED_MAG_FMJ = 26,
		EXTENDED_MAG_AP = 27,
		EXTENDED_MAG_JHP = 28,
		DRUM_MAG_FMJ = 29,
		DRUM_MAG_AP = 30,
		DRUM_MAG_JHP = 31,
		LOW_CAP_MAG_FMJ = 32,
		LOW_CAP_MAG_AP = 33,
		LOW_CAP_MAG_JHP = 34,
		CYLINDER_MAG4 = 35,
		CYLINDER_MAG6 = 36,
		CYLINDER_MAG8 = 37,
		CARBINE_BODY = 38,
		RIFLE_BODY = 39,
		SHORT_BARREL = 40,
		SHOTGUN_CHOKE = 41,
		SHOTGUN_EXTENDED_BARREL = 42,
		NO_RIFLE_STOCK = 43
	}

	public enum AttachmentSlot
	{
		SIGHT = 0,
		BARREL = 1,
		SIDE_RAIL = 2,
		BOTTOM_RAIL = 3,
		AMMUNITION = 4,
		STOCK = 5,
		STABILITY = 6,
		BODY = 7,
		UNASSIGNED = byte.MaxValue
	}

	public enum HitBoxType
	{
		NONE = -1,
		LEG = 0, // Arm and leg should possibly be changed to limb
		ARM = 1,
		BODY = 2,
		HEAD = 3,
		WINDOW = 4,
		HOLE = 5
	}

	public enum AmmoType
	{
		NONE = ItemType.NONE,
		AMMO_12_GAUGE = ItemType.AMMO_12_GAUGE,
		AMMO_556_X45 = ItemType.AMMO_556_X45,
		AMMO_44_CAL = ItemType.AMMO_44_CAL,
		AMMO_762_X39 = ItemType.AMMO_762_X39,
		AMMO_9_X19 = ItemType.AMMO_9_X19
	}

	public abstract class Weapon : IEquatable<Weapon>
	{
		public abstract WeaponType WeaponType { get; }
		public abstract Dictionary<AttachmentSlot, AttachmentType> Attachments { get; }
		public abstract int AmmoInClip { get; set; }
		public abstract int MaxClipSize { get; }
		public abstract AmmoType AmmoType { get; }
		public abstract DamageType DamageType { get; }
		public abstract ushort SerialNumber { get; }
		public abstract object GetComponent();
		public abstract Item ToItem();

		public abstract bool SetAttachment(AttachmentType attachment, bool enabled);

		public override bool Equals(object obj)
		{
			return Equals(obj as Weapon);
		}

		public bool Equals(Weapon other)
		{
			return other != null && SerialNumber == other.SerialNumber;
		}

		public override int GetHashCode()
		{
			return 1780733181 + SerialNumber.GetHashCode();
		}

		public static bool operator ==(Weapon left, Weapon right)
		{
			return EqualityComparer<Weapon>.Default.Equals(left, right);
		}

		public static bool operator !=(Weapon left, Weapon right)
		{
			return !(left == right);
		}
	}
}
