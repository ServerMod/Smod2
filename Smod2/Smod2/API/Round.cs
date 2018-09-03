
namespace Smod2.API
{
	public enum ROUND_END_STATUS {
		ON_GOING,
		MTF_VICTORY,
		SCP_VICTORY,
		SCP_CI_VICTORY,
		CI_VICTORY,
		NO_VICTORY,
		FORCE_END,
		OTHER_VICTORY
	}

	public abstract class Round
	{
		public abstract RoundStats Stats { get; }
		public abstract void EndRound();
		public abstract int Duration { get; }
		public abstract void AddNTFUnit(string unit);
		public abstract void MTFRespawn(bool isCI);
		public abstract void RestartRound();
	}
}
