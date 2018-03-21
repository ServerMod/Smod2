using Smod2.API;

namespace Smod2.Events
{
	public interface IEventAssignTeam : IEvent
	{
		// Called when a team is picked for a player. Nothing is assigned to the player, but you can change what team the player will spawn as.
		// Make sure to set teamOutput to team at the start of your plugin.
		void OnAssignTeam(Player player, Teams team, out Teams teamOutput);
	}

	public interface IEventSetClass : IEvent
	{
		// Called after the player is set a class, at any point in the game. Be sure to set teamclassOutput to the player's class at the start of your plugin.
		// If teamclassOutput is different, the player will be given that new class.
		void OnAssignTeam(Player player, TeamClass teamclass, out TeamClass teamclassOutput);
	}

	public interface IEventTeamRespawn : IEvent
	{
		// Called when CI/NTF respawn
		void OnTeamRespawn(Player[] deadPlayers, out Player[] selectedPlayers, bool spawnChaos, out bool spawnChaosOutput);
	}

	public interface IEventInitSCP : IEvent
	{
		// Called when an SCP is init'd, happens at start of round i think
		void OnInitSCP(TeamClass teamclass);
	}

	public interface IEventDecideTeamRespawnQueue : IEvent
	{
		// when the server reads in the team_respawn_queue setting, you can modify it here. Be sure to set teamsOutput to teams at the start of your plugin.
		void OnDecideTeamRespawnQueue(Teams[] teams, out Teams[] teamsOutput);
	}

	public interface IEventBanSCP : IEvent
	{
		// when an SCP is banned, you can change this with the TeamClass object
		void OnBanSCP(TeamClass teamclass);
	}

}
