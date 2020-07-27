namespace Smod2.API
{
	public enum ItemType
	{
		KEYCARD_JANITOR = 0,
		KEYCARD_SCIENTIST = 1,
		KEYCARD_SCIENTIST_MAJOR = 2,
		KEYCARD_ZONEMANAGER = 3,
		KEYCARD_GUARD = 4,
		KEYCARD_SENIOR_GUARD = 5,
		KEYCARD_CONTAINMENT_ENGINEER = 6,
		KEYCARD_NTF_LIEUTENANT = 7,
		KEYCARD_NTF_COMMANDER = 8,
		KEYCARD_FACILITY_MANAGER = 9,
		KEYCARD_CHAOS_INSURGENCY = 10,
		KEYCARD_O5 = 11,
		RADIO = 12,
		COM15 = 13,
		MEDKIT = 14,
		FLASHLIGHT = 15,
		MICROHID = 16,
		SCP_500 = 17,
		SCP_207 = 18,
		WEAPON_MANAGER_TABLET = 19,
		EPSILON_11_SR = 20,
		PROJECT90 = 21,
		AMMO_556 = 22,
		MP7 = 23,
		LOGICER = 24,
		FRAG_GRENADE = 25,
		FLASH_GRENADE = 26,
		DISARMER = 27,
		AMMO762 = 28,
		AMMO9MM = 29,
		USP = 30,
		SCP_018 = 31,
		SCP_268 = 32,
		ADRENALINE = 33,
		PAINKILLERS = 34,
		COIN = 35,
		NONE = 36,
	}

	public enum KnobSetting
	{
		ROUGH = 0,
		COARSE = 1,
		ONE_TO_ONE = 2,
		FINE = 3,
		VERY_FINE = 4
	}

	public abstract class Item
	{
		public abstract bool InWorld { get; }
		public abstract ItemType ItemType { get; }
		public abstract void Remove();
		public abstract void Drop();
		public abstract Vector GetPosition();
		public abstract void SetPosition(Vector pos);
		public abstract void SetKinematic(bool doPhysics);
		public abstract bool GetKinematic();
		public abstract object GetComponent();
		public abstract bool IsWeapon { get; }
		public abstract Weapon ToWeapon();
	}
}
