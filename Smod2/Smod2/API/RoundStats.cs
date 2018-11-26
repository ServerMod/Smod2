using System;

namespace Smod2.API
{
	public abstract class RoundStats
	{
		public abstract int NTFAlive { get; }
		public abstract int ScientistsAlive { get; }
		public abstract int ScientistsEscaped { get; set; }
		public abstract int ScientistsDead { get; }
		public abstract int ScientistsStart { get; }
		public abstract int ClassDEscaped { get; set; }
		public abstract int ClassDDead { get; }
		public abstract int ClassDAlive { get; }
		public abstract int ClassDStart { get; }
		public abstract int Zombies { get; }
		public abstract int SCPDead { get; }
		public abstract int SCPKills { get; }
		public abstract int SCPAlive { get; }
		public abstract int SCPStart { get; }
		public abstract int GrenadeKills { get; }
		public abstract bool WarheadDetonated { get; }
		public abstract int CiAlive { get; }
	}
}
