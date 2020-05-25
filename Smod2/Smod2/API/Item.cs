namespace Smod2.API
{
	public enum ItemType
	{
		NONE = -1,
		KEYCARD_JANITOR = 0,
		KEYCARD_SCIENTIST = 1,
		KEYCARD_SCIENTIST_MAJOR = 2,
		KEYCARD_ZONE_MANAGER = 3,
		KEYCARD_GUARD = 4,
		KEYCARD_SENIOR_GUARD = 5,
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
		WEAPON_MANAGER_TABLET = 19,
		GUN_E11_SR = 20,
		GUN_PROJECT90 = 21,
		AMMO556 = 22,
		GUN_MP7 = 23,
		GUN_LOGICER = 24,
		GRENADE_FRAG = 25,
		GRENADE_FLASH = 26,
		DISARMER = 27,
		AMMO762 = 28,
		AMMO9MM = 29,
		GUN_USP = 30,
		SCP018 = 31,
		SCP268 = 32,
		ADRENALINE = 33,
		PAINKILLERS = 34,
		COIN = 35,

		NULL = -1,
		CUP = -1,
		JANITOR_KEYCARD = 0,
		SCIENTIST_KEYCARD = 1,
		MAJOR_SCIENTIST_KEYCARD = 2,
		ZONE_MANAGER_KEYCARD = 3,
		GUARD_KEYCARD = 4,
		SENIOR_GUARD_KEYCARD = 5,
		CONTAINMENT_ENGINEER_KEYCARD = 6,
		MTF_LIEUTENANT_KEYCARD = 7,
		MTF_COMMANDER_KEYCARD = 8,
		FACILITY_MANAGER_KEYCARD = 9,
		CHAOS_INSURGENCY_DEVICE = 10,
		O5_LEVEL_KEYCARD = 11,
		COM15 = 13,
		MICROHID = 16,
		E11_STANDARD_RIFLE = 20,
		P90 = 21,
		DROPPED_5 = 22,
		MP7 = 23,
		LOGICER = 24,
		FRAG_GRENADE = 25,
		FLASHBANG = 26,
		DROPPED_7 = 28,
		DROPPED_9 = 29,
		USP = 30
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
