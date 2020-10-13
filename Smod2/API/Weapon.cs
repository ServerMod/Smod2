using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public enum WeaponType
	{
		COM15 = (int)ItemType.COM15,
		MICROHID = (int)ItemType.MICROHID,
		E11_STANDARD_RIFLE = (int)ItemType.E11_STANDARD_RIFLE,
		P90 = (int)ItemType.P90,
		MP7 = (int)ItemType.MP7,
		LOGICER = (int)ItemType.LOGICER,
		USP = (int)ItemType.USP
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

	public enum WeaponSight
	{
		NONE = 0,
		RED_DOT = 1,
		HOLO_SIGHT = 2,
		NIGHT_VISION = 3,
		SNIPER_SCOPE = 4,
		COLLIMATOR = 5
	}

	public enum WeaponBarrel
	{
		NONE = 0,
		SUPPRESSOR = 1,
		OIL_FILTER = 2,
		MUZZLE_BREAK = 3,
		HEAVY_BARREL = 4,
		MUZZLE_BOOSTER = 5
	}

	public enum WeaponOther
	{
		NONE = 0,
		FLASHLIGHT = 1,
		LASER = 2,
		AMMO_COUNTER = 3,
		GYROSCOPIC_STABILIZER = 4
	}

	public enum AttachmentType
	{
		NONE = -1,
		SIGHT = 0,
		BARREL = 1,
		OTHER = 2
	}

	public abstract class Weapon : IEquatable<Weapon>
	{
		public abstract WeaponType WeaponType { get; }
		public abstract WeaponSight Sight { get; set; }
		public abstract WeaponBarrel Barrel { get; set; }
		public abstract WeaponOther Other { get; set; }
		public abstract float AmmoInClip { get; set; }
		public abstract int MaxClipSize { get; }
		public abstract AmmoType AmmoType { get; }
		public abstract DamageType DamageType { get; }
		public abstract object GetComponent();
		public abstract int UniqueIdentifier { get; }
		public abstract Item ToItem();

		public override bool Equals(object obj)
		{
			return Equals(obj as Weapon);
		}

		public bool Equals(Weapon other)
		{
			return other != null &&
				   UniqueIdentifier == other.UniqueIdentifier;
		}

		public override int GetHashCode()
		{
			return 1780733181 + UniqueIdentifier.GetHashCode();
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
