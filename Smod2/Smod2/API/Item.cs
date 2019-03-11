namespace Smod2.API
{
	public enum ItemType
	{
		NULL = -1,
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
		RADIO = 12,
		COM15 = 13,
		MEDKIT = 14,
		FLASHLIGHT = 15,
		MICROHID = 16,
		COIN = 17,
		CUP = 18,
		WEAPON_MANAGER_TABLET = 19,
		E11_STANDARD_RIFLE = 20,
		P90 = 21,
		DROPPED_5 = 22,
		MP4 = 23,
		LOGICER = 24,
		FRAG_GRENADE = 25,
		FLASHBANG = 26,
		DISARMER = 27,
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
	}
}
