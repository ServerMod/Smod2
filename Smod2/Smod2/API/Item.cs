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
		M1911_PISTOL = 13,
		MEDKIT = 14,
		FLASHLIGHT = 15,
		MICROHID = 16,
		COIN = 17,
		CUP = 18,
		AMMOMETER = 19,
		E11_STANDARD_RIFLE = 20,
		SBX7_PISTOL = 21,
		DROPPED_SFA = 22,
		SKORPION_SMG = 23,
		LOGICER = 24,
		POSITRON_GRENADE = 25,
		SMOKE_GRENADE = 26,
		DISARMER = 27,
		DROPPED_RAT = 28,
		DROPPED_PAT = 29
	}

	public enum KnobSetting
	{
		ROUGH,
		COARSE,
		ONE_TO_ONE,
		FINE,
		VERY_FINE
	}

	public abstract class Item
	{
		public abstract bool InWorld { get; }
		public abstract ItemType ItemType { get; }
		public abstract void Remove();
		public abstract void Drop();
	}
}
