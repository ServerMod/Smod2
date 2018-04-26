using System;

namespace Smod2.API
{
	public enum Teams
	{
		NONE = -1,
		SCP = 0,
		NINETAILFOX = 1,
		CHAOS_INSURGENCY = 2,
		SCIENTISTS = 3,
		CLASSD = 4,
		SPECTATOR = 5,
		TUTORIAL = 6
	}

	public enum Classes
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
		CHAOS_INSUGENCY = 8,
		SCP_096 = 9,
		SCP_049_2 = 10,
		ZOMBIE = 10,
		NTF_LIEUTENANT = 11,
		NTF_COMMANDER = 12,
		NTF_GUARD = 13,
		TUTORIAL = 14
	}

	public abstract class TeamClass
	{
		public abstract Teams Team { get; }
		public abstract Classes ClassType { get; }
		public abstract bool ClassDisallowed { get; set; }
		public abstract int MaxHP { get; set; }
		public abstract string Name { get; set; }
	}
}
