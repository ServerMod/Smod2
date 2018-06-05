using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace Smod2.EventSystem.Events
{
	public class DecideRespawnQueueEvent : Event
	{
		public Team[] Teams { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerDecideTeamRespawnQueue)handler).OnDecideTeamRespawnQueue(this);
		}
	}

    public class TeamRespawnEvent : Event
    {
        public bool SpawnChaos { get; set; }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerTeamRespawn)handler).OnTeamRespawn(this);
        }
    }
}
