using Smod2.EventSystem.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerDecideTeamRespawnQueue : IEventHandler
	{
		/// <summary>  
		/// Called at the start, when the team respawn queue is being read. This happens BEFORE it fills it to full with filler_team_id.
		/// <summary>  
		void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev);
	}
}
