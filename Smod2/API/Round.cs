
namespace Smod2.API
{
	public enum RoundEndStatus {
		ON_GOING,
		MTF_VICTORY,
		SCP_VICTORY,
		SCP_CI_VICTORY,
		CI_VICTORY,
		NO_VICTORY,
		FORCE_END,
		OTHER_VICTORY
	}
	public enum LeadingTeam
	{
		FACILITY_FORCES = 0,
		CHAOS_INSURGENCY = 1,
		ANOMALIES = 2,
		DRAW = 3
	}
	public abstract class Round
	{
		public abstract RoundStats Stats { get; }
		public abstract void EndRound();
		public abstract int Duration { get; }
		public abstract void AddNTFUnit(string unit);
		public abstract void MTFRespawn(bool isCI);
		public abstract void RestartRound();
		public abstract bool RoundLock { get; set; }
		public abstract bool LobbyLock { get; set; }
	}
}
