namespace Smod2.API
{
	public enum ItemType
	{
		Keycard_Janitor = 0,
		Keycard_Scientist = 1,
		Keycard_Scientist_Major = 2,
		Keycard_ZoneManager = 3,
		Keycard_Guard = 4,
		Keycard_Senior_Guard = 5,
		Keycard_Containment_Engineer = 6,
		Keycard_NTF_Lieutenant = 7,
		Keycard_NTF_Commander = 8,
		Keycard_Facility_Manager = 9,
		Keycard_Chaos_Insurgency = 10,
		Keycard_O5 = 11,
		Radio = 12,
		COM15 = 13,
		Medkit = 14,
		Flashlight = 15,
		MicroHID = 16,
		SCP_500 = 17,
		SCP_207 = 18,
		Weapon_Manager_Tablet = 19,
		E11SR = 20,
		Project90 = 21,
		Ammo_556 = 22,
		MP7 = 23,
		Logicer = 24,
		GrenadeFrag = 25,
		GrenadeFlash = 26,
		Disarmer = 27,
		Ammo762 = 28,
		Ammo9mm = 29,
		USP = 30,
		SCP_018 = 31,
		SCP_268 = 32,
		Adrenaline = 33,
		Painkillers = 34,
		Coin = 35,
		None = 36,
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
