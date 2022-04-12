using System;

namespace Smod2.API
{
	public abstract class RoundStats
	{
		public abstract int Kills { get; set; }
		public abstract int MTFAlive { get; }
		public abstract int CIAlive { get; }
		public abstract int ScientistsAlive { get; }
		public abstract int ScientistsEscaped { get; set; }
		public abstract int ScientistsDead { get; }
		public abstract int ScientistsStart { get; }
		public abstract int DClassEscaped { get; set; }
		public abstract int DClassDead { get; }
		public abstract int DClassAlive { get; }
		public abstract int DClassStart { get; }
		public abstract int Zombies { get; }
		public abstract int ChangedIntoZombies { get; set; }
		public abstract int SCPDead { get; }
		public abstract int SCPKills { get; set; }
		public abstract int SCPAlive { get; }
		public abstract int SCPStart { get; }
		public abstract int GrenadeKills { get; set; }
		public abstract bool WarheadDetonated { get; }
	}
}
