using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public enum TeamType
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

	[Obsolete("Use TeamType instead of Team")]
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

	public enum RoleType
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

	[Obsolete("Use RoleType instead of Role")]
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
		public abstract TeamType Team { get; }
		public abstract RoleType Role { get; }
		public abstract bool RoleDisallowed { get; set; }
		public abstract int MaxHP { get; set; }
		public abstract string Name { get; set; }
		public abstract object GetClass();

		public static List<RoleType> Playables { get; } = new List<RoleType>
		{
			RoleType.SCP_049,
			RoleType.SCP_049_2,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89,
			RoleType.CLASSD,
			RoleType.CHAOS_INSURGENCY,
			RoleType.SCIENTIST,
			RoleType.FACILITY_GUARD,
			RoleType.NTF_CADET,
			RoleType.NTF_LIEUTENANT,
			RoleType.NTF_COMMANDER,
			RoleType.NTF_SCIENTIST,
			RoleType.TUTORIAL
		};

		public static List<RoleType> SCPs { get; } = new List<RoleType> {
			RoleType.SCP_049,
			RoleType.SCP_049_2,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89
		};

		public static List<RoleType> BannableSCPs { get; } = new List<RoleType> {
			RoleType.SCP_049,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89
		};

		public static List<RoleType> PickableSCPs { get; } = new List<RoleType> {
			RoleType.SCP_049,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89
		};
	}
}
