using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smod2.API
{

	public enum AttachmentType
	{
		NONE = 0,
		SIGHT = 1,
		BARREL = 2,
		OTHER = 3,
	}

	public enum WeaponType
	{
		COM15 = (int)ItemType.COM15,
		MICROHID = (int)ItemType.MICROHID,
		E11_STANDARD_RIFLE = (int)ItemType.E11_STANDARD_RIFLE,
		P90 = (int)ItemType.P90,
		MP4 = (int)ItemType.MP4,
		LOGICER = (int)ItemType.LOGICER,
		USP = (int)ItemType.USP,
	}

	public enum WeaponSight
	{
		NONE = 0,
		RED_DOT = 1,
		HOLO_SIGHT = 2,
		NIGHT_VISION = 3,
		SNIPER_SCOPE = 4,
		COLLIMATOR = 5,
	}

	public enum WeaponBarrel
	{
		NONE = 0,
		SUPRESSOR = 1,
		OIL_FILTER = 2,
		MUZZLE_BREAK = 3,
		HEAVY_BARREL = 4,
		MUZZLE_BOOSTER = 5,
	}

	public enum WeaponOther
	{
		NONE = 0,
		FLASHLIGHT = 1,
		LASER = 2,
		AMMO_COUNTER = 3,
		GYROSCOPIC_STABILIZER = 4,
	}

	public abstract class Weapon
	{
		public abstract WeaponType WeaponType { get; }
		public abstract WeaponSight GetSight();
		public abstract WeaponBarrel GetBarrel();
		public abstract WeaponOther GetOther();
		public abstract void SetAttachments(WeaponSight Sight, WeaponBarrel Barrel, WeaponOther Other);
		public abstract void SetAmountOfAmmoInClip(float ammo);
		public abstract float GetAmmoLeftInClip();
		public abstract int GetMaxClipSize();
		public abstract AmmoType GetAmmoType();
		public abstract object GetComponent();
	}
}
