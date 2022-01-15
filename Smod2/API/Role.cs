using System;
using System.Collections.Generic;

namespace Smod2.API
{
	public enum TeamType
	{
		NONE = -1,
		SCP = 0,
		MTF = 1,
		CHAOS_INSURGENCY = 2,
		SCIENTIST = 3,
		D_CLASS = 4,
		SPECTATOR = 5,
		TUTORIAL = 6
	}

	public enum RoleType
	{
		NONE = -1,
		SCP_173,
		D_CLASS,
		SPECTATOR,
		SCP_106,
		MTF_SPECIALIST,
		SCP_049,
		SCIENTIST,
		SCP_079,
		CHAOS_CONSCRIPT,
		SCP_096,
		SCP_049_2,
		MTF_SERGEANT,
		MTF_CAPTAIN,
		MTF_PRIVATE,
		TUTORIAL,
		FACILITY_GUARD,
		SCP_939_53,
		SCP_939_89,
		CHAOS_RIFLEMAN,
		CHAOS_REPRESSOR,
		CHAOS_MARAUDER
	}

	public abstract class Role
	{
		public abstract TeamType team { get; }
		public abstract RoleType role { get; }
		public abstract bool roleDisallowed { get; set; }
		public abstract int maxHP { get; set; }
		public abstract string name { get; set; }
		public abstract object GetClass();

		public static HashSet<RoleType> playables { get; } = new HashSet<RoleType>
		{
			RoleType.SCP_049,
			RoleType.SCP_049_2,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89,
			RoleType.D_CLASS,
			RoleType.SCIENTIST,
			RoleType.FACILITY_GUARD,
			RoleType.MTF_SERGEANT,
			RoleType.MTF_CAPTAIN,
			RoleType.MTF_PRIVATE,
			RoleType.MTF_SPECIALIST,
			RoleType.TUTORIAL,
			RoleType.CHAOS_CONSCRIPT,
			RoleType.CHAOS_RIFLEMAN,
			RoleType.CHAOS_REPRESSOR,
			RoleType.CHAOS_MARAUDER,
		};

		public static HashSet<RoleType> scps { get; } = new HashSet<RoleType> {
			RoleType.SCP_049,
			RoleType.SCP_049_2,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89
		};

		public static HashSet<RoleType> banableSCPs { get; } = new HashSet<RoleType> {
			RoleType.SCP_049,
			RoleType.SCP_079,
			RoleType.SCP_096,
			RoleType.SCP_106,
			RoleType.SCP_173,
			RoleType.SCP_939_53,
			RoleType.SCP_939_89
		};

		public static HashSet<RoleType> pickableSCPs { get; } = new HashSet<RoleType> {
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
