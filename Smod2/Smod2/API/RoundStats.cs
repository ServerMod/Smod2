using System;

namespace Smod2.API
{
	public abstract class RoundStats
	{
		public abstract int NTFAlive { get; }
		public abstract int ScientistsAlive { get; }
		public abstract int ScientistsEscaped { get; }
		public abstract int ScientistsDead { get; }
		public abstract int ScientistsStart { get; }
		public abstract int ClassDEscaped { get; }
		public abstract int ClassDDead { get; }
		public abstract int ClassDAlive { get; }
		public abstract int ClassDStart { get; }
		public abstract int Zombies { get; }
		public abstract int SCPDead { get; }
		public abstract int SCPKills { get; }
		public abstract int SCPAlive { get; }
		public abstract int SCPStart { get; }
		public abstract bool WarheadDetonated { get; }
	}
}
