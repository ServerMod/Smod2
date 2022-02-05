using Smod2.EventSystem.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerTeamRespawn : IEventHandler
	{
		/// <summary>
		/// Called before MTF or CI respawn.
		/// </summary>
		void OnTeamRespawn(TeamRespawnEvent ev);
	}

	public interface IEventHandlerSetRoleMaxHP : IEventHandler
	{
		/// <summary>
		/// Called when the max HP of each role is being set. This happens every round.
		/// </summary>
		void OnSetRoleMaxHP(SetRoleMaxHPEvent ev);
	}

	public interface IEventHandlerSetSCPConfig : IEventHandler
	{
		/// <summary>
		/// Called when the configs of SCPs are being set. This happens every round.
		/// </summary>
		void OnSetSCPConfig(SetSCPConfigEvent ev);
	}

	public interface IEventHandlerSetMTFUnitName : IEventHandler
	{
		/// <summary>
		/// Called when the name of NTF unit is about to be set. This happens when NTF units respawn.
		/// </summary>
		void OnSetMTFUnitName(SetMTFUnitNameEvent ev);
	}
}
