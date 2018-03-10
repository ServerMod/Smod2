using System.Collections.Generic;

namespace Smod2.API
{
	public abstract class Round
	{
		public abstract RoundStats Stats { get; }
		public abstract void EndRound();
		public abstract int Duration { get; }
	}
}
