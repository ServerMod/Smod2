using Smod2.Game;

namespace Smod2.Events
{
	public interface IEventAssignTeam : IEvent
	{
		// Called when the player is assigned a team at round start.
		void OnAssignTeam(Player player, out Teams team);
	}

	public interface IEventSetClass : IEvent
	{
		// Called when the player is set a class, at any point in the game
		void OnAssignTeam(Player player, out TeamClass teamclass);
	}

	public interface IEventTeamRespawn : IEvent
	{
		// Called when CI/NTF respawn
		void OnTeamRespawn(Player[] deadPlayers, out Player[] selectedPlayers, out bool spawnChaos);
	}

	public interface IEventInitSCP : IEvent
	{
		// Called when an SCP is init'd, happens at start of round i think
		void OnInitSCP(TeamClass teamclass);
	}

	public interface IEventDecideTeamRespawnQueue : IEvent
	{
		// when the server reads in the team_respawn_queue setting, you can modify it here
		void OnDecideTeamRespawnQueue(out Teams[] teams);
	}

	public interface IEventBanSCP : IEvent
	{
		// when an SCP is banned, you can change this with the TeamClass object
		void OnBanSCP(TeamClass teamclass);
	}

}
