namespace Smod2.API
{
	public enum Team
	{
		NONE = -1,
		SCP = 0,
		NINETAILFOX = 1,
		CHAOS_INSURGENCY = 2,
		SCIENTIST = 3,
		CLASSD = 4,
		SPECTATOR = 5,
		TUTORIAL = 6
	}

	public enum Role
	{
		UNASSIGNED = -1,
		SCP_173 = 0,
		CLASSD = 1,
		SPECTATOR = 2,
		SCP_106 = 3,
		NTF_SCIENTIST = 4,
		SCP_049 = 5,
		SCIENTIST = 6,
		SCP_079 = 7,
		CHAOS_INSURGENCY = 8,
		SCP_096 = 9,
		SCP_049_2 = 10,
		ZOMBIE = 10,
		NTF_LIEUTENANT = 11,
		NTF_COMMANDER = 12,
		NTF_CADET = 13,
		TUTORIAL = 14,
		FACILITY_GUARD = 15,
		SCP_939_53 = 16,
		SCP_939_89 = 17
	}

	public abstract class TeamRole
	{
		public abstract Team Team { get; }
		public abstract Role Role { get; }
		public abstract bool RoleDisallowed { get; set; }
		public abstract int MaxHP { get; set; }
		public abstract string Name { get; set; }
		public abstract object GetClass();
	}
}
