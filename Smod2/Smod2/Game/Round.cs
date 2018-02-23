namespace Smod2.Game
{
	public abstract class Round
	{
		public abstract RoundStats stats { get; }
		public abstract void EndRound();
		public int Duration { get; }
	}
}
